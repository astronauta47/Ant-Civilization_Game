using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMainScript : MonoBehaviour
{
    GameObject antsHouse;

    public bool bKilled = false;

    public GameObject nearestAnt;
    public GameManager gameManager;
    Wall wall;

    public float hp = 5.0f;
    public float dmg = 0.5f;

    public float dmgDelay = 1.0f;
    private float dmgDelayTimer;
    private bool bCanHit = true;

    public Mesh[] types = new Mesh[5];

    public int selectedType = 0;

    private void Start()
    {
        antsHouse = GameObject.FindWithTag("Home");

        FindAnt();
    }

    public void SelectMesh(int index)
    {
        selectedType = Mathf.Clamp(index, 0, 5);
        GetComponent<MeshFilter>().mesh = types[selectedType];
    }

    void FindAnt()
    {
        Movement(FindVictim());
    }

    Vector3 FindVictim()
    {
        float dis = 10000;
        int index = 0;

        for (int i = 0; i < SelectAnt.selectedAntsAll.Count; i++)
        {
            if (dis > Vector3.Distance(transform.position, SelectAnt.selectedAntsAll[i].position))
            {
                dis = Vector3.Distance(transform.position, SelectAnt.selectedAntsAll[i].position);
                index = i;
            }
        }

        if (dis < Vector3.Distance(transform.position, antsHouse.transform.position))
        {
            return SelectAnt.selectedAntsAll[index].position;
        }

        return antsHouse.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp < 0.0f && !bKilled)
        {
            bKilled = true;

            EnemysSpawner.allEnemiesInWave--;

            transform.GetChild(1).gameObject.SetActive(true);
            GetComponent<NavMeshAgent>().isStopped = true;
            Destroy(gameObject, 10);
        }


        if (nearestAnt == null)
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
        if (bKilled) return;

        if (nearestAnt == antsHouse)
            antsHouse.GetComponent<MoundStates>().hp -= dmg;
        else
            nearestAnt.GetComponent<AntStates>().hp -= dmg;

        //nearestAnt.GetComponent<AntStates>().IsDamage = true;

        if (wall != null)
        {
            wall.hp -= dmg;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ant" && nearestAnt == null)
            nearestAnt = other.gameObject;
        else if (other.gameObject == antsHouse && nearestAnt == null)
            nearestAnt = antsHouse;

        if (other.tag == "Wall")
        {
            wall = other.GetComponent<Wall>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
        {
            wall = null;
        }
    }

    void Movement(Vector3 position)
    {
        if (nearestAnt != null)
            GetComponent<NavMeshAgent>().SetDestination(nearestAnt.transform.position);
        else
            GetComponent<NavMeshAgent>().SetDestination(antsHouse.transform.position);
    }
}
