using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float healthPoints;
    public float currentHealth;
    public float attackDamage;
    public float movementSpeed;
    public float goldOnKill;

    private CharacterController controler;

    private Transform[] points;
    private Transform target;
    private int wayPointIndex;
    private float rotationFactor = 2.0f;

    public Animator animator;
    public Slider healthBar;

    private GameObject _gold;
    public GameObject gold => _gold;

    public static bool showHealthBars = true;

    private void Awake()
    {
        GameEvents.instance.OnDisableHealthBars += RemoveHealthBars;
        GameEvents.instance.OnEnableHealthBars += AddHealthBars;
        GameEvents.instance.OnMapDetected += FindWayPoints;
    }

    // Start is called before the first frame update
    void Start()
    {
        _gold = GameObject.Find("Gold");

        healthBar.gameObject.SetActive(showHealthBars);

        currentHealth = healthPoints;

        animator.SetFloat("MovSpeed", movementSpeed);

        controler = GetComponent<CharacterController>();

        FindWayPoints();

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

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Creature_death_1");

            _gold.GetComponent<GoldManager>().AddGold(goldOnKill);
        }

    }

    private void LateUpdate()
    {
        healthBar.transform.LookAt(Camera.main.transform);
        healthBar.transform.Rotate(0, 180, 0);
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

    public void UpdateHealthBar()
    {
        healthBar.value = currentHealth / healthPoints;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.GetComponent<Castle>() != null)
        {
            Castle castle = GameManager.instance.castle.GetComponent<Castle>();

            castle.healthPoints -= attackDamage;
            GameEvents.instance.DamageTaken();
            FindObjectOfType<AudioManager>().Play("Hit_2");
            Destroy(gameObject);
        }
    }

    void FindWayPoints()
    {
        GameObject wayPoints = GameObject.Find("WayPoints");

        points = new Transform[wayPoints.transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = wayPoints.transform.GetChild(i);
        }

        target = points[wayPointIndex];
    }

    void RemoveHealthBars()
    {
        healthBar.gameObject.SetActive(false);
        showHealthBars = false;
    }

    void AddHealthBars()
    {
        healthBar.gameObject.SetActive(true);
        showHealthBars = true;
    }

    private void OnDestroy()
    {
        GameEvents.instance.OnDisableHealthBars -= RemoveHealthBars;
        GameEvents.instance.OnEnableHealthBars -= AddHealthBars;
    }

}
