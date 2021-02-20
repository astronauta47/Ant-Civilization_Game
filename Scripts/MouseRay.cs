using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseRay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Cursor newCursor;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    SelectAnt soundManager;

    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SelectAnt>();
    }

    private void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit))
        {

            if (hit.collider.tag == "Enemy" && SelectAnt.selectedAnts.Count > 0)
            {
                Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
                soundManager.audioAttack = true;
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                soundManager.audioAttack = false;
            }

        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Podpowiedź");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Podpowiedź");
    }
}
