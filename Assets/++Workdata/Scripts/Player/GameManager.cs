using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //[SerializeField] private GameObject pausePanel;
    //[SerializeField] private GameObject settingsPanel;
    //
    //[SerializeField] private GameObject EndPanel;
    //[SerializeField] private GameObject WinZone;
    //[SerializeField] private GameObject DialoguePanel;
    
    [SerializeField] private InputActionReference RestartAction;
    
    private bool isPaused;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // EndPanel.SetActive(false);
        // DialoguePanel.SetActive(false);
    }
    
    public void RestartButton(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Reset Game State when R ist pressed.
        GameState.ResetState();
    }
    
    public void PauseButton(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
       // pausePanel.SetActive(true);
       // settingsPanel.SetActive(false);
    }

    public void Resume()
    {
       // pausePanel.SetActive(false);
       // settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

   // public void Restart(InputAction.CallbackContext context)
   // {
   //     GameState.ResetState();
   //     SceneManager.LoadScene("GameScene2");
   //     Time.timeScale = 1f;
   //     isPaused = false;
   // }
   
     public void Restart()
     {
         GameState.ResetState();
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
         Time.timeScale = 1f;
         isPaused = false;
     }

    
    //Win Function
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            TogglePause();
          // settingsPanel.SetActive(false);
          // pausePanel.SetActive(false);
          // EndPanel.SetActive(false);

            Time.timeScale = 0f;
          //  DialoguePanel.SetActive(true);
        }
    }
}