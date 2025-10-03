using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SubtitlesBehaviour : MonoBehaviour
{
    [TextArea] public string srtText;
    public Text uiText;
    public VideoPlayer videoPlayer;

    class Cue
    {
        public double start;
        public double end;
        public string text;
    }

    private List<Cue> cues = new List<Cue>();

    void Awake()
    {
        ParseSrt(srtText);
    }

    void Update()
    {
        if (uiText == null || videoPlayer == null || !videoPlayer.isPlaying) return;
        double t = videoPlayer.time;

        // Find active cue (linear scan is OK for <= few dozen cues)
        for (int i = 0; i < cues.Count; i++)
        {
            var c = cues[i];
            if (t >= c.start && t < c.end)
            {
                uiText.text = c.text;
                return;
            }
        }
        uiText.text = "";
    }

    void ParseSrt(string srt)
    {
        cues.Clear();
        var blocks = Regex.Split(srt.Trim(), @"\r?\n\r?\n");
        var tsRe = new Regex(@"(\d{2}):(\d{2}):(\d{2}),(\d{3})\s*-->\s*(\d{2}):(\d{2}):(\d{2}),(\d{3})");

        foreach (var block in blocks)
        {
            var lines = block.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            if (lines.Length < 2) continue;

            // Line 1 may be index, line 2 is timestamps
            var m = tsRe.Match(lines.Length > 1 ? lines[1] : "");
            int offset = m.Success ? 2 : 1;
            if (!m.Success)
            {
                m = tsRe.Match(lines[0]);
                if (!m.Success) continue;
                offset = 1;
            }

            double start = HmsToSeconds(m.Groups[1].Value, m.Groups[2].Value, m.Groups[3].Value, m.Groups[4].Value);
            double end   = HmsToSeconds(m.Groups[5].Value, m.Groups[6].Value, m.Groups[7].Value, m.Groups[8].Value);
            string text = string.Join("\n", lines, offset, lines.Length - offset);

            cues.Add(new Cue { start = start, end = end, text = text });
        }
    }

    double HmsToSeconds(string h, string m, string s, string ms)
    {
        return int.Parse(h) * 3600 + int.Parse(m) * 60 + int.Parse(s) + int.Parse(ms) / 1000.0;
    }
}
