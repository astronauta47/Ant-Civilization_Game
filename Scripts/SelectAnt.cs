using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class SelectAnt : MonoBehaviour
{
    public static List<Transform> selectedAnts = new List<Transform>();
    public static List<Transform> selectedAntsAll = new List<Transform>();//Lista wszystkich mrówek obecnych na scenie
    
    public Vector3 mousePosition;

    Vector3 mousePos1, mousePos2;
    Vector3 StartPos, EndPos;
    public RectTransform SelectedBox;
    public GameObject cursorTexture;

    SoundManager soundManager;
    public bool audioAttack;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundManager>();
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectedBox.gameObject.SetActive(true);
                mousePos1 = GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition);

                RaycastHit[] hits = Physics.RaycastAll(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition));

                if (hits.Length != 0)
                {
                    for (int i = 0; i < hits.Length; i++)
                    {
                        StartPos = hits[0].point;

                        if (hits[i].collider.tag == "Ant")
                        {
                            if (selectedAnts.Count != 0)
                            {
                                for (int j = 0; j < selectedAnts.Count; j++)
                                {
                                    if (hits[i].collider.transform.Equals(selectedAnts[j]))
                                    {
                                        Debug.Log("Remove");
                                        selectedAnts[0].GetChild(2).gameObject.SetActive(false);
                                        selectedAnts.Remove(hits[i].collider.transform);
                                    }
                                    else
                                    {
                                        Debug.Log("Add");
                                        selectedAnts.Add(hits[i].collider.transform);
                                        if(selectedAnts[0].childCount > 2) selectedAnts[0].GetChild(2).gameObject.SetActive(true);
                                        break;
                                    }

                                }
                            }
                            else
                            {
                                Debug.Log("Add1");
                                selectedAnts.Add(hits[i].collider.transform);
                                selectedAnts[0].GetChild(2).gameObject.SetActive(true);
                            }

                        }

                    }

                }

            }
            if (Input.GetMouseButton(0))
            {
                EndPos = Input.mousePosition;

                Vector3 BoxStart = GetComponent<Camera>().WorldToScreenPoint(StartPos);
                BoxStart.z = 0f;

                Vector3 centre = (BoxStart + EndPos) / 2;
                SelectedBox.position = centre;

                float sizeX = Mathf.Abs(BoxStart.x - EndPos.x);
                float sizeY = Mathf.Abs(BoxStart.y - EndPos.y);

                SelectedBox.sizeDelta = new Vector2(sizeX, sizeY);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                mousePos2 = GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition);
                SelectedBox.gameObject.SetActive(false);

                Rect selectRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);

                foreach (Transform item in selectedAntsAll)
                {
                    if (item != null)
                    {
                        if (selectRect.Contains(GetComponent<Camera>().WorldToViewportPoint(item.transform.position), true))
                        {
                            selectedAnts.Add(item);
                            item.GetChild(2).gameObject.SetActive(true);
                        }
                    }
                }
            }

            else if (Input.GetMouseButtonDown(1))
            {
                RaycastHit[] hits = Physics.RaycastAll(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition));

                if (hits.Length != 0)
                {
                    for (int i = 0; i < hits.Length; i++)
                    {

                        if (hits[i].collider.tag == "Map" || hits[i].collider.tag == "FoW")
                        {
                            for (int j = 0; j < selectedAnts.Count; j++)
                            {
                                selectedAnts[j].GetComponent<NavMeshAgent>().SetDestination(hits[i].collider.transform.position);
                                if (selectedAnts[j].childCount > 2) selectedAnts[j].GetChild(2).gameObject.SetActive(false);
                                else return;

                                if (selectedAnts[j].GetComponent<Worker>())
                                {
                                    selectedAnts[j].GetComponent<Worker>().timer = 0;
                                    selectedAnts[j].GetComponent<Worker>().IsWorking = false;
                                }
                                else if(selectedAnts[j].GetComponent<Solider>())
                                {
                                    selectedAnts[j].GetComponent<Solider>().timer = 0;
                                    selectedAnts[j].GetComponent<Solider>().IsPatroling = false;
                                }
                            }

                            if(selectedAnts.Count > 0)
                            {
                                GameObject I = Instantiate(cursorTexture, hits[i].point + new Vector3(0, 0.5f, 0), Quaternion.Euler(90, 0, 0));
                                Destroy(I, 0.5f);

                                if(!audioAttack) soundManager.PlayAudio(5);
                                else soundManager.PlayAudio(2);
                            }

                            

                            selectedAnts.Clear();
                        }
                    }
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            
            for (int i = 0; i < selectedAnts.Count; i++)
            {
                selectedAnts[i].GetChild(2).gameObject.SetActive(false);
            }

            selectedAnts.Clear();
        }
    }
}
