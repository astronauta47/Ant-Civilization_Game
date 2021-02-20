using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AntStates : MonoBehaviour
{
    public GameObject nearestEnemy;

    public AntType antType;

    public float hp = 5.0f;
    public float dmg = 0.5f;
    public bool hungry;

    public float dmgDelay = 1.0f;
    private float dmgDelayTimer;
    private bool bCanHit = true;

    public bool IsDamage;

    GameManager gameManager;
    Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (hp <= 0.0f)
        {
            SelectAnt.selectedAntsAll.Remove(transform);
            Camera.main.transform.SetParent(player);

            transform.GetChild(1).gameObject.SetActive(true);
            GetComponent<NavMeshAgent>().isStopped = true;

            if(transform.childCount >= 3)
            {
                Destroy(transform.GetChild(2).gameObject);
            }

            Destroy(gameObject, 10f);

            gameManager.Death(transform);
            hp = 100;
        }
            

        if (nearestEnemy == null)
            return;

        if (!bCanHit)
        {
            dmgDelayTimer += Time.deltaTime;

            if (dmgDelayTimer >= dmgDelay)
            {
                dmgDelayTimer = 0.0f;
                bCanHit = true;
            }
        }
        else
        {
            bCanHit = false;
            Hit();
        }
    }

    void Hit()
    {
        nearestEnemy.GetComponent<EnemyMainScript>().hp -= dmg;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && nearestEnemy == null)
            nearestEnemy = other.gameObject;
    }
}
