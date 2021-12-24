using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    private string objectiveName = "portal";

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
            Vector3 targetPosition = new Vector3(objective.transform.position.x, transform.position.y, objective.transform.position.z);

            gameObject.transform.LookAt(targetPosition);
        }
    }
}
