using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HeathBar : MonoBehaviour
{

    private string objectiveName = "castle";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GameObject objective = GameObject.Find(objectiveName);

        if (objective != null)
        {
            gameObject.GetComponent<Slider>().value = (float)(objective.GetComponent<Castle>().healthPoints/objective.GetComponent<Castle>().maxHealthPoints);
            Debug.Log((float)(objective.GetComponent<Castle>().healthPoints / objective.GetComponent<Castle>().maxHealthPoints));
            Debug.Log(objective.GetComponent<Castle>().healthPoints);
        }

    }
}
