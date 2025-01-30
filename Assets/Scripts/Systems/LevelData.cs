using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public int enemyCount;
    public float spawnInterval;
}