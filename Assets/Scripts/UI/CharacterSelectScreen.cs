using UnityEngine;

public class CharacterSelectScreen : GameScreen
{
    public void OnBack()
    {
        MainMenuManager.Instance.ShowMainMenu();
    }

    public void OnCharacterSelect(int character)
    {

    }
}
