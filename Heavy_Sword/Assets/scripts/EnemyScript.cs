using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{

    Rigidbody rb;
    Collider colider;
    NavMeshAgent agent;
    public float hitBackSpeed, hitCoolDown, hurtCoolDown;

    GameObject Player;
    playerScript playerScript;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool hitPlayer = true;
    bool hurtEnemy = true;
    public LayerMask Ground;


    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Sword_Pivot");
        playerScript = GameObject.FindGameObjectWithTag("Player_Model").GetComponent<playerScript>();
        enemyHealth = GetComponent<EnemyHealth>();
        playerHealth = GameObject.FindGameObjectWithTag("Player_Model").GetComponent<PlayerHealth>();

        colider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), (0.2f + colider.bounds.size.y / 2), Ground))
        {
            agent.enabled = true;
            agent.SetDestination(Player.transform.position);
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            agent.enabled = false;
            rb.constraints = RigidbodyConstraints.None;

        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if ( (col.tag == "Sword" || col.tag == "Sword_Pivot" || col.tag == "Sword_End" )&& playerScript.swordSwinging && hurtEnemy)
        {
            if (playerScript.swingingRight)
            {
                hitBack(col.transform, new Vector3(0, 0, 1));
            }
            else
            {
                hitBack(col.transform, new Vector3(0, 0, -1));
            }
            
            enemyHealth.changeHealth(-1);
            StartCoroutine(hurtEnemyCoolDown());

            StartCoroutine(slowDown());


        }
        if (col.transform.tag == "Player_Model" && hitPlayer)
        {
            hitBack(transform, new Vector3(0, 0, -1));
            slowDown();
            playerHealth.changeHealth(-1);
            StartCoroutine(hitplayerCoolDown());

        }
        if(col.transform.tag == "DeathZone")
        {
            Destroy(gameObject);
        }


    }

    private void OnCollisionEnter(Collision col)
    {
       
    }

    IEnumerator hitplayerCoolDown()
    {
        hitPlayer = false;
        yield return new WaitForSeconds(hitCoolDown);
        hitPlayer = true;
    }

    IEnumerator hurtEnemyCoolDown()
    {
        hurtEnemy = false;
        yield return new WaitForSeconds(hurtCoolDown);
        hurtEnemy = true;
    }

    void hitBack(Transform reference,Vector3 direction)
    {
        rb.velocity += reference.TransformDirection((direction * hitBackSpeed));
    }

    IEnumerator slowDown()
    {
        agent.speed = 0.25f;
        yield return new WaitForSeconds(1.5f);
        agent.speed = 3.5f;
    }

}
