using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave", order = 2)]
public class Wave : ScriptableObject
{
    public GameObject[] enemies; // Array prefab enemy yang akan di-spawn
    public float[] spawnYPositions; // Array posisi y untuk setiap enemy
    public float[] spawnDelays; // Array spawn delay untuk setiap enemy dalam interval ketukan
}
