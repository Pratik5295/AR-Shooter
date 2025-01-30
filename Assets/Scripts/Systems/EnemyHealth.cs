public class EnemyHealth : Health
{
    public override void Die()
    {
        base.Die();

        //Notify level manager about death

        LevelManager.Instance.EnemyDefeated();
    }
}
