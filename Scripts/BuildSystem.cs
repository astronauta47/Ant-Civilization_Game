using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public GameObject BuildGameObject;
    public GameObject BuildGameObjectInstant;
    public SoundManager soundManager;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundManager>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit[] hits = Physics.RaycastAll(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition));

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.tag == "Map")
                {
                    BuildGameObjectInstant = Instantiate(BuildGameObject, hits[i].point + new Vector3(0, 0.2f, 0), Quaternion.identity);
                    soundManager.PlayAudio(4);
                    enabled = false;
                }
                    
            }

        }
    }
}
