using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    private string objectiveName = "portal";

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
        GameObject objective = GameObject.Find(objectiveName);

        if (objective != null)
        {
            Vector3 targetPosition = new Vector3(objective.transform.position.x, transform.position.y, objective.transform.position.z);

            gameObject.transform.LookAt(targetPosition);
        }

        if (healthPoints <= 0)
        {
            Debug.Log("GGWP");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            healthPoints -= enemy.attackDamage;
        }
    }


}
