using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysSpawner : MonoBehaviour
{
    private Vector3 spawnPos;
    private int numOfEnemysInGroup;

    int randomIndexOfSpawnpoint;

    public static int allEnemiesInWave;

    public GameObject enemy_PF;

    int waveIndex = 1;

    SoundManager soundManager;

    public Mesh[] enemyMesh = new Mesh[5];

    GameObject[] fowUndiscovered = new GameObject[500]; // for enemys spawn

    public float timer;
    public float spawnDelay = 60.0f;

    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundManager>();
        UpdateSpawnpoints();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        timer += Time.deltaTime;

        if (timer >= spawnDelay)
        {
            timer = 0.0f;
            Debug.Log("Spawning random enemy");
            */

        if(Input.GetKeyDown(KeyCode.I)) //DEBUG_ONLY
            Spawn();
        //}
    }

    void UpdateSpawnpoints()
    {

        randomIndexOfSpawnpoint = Random.Range(0, 4);

            
        spawnPos = transform.GetChild(randomIndexOfSpawnpoint).position;
    }

    void Spawn()
    {
        soundManager.GetComponent<AudioSource>().Stop();
        soundManager.PlayAudio(1);

        numOfEnemysInGroup = Random.Range(waveIndex * 2, waveIndex * 5);

        int selectedEnemy;

        while(numOfEnemysInGroup > 0)
        {
            int randEnemy = Random.Range(0, 100);

            UpdateSpawnpoints();

            float hp;
            float dmg;

            if(randEnemy < 50)
            {
                if (numOfEnemysInGroup < 1)
                    continue;

                selectedEnemy = 0;

                hp = 6.0f;
                dmg = 3.0f;

                numOfEnemysInGroup--;
            }
            else if(randEnemy < 75)
            {
                if (numOfEnemysInGroup < 3)
                    continue;

                selectedEnemy = 1;

                hp = 50.0f;
                dmg = 4.0f;

                numOfEnemysInGroup -= 3;
            }
            else if(randEnemy < 85)
            {
                if (numOfEnemysInGroup < 6)
                    continue;

                selectedEnemy = 2;

                hp = 10.0f;
                dmg = 6.0f;

                numOfEnemysInGroup -= 6;
            }
            else if(randEnemy < 95)
            {
                if (numOfEnemysInGroup < 10)
                    continue;

                selectedEnemy = 3;

                hp = 15.0f;
                dmg = 8.0f;

                numOfEnemysInGroup -= 10;
            }
            else
            {
                if (numOfEnemysInGroup < 25)
                    continue;

                selectedEnemy = 4;

                hp = 75.0f;
                dmg = 10.0f;

                numOfEnemysInGroup -= 25;
            }

            GameObject I = Instantiate(enemy_PF, spawnPos, Quaternion.identity);

            I.transform.GetChild(0).GetComponent<MeshFilter>().mesh = enemyMesh[selectedEnemy];
            I.GetComponent<EnemyMainScript>().hp = hp;
            I.GetComponent<EnemyMainScript>().dmg = dmg;

            allEnemiesInWave++;
        }

        waveIndex++;
    }
}
