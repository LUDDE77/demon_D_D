STARTFILM – Leveranspaket (v1)

INNEHÅLL
├─ intro_voiceover_sv.txt        (VO-manus på svenska)
├─ intro_subtitles_sv.srt        (Undertexter med tidskoder, 35s)
├─ intro_shotlist.json           (Storyboard/shotlista med SFX/VFX/overlays)
├─ IntroController.cs            (Unity: spela video, skippa, spela bara en gång)
├─ SubtitlesBehaviour.cs         (Unity: enkel SRT-rendering synkad till VideoPlayer)

SÅ HÄR SÄTTER DU UPP I UNITY
1) Skapa en scen "Intro". Lägg till:
   - VideoPlayer på ett tomt GameObject. Ställ Source = VideoClip och länka din Intro.mp4.
   - En Canvas (Screen Space - Overlay) med en Text (UI) för undertexter längst ned.
   - Lägg SubtitlesBehaviour på samma Canvas-objekt, klistra in innehållet från intro_subtitles_sv.srt i srtText,
     och länka VideoPlayer + Text.
   - Lägg till en CanvasGroup + Button för "Hoppa över". Länka till IntroController.skipPrompt/skipButton.
2) Lägg IntroController på ett GameObject och länka referenserna.
3) I Build Settings: Lägg till scenerna i ordning [Intro, Level1].
4) Exportera din video i två format om du vill stödja båda lägen:
   - 1920x1080 (16:9) och 1080x1920 (9:16), H.264, 24 fps, -2 dB mix.
   Tips: Spara två VideoClips och välj rätt i runtime baserat på orientering.
5) Sätt PlayerPrefs om du vill testa omstart:
   PlayerPrefs.DeleteKey("intro_seen");

KLIPP- & LJUDANTECKNINGAR
- Musik: låg drone (D), 60–65 BPM känsla. Sista sekund: svag upplyft tonik.
- SFX: kort stinger vid hot, RF-glitch vid signalbortfall, glas-spricka 10% opacitet i början.
- Text: Använd semi-monospace/teknisk font, 80–90% vit med 30% svart outline.

LICENS & INNEHÅLL
- Alla texter och skript i detta paket är fria för dig att använda i projektet.
- Bild- och videomaterial måste du skapa/sourca själv eller generera med dina verktyg.
