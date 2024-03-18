using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private UIController ui;
    private int score = 0;

    [SerializeField]
    private GameObject iguana;

    [SerializeField] private Transform iguanaSpawnPt;

    [SerializeField]
    private GameObject enemyPrefab;
    //private GameObject enemy;
    private Vector3 spawnPoint = new Vector3(0, 0, 5);
    private int enemyAmount = 5;
    private int iguanaAmount = 5;
    private GameObject[] enemies = new GameObject[5];
    private GameObject[] iguanas = new GameObject[5];
    // Update is called once per frame


    private void Start()
    {

        ui.UpdateScore(score);
        for (int i = 0; i < iguanaAmount; i++)
        {
            iguanas[i] = Instantiate(iguana) as GameObject;
            iguanas[i].transform.position = iguanaSpawnPt.position;
            float angle = Random.Range(0, 360);
            iguanas[i].transform.Rotate(0, angle, 0);
            
        }
    }
    void Update()
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            if (enemies[i] == null)
            {
                enemies[i] = Instantiate(enemyPrefab) as GameObject;
                
                enemies[i].transform.position = spawnPoint;
               
                
                float angle = Random.Range(0, 360);
                enemies[i].transform.Rotate(0, angle, 0);
                WanderingAI ai = enemies[i].GetComponent<WanderingAI>();
                ai.SetDifficulty(GetDifficulty());
            }
        }
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
        Messenger<int>.AddListener(GameEvent.DIFFICULTY_CHANGED, OnDifficultyChanged);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
        Messenger<int>.RemoveListener(GameEvent.DIFFICULTY_CHANGED, OnDifficultyChanged);
    }
   
    private void OnEnemyDead()
    {
        score++;
        ui.UpdateScore(score);
    }

    private void OnDifficultyChanged(int newDifficulty)
    {
        Debug.Log("Scene.OnDifficultyChanged(" + newDifficulty + ")");
        for (int i = 0; i < enemies.Length; i++)
        {
            WanderingAI ai = enemies[i].GetComponent<WanderingAI>();
            ai.SetDifficulty(newDifficulty);
        }
    }

    public int GetDifficulty()
    {
        return PlayerPrefs.GetInt("difficulty", 1);
    }

}
