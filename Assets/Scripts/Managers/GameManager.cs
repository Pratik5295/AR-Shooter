using System;
using UnityEngine;

    public enum GameState
    {
        DEFAULT = 0,
        GAME = 1,
        PAUSED = 2
    }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private GameState State;

    public GameState GetCurrentState() => State;

    public bool IsGameRunning() => State == GameState.GAME;

    [SerializeField]
    private GameObject PlayerObject;

    public Action<GameState> OnStateChanged;

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
        PlayerObject = gamePlayer;
    }

}

