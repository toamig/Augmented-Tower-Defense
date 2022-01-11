using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TurretAI : MonoBehaviour {

    public enum TurretType
    {
        Single = 1,
        Dual = 2,
        Catapult = 3,
    }
    
    public GameObject currentTarget;
    public Transform turretHead;

    public float attackDist = 10.0f;
    public float attackDamage;
    public float shootCoolDown;
    private float timer;
    public float loockSpeed;
    public float cost;

    //public Quaternion randomRot;
    public Vector3 randomRot;
    public Animator animator;

    [Header("[Turret Type]")]
    public TurretType turretType = TurretType.Single;
    
    public Transform muzzleMain;
    public Transform muzzleSub;
    public GameObject muzzleEff;
    public GameObject bullet;

    public GameObject levelUpParticles;
    private bool shootLeft = true;

    private Transform lockOnPos;

    private int maxLevel = 3;
    private int currentLevel;
    private GameObject _gold;
    public GameObject gold => _gold;

    public Material activeMaterial;
    public Material inactiveMaterial;
    public Material defaultMaterial;

    public bool set = false;

    //public TurretShoot_Base shotScript;

    void Start () {
        currentLevel = 1;

        InvokeRepeating("CheckForTarget", 0, 0.5f);
        _gold = GameObject.Find("Gold");
        _gold.GetComponent<GoldManager>().TurretSpawn(gameObject, cost);

        //shotScript = GetComponent<TurretShoot_Base>();

        if (transform.GetChild(0).GetComponent<Animator>())
        {
            animator = transform.GetChild(0).GetComponent<Animator>();
        }

        randomRot = new Vector3(0, Random.Range(0, 359), 0);
    }
	
	void Update () {
        if (set)
        {
            if (currentTarget != null)
            {
                FollowTarget();

                float currentTargetDist = Vector3.Distance(transform.position, currentTarget.transform.position);
                if (currentTargetDist > attackDist)
                {
                    currentTarget = null;
                }
            }
            else
            {
                IdleRotate();
            }

            timer += Time.deltaTime;
            if (timer >= shootCoolDown)
            {
                if (currentTarget != null)
                {
                    timer = 0;

                    if (animator != null)
                    {
                        animator.SetTrigger("Fire");
                        ShootTrigger();
                    }
                    else
                    {
                        ShootTrigger();
                    }
                }
            }
        }
        else
        {
            if (_gold.GetComponent<GoldManager>().goldValue >= cost)
            {
                _gold.GetComponent<GoldManager>().TurretSpawn(gameObject, cost);
                activeTurret();
            }
        }
	}

    private void CheckForTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackDist);
        float distAway = Mathf.Infinity;

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].tag == "Enemy")
            {
                float dist = Vector3.Distance(transform.position, colls[i].transform.position);
                if (dist < distAway)
                {
                    currentTarget = colls[i].gameObject;
                    distAway = dist;
                }
            }
        }
    }

    private void FollowTarget() //todo : smooth rotate
    {
        Vector3 targetDir = currentTarget.transform.position - turretHead.position;
        targetDir.y = 0;
        //turretHead.forward = targetDir;
        if (turretType == TurretType.Single)
        {
            turretHead.forward = targetDir;
        }
        else
        {
            turretHead.transform.rotation = Quaternion.RotateTowards(turretHead.rotation, Quaternion.LookRotation(targetDir), loockSpeed * Time.deltaTime);
        }
    }

    private void ShootTrigger()
    {
        //shotScript.Shoot(currentTarget);
        Shoot(currentTarget);
        //Debug.Log("We shoot some stuff!");
    }
    
    Vector3 CalculateVelocity(Vector3 target, Vector3 origen, float time)
    {
        Vector3 distance = target - origen;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDist);
    }

    public void IdleRotate()
    {
        bool refreshRandom = false;
        
        if (turretHead.rotation != Quaternion.Euler(randomRot))
        {
            turretHead.rotation = Quaternion.RotateTowards(turretHead.transform.rotation, Quaternion.Euler(randomRot), loockSpeed * Time.deltaTime * 0.2f);
        }
        else
        {
            refreshRandom = true;

            if (refreshRandom)
            {

                int randomAngle = Random.Range(0, 359);
                randomRot = new Vector3(0, randomAngle, 0);
                refreshRandom = false;
            }
        }
    }

    public void Shoot(GameObject go)
    {
        if (turretType == TurretType.Catapult)
        {
            lockOnPos = go.transform;

            Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
            GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
            Projectile projectile = missleGo.GetComponent<Projectile>();
            projectile.target = lockOnPos;
            projectile.attackDamage = attackDamage; 
        }
        else if(turretType == TurretType.Dual)
        {
            if (shootLeft)
            {
                Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
                GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
                Projectile projectile = missleGo.GetComponent<Projectile>();
                projectile.target = transform.GetComponent<TurretAI>().currentTarget.transform;
                projectile.attackDamage = attackDamage;
            }
            else
            {
                Instantiate(muzzleEff, muzzleSub.transform.position, muzzleSub.rotation);
                GameObject missleGo = Instantiate(bullet, muzzleSub.transform.position, muzzleSub.rotation);
                Projectile projectile = missleGo.GetComponent<Projectile>();
                projectile.target = transform.GetComponent<TurretAI>().currentTarget.transform;
                projectile.attackDamage = attackDamage;
            }

            shootLeft = !shootLeft;
        }
        else
        {
            Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
            GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
            Projectile projectile = missleGo.GetComponent<Projectile>();
            projectile.target = currentTarget.transform;
            projectile.attackDamage = attackDamage;
        }
    }

    public void Upgrade()
    {
        if (currentLevel < maxLevel)
        {
            attackDamage *= 1.5f;
            attackDist *= 1.2f;
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            ParticleSystem[] particles = levelUpParticles.GetComponentsInChildren<ParticleSystem>();

            foreach(ParticleSystem particle in particles)
            {
                particle.Play();
            }

            // save current augmentation state
            VuMarkHandler vuMarkHandler = GameManager.instance.vuMarkHandler;
            VuMarkBehaviour vuMarkBehaviour = GetComponentInParent<VuMarkBehaviour>();
            vuMarkHandler.SaveVuMarkAugmentation(vuMarkBehaviour);

            currentLevel++;
            _gold.GetComponent<GoldManager>().RemoveGold(cost * currentLevel);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Upgrade"){
            if(cost * (currentLevel + 1) > _gold.GetComponent<GoldManager>().goldValue)
            {
                other.gameObject.GetComponent<Renderer>().material = inactiveMaterial;
            }
            else
            {
                Upgrade();
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Upgrade")
        {
            other.gameObject.GetComponent<Renderer>().material = defaultMaterial;
        }
    }

    public void OnDestroy()
    {
        if (set)
        {
            _gold.GetComponent<GoldManager>().AddGold(cost);
        }
    }

    public void inactiveTurret()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            if(r.name != "Quad" && r.name != "Eff_Spark00")
                r.material = inactiveMaterial;
        }
    }

    public void activeTurret()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            if (r.name != "Quad" && r.name != "Eff_Spark00")
                r.material = activeMaterial;
        }
    }
}
