using TMPro;
using UnityEngine;


/// <summary>
/// Functions as the UI Manager
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance = null;


    [Header("UI Screens")]
    public GameScreen mainMenuScreen;
    public GameScreen pauseScreen;
    public GameScreen characterSelectScreen;


    [SerializeField]
    private TextMeshProUGUI statusText;

    private GameScreen activeScreen;
    private bool isPaused = false;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetActiveScreen(mainMenuScreen);
    }

    public void StartGame()
    {
        Debug.Log("Starting Game...");
        SetActiveScreen(null); // Hide all menus

        GameManager.Instance.StartGame();
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            SetActiveScreen(pauseScreen);
            Time.timeScale = 0f;
            Debug.Log("Game Paused");
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            isPaused = false;
            SetActiveScreen(null); // Hide pause menu
            Time.timeScale = 1f;
            Debug.Log("Game Resumed");
        }
    }

    public void OpenCharacterSelect()
    {
        Debug.Log("Opening Character Select...");
        SetActiveScreen(characterSelectScreen);
    }

    public void ShowMainMenu()
    {
        Debug.Log("Showing Main Menu...");
        SetActiveScreen(mainMenuScreen);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    private void SetActiveScreen(GameScreen newScreen)
    {
        if (activeScreen != null)
        {
            activeScreen.Hide();
        }

        activeScreen = newScreen;

        if (activeScreen != null)
        {
            activeScreen.Show();
        }
    }


    public void SetStatusText(string text)
    {
        statusText.text = text;
    }
}
