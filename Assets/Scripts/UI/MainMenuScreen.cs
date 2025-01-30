using UnityEngine;

public class MainMenuScreen : GameScreen
{
    public void OnStartGame()
    {
        MainMenuManager.Instance.StartGame();
    }

    public void OnCharacterSelect()
    {
        MainMenuManager.Instance.OpenCharacterSelect();
    }

    public void OnQuitGame()
    {
        MainMenuManager.Instance.QuitGame();
    }

}
