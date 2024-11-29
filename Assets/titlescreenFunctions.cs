using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class titlescreenFunctions : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    public AudioClip[] audiothing;

    public audioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();

        if (PlayerPrefs.HasKey("music"))
        {
            audioManager.musicSource.volume = PlayerPrefs.GetFloat("music");
            musicSlider.value = PlayerPrefs.GetFloat("music");
            Debug.Log("playerprefs Music value: " + PlayerPrefs.GetFloat("music"));
            Debug.Log("MusicSource Value: " + audioManager.musicSource.volume);
            Debug.Log("Music Slider Value: " + musicSlider.value);
        }

        if (PlayerPrefs.HasKey("SFX"))
        {
            audioManager.SFXSource.volume = PlayerPrefs.GetFloat("SFX");
            sfxSlider.value = PlayerPrefs.GetFloat("SFX");
            Debug.Log(PlayerPrefs.GetFloat("SFX"));
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("1-1");
        audioManager.stopmusic();
        audioManager.playMusic(audiothing[1]);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void timeScaleChange(float scale)
    {
        Time.timeScale = scale;
    }



    public void changeMusicSlider(Slider slider)
    {
        audioManager.musicSource.volume = slider.value;
        PlayerPrefs.SetFloat("music", slider.value);
    }
    public void changeSFXSlider(Slider slider)
    {
        audioManager.SFXSource.volume = slider.value;
        PlayerPrefs.SetFloat("SFX", slider.value);
    }

    

}