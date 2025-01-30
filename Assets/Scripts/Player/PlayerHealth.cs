using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public override void Die()
    {
        base.Die();

        //Restart scene as player died
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
}
