using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoundStates : MonoBehaviour
{

    public float hp = 35.0f;
    UI ui;

    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0.0f)
            ui.GameOver();
    }
}
