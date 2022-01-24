using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public float healthPoints;

    public float maxHealthPoints;

    // Start is called before the first frame update
    void Start()
    {
        healthPoints = maxHealthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoints <= 0)
        {

            GameManager.instance.audioManager.Play("Defeat");
            Debug.Log("GGWP");
        }
    }


}
