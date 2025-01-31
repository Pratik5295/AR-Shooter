using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public enum GameState
{ 
   DEFAULT = 0,
   GAME = 1,
   PAUSED = 2,
   GAMEOVER = 3
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


    public Action<GameState> OnStateChanged;


    [Header("Reference to AR components")]
    [SerializeField]
    private GameObject ARManager;

    [SerializeField]
    private ObjectSpawner ObjectSpawner;


    [Header("Character Selection")]
    [SerializeField]
    private CharacterSelect CharacterSelect = CharacterSelect.DEFAULT;

    public List<GameObject> PlayerPrefabs = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            State = GameState.DEFAULT;
        }
        else
        {
            Destroy(gameObject);
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

    public void StartGame()
    {
        SetState(GameState.GAME);

        //Update the AR related game objects about scene start
        UpdateARManager();
    }

    public void GameOver()
    {
        SetState(GameState.GAMEOVER);
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

    public void UpdateARManager()
    {
        ARManager.SetActive(true);

        //Set the updated character
        List<GameObject> objectsToSpawn = new List<GameObject>
        {
            GetSpawnPlayerObject()
        };




        ObjectSpawner.objectPrefabs = objectsToSpawn;
    }

    private GameObject GetSpawnPlayerObject()
    {
        switch (CharacterSelect)
        {
            case CharacterSelect.ONE:
                return PlayerPrefabs[1];
            case CharacterSelect.DEFAULT:
            default:
                return PlayerPrefabs[0];
        }
    }


    /// <summary>
    /// Only for testing purposes in Unity
    /// </summary>
    [ContextMenu("Force Start Game")]
    public void ForceStartGame()
    {
        SetState(GameState.GAME);
    }


}

