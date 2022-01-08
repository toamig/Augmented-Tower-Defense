using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int helthPoints;
    public int attackDamage;
    public float movementSpeed;
    public int goldOnKill;

    private CharacterController controler;

    private Transform[] points;
    private Transform target;
    private int wayPointIndex;
    private float rotationFactor = 2.0f;

    public Animator animator;

    private float countDown = 3f;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetFloat("MovSpeed", movementSpeed);

        controler = GetComponent<CharacterController>();

        GameObject wayPoints = GameObject.Find("WayPoints");

        points = new Transform[wayPoints.transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = wayPoints.transform.GetChild(i);
        }

        wayPointIndex = 0;
        target = points[wayPointIndex];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(wayPointIndex >= 1)
        {
            HandleRotation();
        }

        Vector3 dir = target.position - transform.position;
        controler.SimpleMove(dir.normalized * movementSpeed);

        

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            GetNextWayPoint();
        }

    }

    private void HandleRotation()
    {
        Vector3 lookAtPos;

        lookAtPos.x = target.position.x;
        lookAtPos.y = transform.position.y;
        lookAtPos.z = target.position.z;

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(lookAtPos - transform.position);
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactor * Time.deltaTime);


    }

    private void GetNextWayPoint()
    {
        if(wayPointIndex >= points.Length - 1)
        {
            target = GameManager.instance.castle.transform;
        }
        else
        {
            wayPointIndex++;
            target = points[wayPointIndex];
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.GetComponent<Projectile>() != null)
        {
            animator.SetBool("Hit", true);
        }
        else if (hit.gameObject.GetComponent<Castle>() != null)
        {
            Castle castle = GameManager.instance.castle.GetComponent<Castle>();

            castle.healthPoints -= attackDamage;
            GameEvents.instance.DamageTaken();

            Destroy(gameObject);
        }
    }

}
