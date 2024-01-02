using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.SceneView;

public class GameIntro : MonoBehaviour
{
    [SerializeField] private AudioClip IntroMusic;
    private AudioSource soundChannel;
    public bool _playIntroMusic;

    void Start()
    {
        soundChannel = gameObject.GetComponents<AudioSource>()[5];
        soundChannel.enabled = true;
        soundChannel.clip = IntroMusic;
    }

    public void PlayIntro()
    {
        _playIntroMusic = true;
        StartCoroutine(IntroScreen());
    }

    private IEnumerator IntroScreen()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerUI>().ShowIntroBackground();

        player.GetComponent<CameraFade>().StartFadeOutEffect();

        PlayIntroMusic();

        yield return new WaitForSeconds(4f);

        player.GetComponent<PlayerUI>().ShowIntroTextStudio();

        yield return new WaitForSeconds(3f);

        player.GetComponent<PlayerUI>().HideIntroTextStudio();

        yield return new WaitForSeconds(2f);

        player.GetComponent<PlayerUI>().ShowIntroTextPresents();

        yield return new WaitForSeconds(2f);

        player.GetComponent<PlayerUI>().HideIntroTextPresents();

        yield return new WaitForSeconds(2f);

        player.GetComponent<PlayerUI>().ShowIntroText();

        yield return new WaitForSeconds(6f);

        player.GetComponent<PlayerUI>().HideIntroText();

        player.GetComponent<CameraFade>().StartFadeInEffect();

        yield return new WaitForSeconds(3f);

        player.GetComponent<PlayerUI>().HideIntroBackground();

        StopIntroMusic();

        _playIntroMusic = false;
    }


    private void PlayIntroMusic()
    {
        soundChannel.Play();
    }

    private void StopIntroMusic()
    {
        StartCoroutine(Effects.StartFade(soundChannel, 15f, 0f));
    }

}
