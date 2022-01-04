using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public Text gold;
    public float goldValue;
    private GameObject _turret1;
    public GameObject turret1 => _turret1;

    private GameObject _turret2;
    public GameObject turret2 => _turret2;

    // Start is called before the first frame update
    void Awake()
    {
        gold.text = goldValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyKilled(string enemy)
    {
        if(enemy == "enemy1")
        {
            goldValue += 10;
            gold.text = goldValue.ToString();
        }
    }

    public void TurretSpawn(GameObject turret, float cost)
    {
        if (goldValue - cost < 0)
        {
            Destroy(turret);
            goldValue -= cost;
        }
        else
        {
            goldValue -= cost;
            gold.text = goldValue.ToString();
        }
    }

    public void TurretDelete(float cost)
    {
        goldValue += cost;
        gold.text = goldValue.ToString();
    }
}
