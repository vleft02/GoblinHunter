using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GameRuntimeManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] List<GameObject> EnemyPrefabs;
    [SerializeField] GameObject Terrain;
    [SerializeField] bool ProceduralGen;
    Dictionary<string, GameObject> enemies;
    GameObject playerInstance;
    private AudioSource audioSource;
    private bool musicStopped;

    void OnEnable()
    {
        enemies = new Dictionary<string, GameObject>();
        if (!ProceduralGen) 
        {
            Spawn();
        }
  
    }

    private void OnDisable()
    {
        DisableGameObjects();
    }

    public void Spawn()
    {
        SpawnPlayer();
        SpawnEnemies();
        DisableGore();
        SetVolume();
        PlayMusic();
    }

    private void SetVolume()
    {

        List<GameObject> audioSourceHolderList = GameObject.FindGameObjectsWithTag("AudioPlayer").ToList();
        audioSourceHolderList.Add(GameObject.Find("Player"));
        foreach (GameObject audioSourceHolder in audioSourceHolderList) 
        {
            AudioSource[] audioSources = audioSourceHolder.GetComponents<AudioSource>();
            foreach (AudioSource audioSource in audioSources) 
            {
                audioSource.volume = PlayerProfile.GetVolume();
            }
        }
    }


    private void SpawnPlayer()
    {
        Vector3 playerPos = new Vector3(PlayerProfile.gameData.currentArea.GetPlayerPosition()[0], PlayerProfile.gameData.currentArea.GetPlayerPosition()[1], PlayerProfile.gameData.currentArea.GetPlayerPosition()[2]);
        Vector3 playerRotation = new Vector3(PlayerProfile.gameData.currentArea.GetPlayerRotation()[0], PlayerProfile.gameData.currentArea.GetPlayerRotation()[1], PlayerProfile.gameData.currentArea.GetPlayerRotation()[2]);

        playerInstance = Instantiate(Player,playerPos,Quaternion.identity);
        GameObject.Find("Player").transform.localPosition = Vector3.zero;
        GameObject.Find("Player").transform.localRotation = Quaternion.Euler(playerRotation);
    }

    private void SpawnEnemies()
    {

                if (PlayerProfile.gameData.currentArea.GetEnemies() != null && PlayerProfile.gameData.currentArea.GetEnemies().Count != 0)
                {
                    foreach (EnemyData enemy in PlayerProfile.gameData.currentArea.GetEnemies())
                    {
                        GameObject tempEnemy = Instantiate(EnemyPrefabs[enemy.GetEnemyType()]);
                        tempEnemy.name = enemy.GetName();
                        Vector3 spawnPos = new Vector3(enemy.GetEnemyPosition()[0], enemy.GetEnemyPosition()[1], enemy.GetEnemyPosition()[2]);
                        tempEnemy.GetComponent<NavMeshAgent>().Warp(spawnPos);
                        tempEnemy.transform.position = spawnPos;
                        enemies.Add(tempEnemy.name, tempEnemy);
                    }
                }

    }

    private void Update()
    {
        if (GameObject.Find("Player").GetComponent<GameIntro>()._playIntroMusic &&
            !musicStopped)
        {
            musicStopped = true;
            audioSource.Stop();
        }

        if (musicStopped && !GameObject.Find("Player").GetComponent<GameIntro>()._playIntroMusic)
        {
            audioSource.Play();
        }

    }

    private void PlayMusic()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
        musicStopped = false;
    }

    private void DisableGore() 
    {
        if (!PlayerProfile.GetGoreEnabled()) 
        {
            if (GameObject.Find("Gore") != null) 
            {
                GameObject.Find("Gore").SetActive(false);
            }
        }
    }

    public void DisableGameObjects()
    {
        Destroy(playerInstance);
        playerInstance = null;
        foreach (var entry in enemies)
        {
            Destroy(entry.Value);
        }
    }


}

