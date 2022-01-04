using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public Text gold;
    public int goldInt;
    private GameObject _turret1;
    public GameObject turret1 => _turret1;

    private GameObject _turret2;
    public GameObject turret2 => _turret2;

    private bool t1Active = false;
    private bool t2Active = false;


    // Start is called before the first frame update
    void Awake()
    {
        gold.text = goldInt.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _turret1 = GameObject.Find("FattyCannonG02");
        _turret2 = GameObject.Find("FattyMortarG02");

        if (_turret1 != null && t1Active == false)
        {
            if(goldInt - 100 < 0)
            {
                _turret1.SetActive(false);
            }
            else
            {
                t1Active = true;
                goldInt -= 100;
                gold.text = goldInt.ToString();
                _turret1.SetActive(true);
            }
        }

        if(_turret1 == null && t1Active == true)
        {
            t1Active = false;
            goldInt += 100;
            gold.text = goldInt.ToString();
        }

        if (_turret2 != null && t2Active == false)
        {
            if (goldInt - 200 < 0)
            {
                _turret2.SetActive(false);
            }
            else
            {
                t2Active = true;
                goldInt -= 200;
                gold.text = goldInt.ToString();
                _turret2.SetActive(true);
            }
        }

        if (_turret2 == null && t2Active == true)
        {
            t2Active = false;
            goldInt += 200;
            gold.text = goldInt.ToString();
        }
    }

    public void EnemyKilled(string enemy)
    {
        if(enemy == "enemy1")
        {
            goldInt += 10;
            gold.text = goldInt.ToString();
        }
    }
}
