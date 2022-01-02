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
                gold.text = (goldInt - 100).ToString();
                goldInt -= 100;
                _turret1.SetActive(true);
            }
        }

        if(_turret1 == null && t1Active == true)
        {
            t1Active = false;
            gold.text = (goldInt + 100).ToString();
            goldInt += 100;
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
                gold.text = (goldInt - 200).ToString();
                goldInt -= 200;
                _turret2.SetActive(true);
            }
        }

        if (_turret2 == null && t2Active == true)
        {
            t2Active = false;
            gold.text = (goldInt + 200).ToString();
            goldInt += 200;
        }
    }
}
