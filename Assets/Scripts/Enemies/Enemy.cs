using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int helthPoints;
    public int attackDamage;
    public float attackSpeed;
    public float movementSpeed;
    public int goldOnKill;
    public bool ranged;

    public Animator animator;

    private string objectiveName = "castle";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("MovSpeed", movementSpeed);

        GameObject objective = GameObject.Find(objectiveName);

        if (objective != null)
        {
            Vector3 position = Vector3.MoveTowards(transform.position, objective.transform.position, movementSpeed * Time.deltaTime);

            transform.position = position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Projectile>() != null) { 
            animator.SetBool("Hit", true);
        }
    }

}
