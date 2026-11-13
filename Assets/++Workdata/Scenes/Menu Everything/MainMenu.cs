using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider volumeslider;
    public Slider sfxslider;

    private void Start()
    {
        LoadVolume();
        MusicManager.Instance.PlayMusic("Main Menu");

        
    }
    public void onclick()
    {
         SoundManager.Instance.PlaySound2D("CLICK");
    }
  public void PlayGame()
  {
      TransitionManagerscript.Instance.LoadScene("GameSceneFINAL","transition");
      MusicManager.Instance.PlayMusic("Game");
  }

  public void QuitGame()
  {
      Application.Quit();
  }
  public void UpdateMusicVolume (float volume)
  {
    audioMixer.SetFloat("ParameterforVolume",volume);
  }
  public void UpdateSoundVolume (float volume)
  {
    audioMixer.SetFloat("ParameterforSFX",volume);
  }
  public void SaveVolume()
  {
     audioMixer.GetFloat("ParameterforVolume", out float parameterforVolume);
        PlayerPrefs.SetFloat("ParameterforVolume", parameterforVolume);
 
        audioMixer.GetFloat("ParameterforSFX", out float parameterforSFX);
        PlayerPrefs.SetFloat("ParameterforSFX", parameterforSFX);
  }
  public void LoadVolume()
    {
        volumeslider.value = PlayerPrefs.GetFloat("ParameterforVolume");
        sfxslider.value = PlayerPrefs.GetFloat("ParameterforSFX");
    }
}
