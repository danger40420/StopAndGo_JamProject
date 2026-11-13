using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField]
    private Musicplayerscript musicplayer;
    [SerializeField]
    private AudioSource musicsource;

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        
    }
        public void PlayMusic(string trackName, float fadeduration = 0.5f)
        {
            StartCoroutine(AnimateMusicCrossfade(musicplayer.GetClipFromName(trackName), fadeduration));
        }

    IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeduration = 0.5f)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeduration;
            musicsource.volume = Mathf.Lerp(1f, 0, percent);
            yield return null;
        }
        musicsource.clip = nextTrack;
        musicsource.Play();

        percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeduration;
            musicsource.volume = Mathf.Lerp(0, 1f, percent);
            yield return null;
        }
    }
}
