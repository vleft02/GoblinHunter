using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRuntimeManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] int EnemyCount;
    [SerializeField] GameObject Terrain;
    List<GameObject> enemies;
    GameObject playerInstance;
    void OnEnable()
    {
        enemies = new List<GameObject>();
        SpawnPlayer();

        SpawnEnemies();
        
    }

    private void SpawnPlayer()
    {
        playerInstance = Instantiate(Player);
        if (SaveSystem.currentSave != null)
        {
            GameObject.Find("Player").transform.position = new Vector3(SaveSystem.currentSave.position[0], SaveSystem.currentSave.position[1], SaveSystem.currentSave.position[2]);
            GameObject.Find("Player").transform.rotation.eulerAngles.Set(SaveSystem.currentSave.rotation[0], SaveSystem.currentSave.rotation[1], SaveSystem.currentSave.rotation[2]);
        }
        else
        {
            GameObject.Find("Player").transform.position = new Vector3(3, 0, 3);
            GameObject.Find("Player").transform.rotation.eulerAngles.Set(90, 0, 0);
        }
    }

    private void SpawnEnemies()
    {
        if (Terrain.GetComponent<NavMeshSurface>() != null)
        {
            int counter = 0;
            while (counter <= EnemyCount)
            {
                GameObject tempEnemy = Instantiate(EnemyPrefab);
                tempEnemy.name = "Enemy" + counter;
                /*            tempEnemy.transform.position = new Vector3(Random.value * 5, 2, Random.value * 5);*/
                /*            tempEnemy.transform.position = new Vector3(0, 0, 0);*/
                enemies.Add(tempEnemy);
                counter++;
            }
        }

    }
    private void OnDisable()
    {
        DisableGameObjects();
    }

    public void DisableGameObjects()
    {
        Time.timeScale = 1f;
        Destroy(playerInstance);
        playerInstance = null;
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

}

