using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafTrap : MonoBehaviour
{

    public int numOfUses = 5;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            numOfUses--;

            if (numOfUses <= 0)
                Destroy(this.gameObject);

        }
    }
}
