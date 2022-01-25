using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING, FINISHED };

    public float timeBetweenWaves;

    public float waveCountDown;

    public float searchCountDown;

    public int nextWave;

    private SpawnState state = SpawnState.COUNTING;

    [System.Serializable]
    public struct EnemyObject
    {
        public EnemyType enemyType;
        public GameObject enemyObject;
    }

    [SerializeField]
    public EnemyObject[] enemyObjects;

    [SerializeField]
    public Wave[] waves;


    // Start is called before the first frame update
    void Start()
    {
        waveCountDown = 3f;
        searchCountDown = 1f;
        nextWave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameStarted)
        {

            if (state == SpawnState.WAITING)
            {
                if (!EnemyIsAlive())
                {
                    WaveCompleted();
                }
                else
                {
                    return;
                }
            }

            if (state != SpawnState.FINISHED && waveCountDown <= 0)
            {
                GameEvents.instance.WaveChange();

                if (state != SpawnState.SPAWNING)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }

            }
            else
            {
                waveCountDown -= Time.deltaTime;
            }                           
        }
    }

    public void WaveCompleted()
    {
        if(nextWave + 1 > waves.Length - 1)
        {
            state = SpawnState.FINISHED;
            GameEvents.instance.Victory();
            GameManager.instance.audioManager.Play("Victory");
        }
        else
        {
            state = SpawnState.COUNTING;
            waveCountDown = timeBetweenWaves;
            nextWave++;
        }
    }

    public IEnumerator SpawnWave(Wave wave)
    {
        GameManager.instance.audioManager.Play("New_round_2");

        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.enemies.Length; i++)
        {
            for (int j = 0; j < wave.enemies[i].count; j++)
            {
                SpawnEnemy(wave.enemies[i].enemyType);
                yield return new WaitForSeconds(wave.timeBetweenEnemies);
            }
        }

        state = SpawnState.WAITING;
        yield break;
               
    }

    public void SpawnEnemy(EnemyType enemyType)
    {
        Transform spawnTransform = GameManager.instance.portal.transform;

        foreach(EnemyObject enemyObject in enemyObjects)
        {
            if(enemyObject.enemyType == enemyType)
            {
                GameObject test = Instantiate(enemyObject.enemyObject, spawnTransform.position, spawnTransform.rotation);
                test.transform.LookAt(GameManager.instance.portal.transform.forward);
            }
        }
    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if(searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }
}
