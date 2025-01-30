using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public override void Die()
    {
        //Directly restart the scene instead of deleting player

        //Restart scene as player died
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
}
