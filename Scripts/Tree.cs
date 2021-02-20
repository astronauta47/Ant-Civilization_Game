using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    public int FruitCount;
    public GameObject[] items;
    public TreeType treeType;

    //TextMesh FruitCountText;
    float timer;
    float delay;

    private void Start()
    {
        FruitCount = Random.Range(100, 300);
        delay = Random.Range(2, 3);

        //FruitCountText = GetComponentInChildren<TextMesh>();
        //ApplyText();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > delay * 3)
        {
            FruitCount++;
            //ApplyText();
            timer = 0;
        }
    }

    //public void ApplyText()
    //{
    //    FruitCountText.text = FruitCount.ToString();
    //}

    public enum TreeType
    {
        Fruit, Sticks, Leaves
    }

}
