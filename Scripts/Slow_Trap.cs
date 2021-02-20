using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Slow_Trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<NavMeshAgent>().speed /= 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Enemy")
        {
            other.GetComponent<NavMeshAgent>().speed *= 2;
        }
    }
}
