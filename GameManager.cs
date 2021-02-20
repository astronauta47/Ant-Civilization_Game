using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    float timer;
    int houngryAnts;

    public static List<Transform> AllTree = new List<Transform>();
    public static List<Transform> PatrolPointList = new List<Transform>();

    Transform UI;
    Transform Home;

    Vector3 CamPosition;
    Quaternion CamRotation;
    bool ImAnt;
    int index;
    int NeedFood;

    PlayerMovement player;

    //Start values
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Home = GameObject.FindGameObjectWithTag("Home").transform;

        for (int i = 0; i < 4; i++)
        {
            PatrolPointList.Add(Home.GetChild(i));
        }

        AllTree.Add(GameObject.FindGameObjectWithTag("StartTree").transform);
        AllTree[0].tag = "Tree";

        UI = GameObject.FindGameObjectWithTag("UI").transform;
        PlayerPrefs.SetInt("Stick", 1000);
        PlayerPrefs.SetInt("Leaves", 1000);
        PlayerPrefs.SetInt("Food", 1000);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 15)
        {
            for (int i = 0; i < SelectAnt.selectedAntsAll.Count; i++)
            {
                if (SelectAnt.selectedAntsAll[i].GetComponent<Worker>()) NeedFood = 1;
                else if (SelectAnt.selectedAntsAll[i].GetComponent<Solider>()) NeedFood = 2;
                else  NeedFood = 25;

                if (PlayerPrefs.GetInt("Food") != 0)
                {
                    PlayerPrefs.SetInt("Food", PlayerPrefs.GetInt("Food") - NeedFood);
                }
                else
                {
                    SelectAnt.selectedAntsAll[Random.Range(0, SelectAnt.selectedAntsAll.Count)].GetComponent<AntStates>().hungry = true;
                }
            }

            for (int i = 0; i < SelectAnt.selectedAntsAll.Count; i++)
            {
                if(SelectAnt.selectedAntsAll[i].GetComponent<AntStates>().hungry)
                {
                    //Zabij mrówkę
                    Debug.Log("KILL");

                    SelectAnt.selectedAntsAll[i].GetComponent<NavMeshAgent>().isStopped = true;

                    if (SelectAnt.selectedAntsAll[i].transform.childCount > 2)
                    {
                        Destroy(SelectAnt.selectedAntsAll[i].transform.GetChild(2).gameObject);
                    }

                    Destroy(SelectAnt.selectedAntsAll[i].gameObject, 10f);

                    Death(SelectAnt.selectedAntsAll[i]);

                    SelectAnt.selectedAntsAll[i].transform.GetChild(1).gameObject.SetActive(true);
                    SelectAnt.selectedAntsAll.Remove(SelectAnt.selectedAntsAll[i]);
                    i--;

                    

                }
            }

            UI.GetChild(2).GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("Food").ToString();

            timer = 0;
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
            
                if(!ImAnt)
                {
                    CamPosition = Camera.main.transform.position;
                    CamRotation = Camera.main.transform.rotation;
                    player.enabled = false;
                    ImAntVoid();

                }
                else
                {
                    Camera.main.transform.position = CamPosition;
                    Camera.main.transform.rotation = CamRotation;
                    player.enabled = true;
                    Camera.main.transform.SetParent(player.transform);
                }

                ImAnt = !ImAnt;

            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            index++;

            if (index >= SelectAnt.selectedAntsAll.Count)
                index = 0;

            ImAntVoid();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            index--;

            if (index <= 0)
                index = SelectAnt.selectedAntsAll.Count -1;

            ImAntVoid();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(CamPosition != Vector3.zero)
            {
                Camera.main.transform.position = CamPosition;
                Camera.main.transform.rotation = CamRotation;
                player.enabled = true;
                Camera.main.transform.SetParent(player.transform);
            }

        }

    }

    void ImAntVoid()
    {
        Transform MyAnt = SelectAnt.selectedAntsAll[index];

        Camera.main.transform.position = MyAnt.position;
        Camera.main.transform.rotation = MyAnt.rotation;
        Camera.main.transform.SetParent(MyAnt.GetChild(0));
        if(MyAnt.GetComponent<Worker>())Camera.main.transform.localPosition = new Vector3(0, 0.0956f, 0);
        else if(MyAnt.GetComponent<Solider>()) Camera.main.transform.localPosition = new Vector3(0, 0.883f, 0); 
    }

    public void StartAntCameraSystem()
    {
        if (!ImAnt)
        {
            CamPosition = Camera.main.transform.position;
            CamRotation = Camera.main.transform.rotation;
            player.enabled = false;
            ImAntVoid();

        }
        else
        {
            Camera.main.transform.position = CamPosition;
            Camera.main.transform.rotation = CamRotation;
            player.enabled = true;
            Camera.main.transform.SetParent(player.transform);
        }

        ImAnt = !ImAnt;
    }

    public void Death(Transform t)
    {
        if (t.GetComponent<AntStates>().antType == AntType.Worker)
        {
            AntsSpawner.AntCount[0]--;
        }
        else if (t.GetComponent<AntStates>().antType == AntType.Solider)
        {
            AntsSpawner.AntCount[1]--;
        }
        else if (t.GetComponent<AntStates>().antType == AntType.Flying)
        {
            AntsSpawner.AntCount[2]--;
        }

        Camera.main.transform.SetParent(player.transform);
        Home.GetComponent<AntsSpawner>().Display();

        GetComponent<SoundManager>().PlayAudio(3);
        Debug.Log("Audio");
    }

    public void RandomFood(string item)
    {
        for (int i = 0; i < 3; i++)
        {
            if (item == "Stick" + i + "(Clone)")
            {
                PlayerPrefs.SetInt("Stick", PlayerPrefs.GetInt("Stick") + 1);
                UI.GetChild(0).GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("Stick").ToString();
            }
            else if (item == "Leaves" + i + "(Clone)")
            {
                PlayerPrefs.SetInt("Leaves", PlayerPrefs.GetInt("Leaves") + 1);
                UI.GetChild(1).GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("Leaves").ToString();
            }
            else if (item == "Food" + i + "(Clone)")
            {
                PlayerPrefs.SetInt("Food", PlayerPrefs.GetInt("Food") + 10);
                UI.GetChild(2).GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("Food").ToString();
            }
        }
       

    }
}
