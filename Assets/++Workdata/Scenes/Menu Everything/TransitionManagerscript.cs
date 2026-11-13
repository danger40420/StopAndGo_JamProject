using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class TransitionManagerscript : MonoBehaviour
{
    public static TransitionManagerscript Instance;

    public Slider progressbar;

    public GameObject TransitionContainer;

    private TransitionScript[] transitions;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        transitions = TransitionContainer.GetComponentsInChildren<TransitionScript>();
    }
    public void LoadScene(string sceneName, string transitionName)
    {
        StartCoroutine(LoadSceneAsync(sceneName, transitionName));
    }
    private IEnumerator LoadSceneAsync(string sceneName, string transitionName)
{
    Debug.Log("play button requested scene: " + sceneName);

    Debug.Log("available scenes:");

    for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
    {
        Debug.Log(SceneUtility.GetScenePathByBuildIndex(i));
    }

    if (transitions == null)
    {
        Debug.LogError("Transitions array is null!");
        yield break;
    }

    TransitionScript transition = transitions.FirstOrDefault(t => t.name == transitionName);

    if (transition == null)
    {
        Debug.LogError("Transition not found: " + transitionName);
        yield break;
    }

    AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);

    if (scene == null)
    {
        Debug.LogError("failed to load: " + sceneName);
        yield break;
    }

    scene.allowSceneActivation = false;

    yield return transition.AnimateTransitionIn();

    if (progressbar != null)
        progressbar.gameObject.SetActive(true);

    while (scene.progress < 0.9f)
    {
        if (progressbar != null)
            progressbar.value = scene.progress;

        yield return null;
    }

    scene.allowSceneActivation = true;

    if (progressbar != null)
        progressbar.gameObject.SetActive(false);

    yield return transition.AnimateTransitionOut();
}
}
