using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public float timeBetweenWaves;

    private float countDown;

    private int currentWave;

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

    private Transform spawn;


    // Start is called before the first frame update
    void Start()
    {
        countDown = 2f;
        currentWave = 0;
    }

    // Update is called once per frame
    void Update()
    {

        spawn = GameManager.instance.portal.transform;

        if (countDown <= 0)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }

        countDown -= Time.deltaTime;

    }

    public IEnumerator SpawnWave()
    {
        Debug.Log(currentWave);

        for (int i = 0; i < waves[currentWave].enemies.Length; i++)
        {
            for (int j = 0; j < waves[currentWave].enemies[i].count; j++)
            {
                SpawnEnemy(waves[currentWave].enemies[i].enemyType);
                yield return new WaitForSeconds(waves[currentWave].timeBetweenEnemies);
            }
        }

        if(currentWave + 1 == waves.Length)
        {
            Debug.Log("No More Waves");
            yield break;
        }
        else
        {
            currentWave++;
        }

               
    }

    public void SpawnEnemy(EnemyType enemyType)
    {
        foreach(EnemyObject enemyObject in enemyObjects)
        {
            if(enemyObject.enemyType == enemyType)
            {
                GameObject test = Instantiate(enemyObject.enemyObject, spawn.position, spawn.rotation);
                test.transform.LookAt(GameManager.instance.castle.transform);
                //test.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 5));
            }
        }
    }
}
