using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    Vector3 Home;
    Transform MyHome;
    int index;
    bool Iswear;
    bool IsNull;

    float delay = 7f;
    public float timer;

    public bool IsWorking = true;

    GameManager gameManager;
    List<Transform> PrioritiTreeList = new List<Transform>();

    private void Start()
    {
        MyHome = GameObject.FindGameObjectWithTag("Home").transform;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        GoToTree();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Tree" && !Iswear)
        {
            if (other.GetComponent<Tree>().FruitCount != 0)
            {
                other.GetComponent<Tree>().FruitCount--;
                //other.GetComponent<Tree>().ApplyText();
                Instantiate(other.GetComponent<Tree>().items[Random.Range(0, other.GetComponent<Tree>().items.Length)], transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, transform);
                IsNull = false;
            }
            else
            {
                IsNull = true;
                return;
            }

            FindHome();
            GetComponent<NavMeshAgent>().SetDestination(Home);
            Iswear = true;
        }
        else if(other.tag == "Home" && Iswear)
        {
            GoToTree();
            if(transform.childCount > 3) gameManager.RandomFood(transform.GetChild(3).gameObject.name);
            Iswear = false;
            Destroy(transform.GetChild(3).gameObject);
        }
    }

    public void GoToTree()
    {
        if(!MyHome.GetComponent<AntsSpawner>().IsProritet)
        {
            FindNormalTree();
        }
        else
        {
            for (int i = 0; i < GameManager.AllTree.Count; i++)
            {
                if(GameManager.AllTree[i].GetComponent<Tree>().treeType == MyHome.GetComponent<AntsSpawner>().ProritetTreeType)
                {
                    PrioritiTreeList.Add(GameManager.AllTree[i]);
                }
            }

            if(PrioritiTreeList.Count == 0)
            {
                FindNormalTree();
                Debug.Log("Normal");
            }
            else
            {
                GetComponent<NavMeshAgent>().SetDestination(PrioritiTreeList[Random.Range(0, PrioritiTreeList.Count)].position);
            }
        }

    }

    void FindNormalTree()
    {
        if (index >= GameManager.AllTree.Count)
        {
            index = 0;
        }

        GetComponent<NavMeshAgent>().SetDestination(GameManager.AllTree[index].position);
        index++;
    }

    private void Update()
    {
        if(!IsWorking)
        {
            timer += Time.deltaTime;

            if (timer > delay)
            {
                FindHome();
                GetComponent<NavMeshAgent>().SetDestination(Home);
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<BoxCollider>().enabled = true;
                IsWorking = true;
            }
        }

    }

    void FindHome()
    {
        float dis = 100000;
        int disIndex = 0;

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Home").Length; i++)
        {
           if(dis > Vector3.Distance(transform.position, GameObject.FindGameObjectsWithTag("Home")[i].transform.position))
           {
                dis = Vector3.Distance(transform.position, GameObject.FindGameObjectsWithTag("Home")[i].transform.position);
                disIndex = i;
           }
        }

        Home = GameObject.FindGameObjectsWithTag("Home")[disIndex].transform.position;
    }
}
