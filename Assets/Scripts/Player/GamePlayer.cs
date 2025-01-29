using UnityEngine;

/// <summary>
/// Base Player class for the game to communicate
/// </summary>
public class GamePlayer : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    private void Start()
    {
        //Find Game manager and notify the reference
        gameManager = GameManager.Instance;

        if(gameManager != null )
        {
            gameManager.SetPlayer(gameObject);
        }
    }
}
