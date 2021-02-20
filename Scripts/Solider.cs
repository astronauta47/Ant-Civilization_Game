using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Solider : MonoBehaviour
{
    public float timer;

    int index = 1;
    public bool IsPatroling = true;

    private void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(GameManager.PatrolPointList[0].position);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > 60)
        {
            IsPatroling = true;
            GetComponent<NavMeshAgent>().SetDestination(GameManager.PatrolPointList[Random.Range(0, 4)].position);
            timer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPatroling)
        {
            if (other.tag == "PatrolPoint")
            {
                GetComponent<NavMeshAgent>().SetDestination(GameManager.PatrolPointList[index].position);
                index++;
                if (index == GameManager.PatrolPointList.Count)
                    index = 0;
            }
        }

    }
}
