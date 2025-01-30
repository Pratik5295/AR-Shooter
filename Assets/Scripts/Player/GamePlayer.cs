using UnityEngine;

/// <summary>
/// Base Player class for the game to communicate
/// </summary>
public class GamePlayer : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private GameObject playerObject;

    private void Start()
    {
        //Find Game manager and notify the reference
        gameManager = GameManager.Instance;

        if(gameManager != null )
        {
            gameManager.SetPlayer(playerObject);
        }
    }
}
