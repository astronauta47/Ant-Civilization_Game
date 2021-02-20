using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public BuildSystem buildSystem;

    public Canvas GameOverCanvas;

    public void Buy()
    {
        ShopElement shopElement = EventSystem.current.currentSelectedGameObject.GetComponent<ShopElement>();

        for (int i = 0; i < shopElement.Cost.Count; i++)
        {

            if (PlayerPrefs.GetInt(shopElement.Cost[i].ItemName) >= shopElement.Cost[i].count)
            {
                PlayerPrefs.SetInt(shopElement.Cost[i].ItemName, PlayerPrefs.GetInt(shopElement.Cost[i].ItemName) - shopElement.Cost[i].count);

                transform.GetChild(0).GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("Stick").ToString();
                transform.GetChild(1).GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("Leaves").ToString();
                transform.GetChild(2).GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt("Food").ToString();

                buildSystem.BuildGameObject = shopElement.BuildGO;
                buildSystem.enabled = true;
            }
        }
        
    }

    public void GameOver()
    {
        GameOverCanvas.gameObject.SetActive(true);
    }

    public void GameOverButton()
    {
        SceneManager.LoadScene(0);
    }

    public void Rotate()
    {
        buildSystem.BuildGameObjectInstant.transform.Rotate(0, 90, 0);
    }

    private void OnMouseEnter()
    {
        Debug.Log("Podpowiedź");
    }

}
