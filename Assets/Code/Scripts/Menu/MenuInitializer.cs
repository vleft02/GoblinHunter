using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuInitializer : MonoBehaviour
{
    [SerializeField] GameObject startingMenuPanel;
        
    void Start()
    {
        startingMenuPanel.SetActive(true);
        UpdateVolume();
    }

    public void UpdateVolume() 
    {
        List<GameObject> audioSourceHolderList = GameObject.FindGameObjectsWithTag("AudioPlayer").ToList();
        foreach (GameObject audioSourceHolder in audioSourceHolderList)
        {
            AudioSource[] audioSources = audioSourceHolder.GetComponents<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.volume = PlayerProfile.GetVolume();
            }
        }
    }
}
