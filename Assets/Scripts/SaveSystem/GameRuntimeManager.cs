using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameRuntimeManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] Vector3 playerSpawn;
    [SerializeField] List<Vector3> EnemySpawnPositons;
    [SerializeField] GameObject Terrain;
    Dictionary<string, GameObject> enemies;
    Dictionary<string, int> enemyIndexes;
    GameObject playerInstance;
    void OnEnable()
    {
        EventManager.EnemyDeathEvent += RemoveEnemy;
        enemies = new Dictionary<string, GameObject>();
        enemyIndexes = new Dictionary<string, int>();
        SpawnPlayer();
        SpawnEnemies();
        
    }

    private void RemoveEnemy(GameObject enemy)
    {
        EnemySpawnPositons.Remove(EnemySpawnPositons[enemyIndexes[enemy.name]]);
    }

    private void SpawnPlayer()
    {

        playerInstance = Instantiate(Player);
        /*        if (SaveSystem.currentSave != null)
                {
                    GameObject.Find("Player").transform.position = new Vector3(SaveSystem.currentSave.position[0], SaveSystem.currentSave.position[1], SaveSystem.currentSave.position[2]);
                    GameObject.Find("Player").transform.rotation.eulerAngles.Set(SaveSystem.currentSave.rotation[0], SaveSystem.currentSave.rotation[1], SaveSystem.currentSave.rotation[2]);
                }
                else
                {
                    GameObject.Find("Player").transform.position = playerSpawn;
                    GameObject.Find("Player").transform.rotation.eulerAngles.Set(90, 0, 0);
                }*/

        if (SaveSystem.newArea)
        {
            GameObject.Find("Player").transform.position = playerSpawn;
            GameObject.Find("Player").transform.rotation.eulerAngles.Set(90, 0, 0);
            SaveSystem.newArea = false;
        }
        else if (SaveSystem.currentSave == null)
        {
            Debug.Log("Game Continuity broken");
        }
        else{
            GameObject.Find("Player").transform.position = new Vector3(SaveSystem.currentSave.position[0], SaveSystem.currentSave.position[1], SaveSystem.currentSave.position[2]);
            GameObject.Find("Player").transform.rotation.eulerAngles.Set(SaveSystem.currentSave.rotation[0], SaveSystem.currentSave.rotation[1], SaveSystem.currentSave.rotation[2]);
        }
    }

    private void SpawnEnemies()
    {
        if (Terrain.GetComponent<NavMeshSurface>() != null)
        {
            int counter = 0;
            foreach (Vector3 pos in EnemySpawnPositons) 
            {
                GameObject tempEnemy = Instantiate(EnemyPrefab);
                tempEnemy.name = "Enemy" + counter;
                tempEnemy.transform.position = pos;
                Debug.Log(tempEnemy.transform.position);
                counter++;
                enemies.Add(tempEnemy.name, tempEnemy);
                enemyIndexes.Add(tempEnemy.name, counter);
                tempEnemy.GetComponent<NavMeshAgent>().Warp(pos);
            }

        }

    }
    private void OnDisable()
    {
        DisableGameObjects();
        EventManager.EnemyDeathEvent -= RemoveEnemy;
    }

    public void DisableGameObjects()
    {
        Time.timeScale = 1f;
        Destroy(playerInstance);
        playerInstance = null;
        foreach (var entry in enemies)
        {
            Destroy(entry.Value);
        }
    }

}

