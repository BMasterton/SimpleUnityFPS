using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    //private GameObject enemy;
    private Vector3 spawnPoint = new Vector3(0, 0, 5);
    private int enemyAmount = 5;
    private GameObject[] enemies = new GameObject[5];
    // Update is called once per frame


   //private void Start()
    //{
    //    for (int i = 0; i < enemyAmount; i++) { 
    //        enemies[i] = enemy ;
        
    //    }
        
    //}
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
            }
        }
    }
}
