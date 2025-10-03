using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    [Header("References")]
    public VideoPlayer videoPlayer;
    public CanvasGroup skipPrompt;
    public Button skipButton;
    public string nextSceneName = "Level1";
    public bool playOnlyOnce = true;

    [Header("Settings")]
    public float skippableAfterSeconds = 5f;

    private bool _skippable = false;
    private const string INTRO_SEEN_KEY = "intro_seen";

    void Awake()
    {
        if (playOnlyOnce && PlayerPrefs.GetInt(INTRO_SEEN_KEY, 0) == 1)
        {
            Finish();
            return;
        }

        if (skipPrompt != null) skipPrompt.alpha = 0f;
        if (skipButton != null) skipButton.onClick.AddListener(Skip);

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.Play();
        }
        Invoke(nameof(EnableSkip), skippableAfterSeconds);
    }

    void Update()
    {
        if (!_skippable) return;

        // Touch anywhere or press Space/Escape to skip.
        if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            Skip();
        }
    }

    void EnableSkip()
    {
        _skippable = true;
        if (skipPrompt != null)
        {
            skipPrompt.alpha = 1f;
            skipPrompt.blocksRaycasts = true;
            skipPrompt.interactable = true;
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        Finish();
    }

    public void Skip()
    {
        Finish();
    }

    void Finish()
    {
        PlayerPrefs.SetInt(INTRO_SEEN_KEY, 1);
        PlayerPrefs.Save();

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // If no scene provided, just disable this object.
            gameObject.SetActive(false);
        }
    }
}
