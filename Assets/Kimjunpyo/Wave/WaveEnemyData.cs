using UnityEngine;

[System.Serializable]
public class WaveEnemyData
{
    public SoEnemy enemySO;         // 기존 SO 방식 (기본 사용)
    public GameObject overridePrefab; // SO 대신 직접 프리팹 지정 가능 (선택적)
    public int count;
}
