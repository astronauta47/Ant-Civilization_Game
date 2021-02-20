using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTrap : MonoBehaviour
{
    GameObject hittedEnemy;

    float frogDmg = 10.0f;

    GameObject tongue;

    Vector3 tongueBaseScale = new Vector3(1f, 1f, 0.65f);
    Vector3 tongueScaledScale = new Vector3(3f, 1f, 0.65f);

    private float hitTimer = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        tongue = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (hitTimer >= 0.0f)
            hitTimer += Time.deltaTime;

        if (hitTimer >= 0.5f)
            HideTongue();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            hittedEnemy = other.gameObject;
            Hit();
        }
    }

    void Hit()
    {
        tongue.transform.localScale = tongueScaledScale;

        hitTimer = 0.0f;

        hittedEnemy.GetComponent<EnemyMainScript>().hp -= frogDmg;
    }

    private void HideTongue()
    {
        hitTimer = -1.0f;

        tongue.transform.localScale = tongueBaseScale;
    }
}
