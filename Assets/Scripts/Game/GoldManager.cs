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
        GameEvents.instance.OnStartGame += PassiveGold;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurretSpawn(GameObject turret, float cost)
    {
        if (goldValue - cost < 0)
        {
            turret.GetComponent<TurretAI>().inactiveTurret();
        }
        else
        {
            goldValue -= cost;
            gold.text = goldValue.ToString();
            turret.GetComponent<TurretAI>().set = true;
            GameManager.instance.audioManager.Play("Upgrade");

        }
    }

    public void AddGold(float cost)
    {
        goldValue += cost;
        gold.text = goldValue.ToString();
    }

    public void RemoveGold(float cost)
    {
        goldValue -= cost;
        gold.text = goldValue.ToString();
    }

    public void PassiveGold()
    {
        InvokeRepeating("AddPassiveGold", 2.0f, 2.0f);
    }

    private void AddPassiveGold()
    {
        AddGold(2);
    }
}
