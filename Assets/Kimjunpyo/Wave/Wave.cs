using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "So/Wave")]
public class Wave : ScriptableObject
{
    public List<WaveEnemyData> enemies;
    public float hpMultiplier = 1.0f;
    public float atkMultiplier = 1.0f;
}
