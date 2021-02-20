using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AntsSpawner : MonoBehaviour
{
    Slider PercentSlider;
    Slider BornSlider;

    public float antsPerMinute = 10;
    private float spawnTimer = 20;

    public GameObject[] ants_PF;

    //New System
    bool[] CheckBoxes = new bool[3];
    public static int[] AntCount = new int[3];
    public static int[] MaxAntCount = new int[3];

    public bool IsProritet;
    public Tree.TreeType ProritetTreeType;

    public Toggle[] AntToggle;
    public Toggle[] ItemsToggle;
    public InputField[] AntMaxText;
    public Text[] AntText;

    public Sprite[] proriti;

    GameObject I;

    private void Start()
    {
        for (int i = 0; i < MaxAntCount.Length; i++)
        {
            MaxAntCount[i] = 10;
        }

        PercentSlider = GameObject.FindGameObjectWithTag("UI").transform.GetChild(3).GetComponent<Slider>();
        BornSlider = GameObject.FindGameObjectWithTag("UI").transform.GetChild(4).GetComponent<Slider>();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= 60.0f / antsPerMinute)
        {
            //antsPerMinute = BornSlider.value / 2f;
            spawnTimer = 0.0f;
            SpawnAnt();
        }
    }

    void SpawnAnt()
    {
        
        bool tmdbool = false;

        for (int i = 0; i < 3; i++)
        {
            CheckBoxes[i] = AntToggle[i].isOn;

            MaxAntCount[i] = int.Parse(AntMaxText[i].text);
        }

        for (int i = 0; i < 3; i++)
        {
            if(CheckBoxes[i])
            {
                tmdbool = true;
            }
        }

        if(!tmdbool)
        {
            for (int i = 0; i < 3; i++)
            {
                CheckBoxes[i] = true;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (CheckBoxes[i])
            {
                if(AntCount[i] < MaxAntCount[i])
                {
                    I = Instantiate(ants_PF[i], transform.position, Quaternion.identity);
                    SelectAnt.selectedAntsAll.Add(I.transform);
                    AntCount[i]++;
                    AntText[i].text = AntCount[i].ToString();

                    SetProperty(i);
                    break;
                }
                else if(i == 3)
                {
                    i = 0;
                }
                else
                {
                    i++;
                }
                
            }
        }
        
    }

    void SetProperty(int index)
    {
        if(index == 0)
        {
            I.GetComponent<AntStates>().antType = AntType.Worker;
            I.AddComponent<Worker>();
        }
        else if(index == 2)
        {
            I.GetComponent<AntStates>().antType = AntType.Solider;
            I.AddComponent<Solider>();
        }
        else if(index == 3)
        {
            Debug.Log("Fly!!");
            //Latająca
        }
    }


    public void AddMaxAnt(int i)
    {
        MaxAntCount[i] += 1;
        AntMaxText[i].text = MaxAntCount[i].ToString();
    }
    public void MinMaxAnt(int i)
    {
        if(MaxAntCount[i] > 0)
        {
            MaxAntCount[i] -= 1;
            AntMaxText[i].text = MaxAntCount[i].ToString();
        }

    }

    public void ToggleOFF(int j)
    {
        
        if (AntToggle[j].isOn)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i != j)
                {
                    AntToggle[i].isOn = false;
                    AntToggle[i].GetComponent<Image>().sprite = proriti[0];
                }
            }
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = proriti[1];
        }
        else
            AntToggle[j].GetComponent<Image>().sprite = proriti[0];


    }

    public void ToggleItem(int j)
    {
        for (int i = 0; i < 3; i++)
        {
            ItemsToggle[i].GetComponent<Image>().sprite = proriti[0];
        }

        if(EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>().isOn)
        {
            IsProritet = true;

            if (j == 0)
            {
                ProritetTreeType = Tree.TreeType.Sticks;
            }
            else if(j == 1)
            {
                ProritetTreeType = Tree.TreeType.Leaves;
            }
            else if(j == 2)
            {
                ProritetTreeType = Tree.TreeType.Fruit;
            }

            ItemsToggle[j].GetComponent<Image>().sprite = proriti[1];
        }

        

    }

    public void Display()
    {
        for (int i = 0; i < 3; i++)
        {
            AntText[i].text = AntCount[i].ToString();
        }
    }

}

public enum AntType
{
    Worker, Solider, Flying
}
