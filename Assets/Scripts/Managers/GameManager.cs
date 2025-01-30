using System;
using TMPro;
using UnityEngine;

public enum GameState
{ 
   DEFAULT = 0,
   GAME = 1,
   PAUSED = 2
}

public enum CharacterSelect
{
    DEFAULT = 0,
    ONE = 1
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private GameState State;

    public GameState GetCurrentState() => State;

    public bool IsGameRunning() => State == GameState.GAME;

    [SerializeField]
    private GameObject playerObject;
    public GameObject PlayerObject => playerObject;

    public TextMeshProUGUI moveText;

    public Action<GameState> OnStateChanged;


    [Header("Character Selection")]
    [SerializeField]
    private CharacterSelect CharacterSelect = CharacterSelect.DEFAULT;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            State = GameState.DEFAULT;
        }
    }

    private void Start()
    {
        SetState(GameState.PAUSED);
    }

    private void SetState(GameState state)
    {
        State = state;
        OnStateChanged?.Invoke(State);
    }

    public void PauseGame()
    {
        SetState(GameState.PAUSED);
    }

    public void ResumeGame()
    {
        SetState(GameState.GAME);
    }

    public void SetPlayer(GameObject gamePlayer)
    {
        playerObject = gamePlayer;
        SetState(GameState.GAME);
    }

    public void SelectCharacter(CharacterSelect characterSelect)
    {
        CharacterSelect = characterSelect;
    }

}

