using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowEnemy : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if(!other.GetComponent<EnemyMainScript>().bKilled)
            {
                transform.parent.GetComponent<NavMeshAgent>().SetDestination(other.transform.position);
            }
            else
            {
                if(GetComponentInParent<Worker>())
                {
                    GetComponentInParent<Worker>().IsWorking = false;
                    GetComponentInParent<Worker>().timer = 10000;
                }
            }
        }
    }
}
