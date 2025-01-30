using UnityEngine;

public class PauseScreen : GameScreen
{
    public void OnResume()
    {
        MainMenuManager.Instance.ResumeGame();
    }
}
