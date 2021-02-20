using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopElement : MonoBehaviour
{
    public GameObject BuildGO;
    public List<Price> Cost;

    [Serializable]
    public struct Price
    {
        public string ItemName;
        public int count;
    }

}


