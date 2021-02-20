using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoW_DiscoverNewTerrain : MonoBehaviour
{
    public bool bDiscovered = false;
    Transform Tree;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ant")
        {
            
            bDiscovered = true;
            UpdateVisibility();
            if(Tree != null)GameManager.AllTree.Add(Tree);
        }

        if(other.tag == "Tree")
        {
            Tree = other.transform;
        }
    }

    void UpdateVisibility()
    {
        gameObject.SetActive(!bDiscovered);
    }
}
