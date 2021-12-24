using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public float timeBetweenEnemies;

    [System.Serializable]
    public struct SubWave
    {
        public EnemyType enemyType;
        public int count;
    }

    public SubWave[] enemies;
}
