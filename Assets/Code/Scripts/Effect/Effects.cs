using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public static class Effects
{
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
    public static IEnumerator StartFade(UnityEngine.UI.Image img, float duration, float targetVolume)
    {
        float currentTime = 0;
        UnityEngine.Color start = img.color;
/*        float start = color;*/
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            start.a = Mathf.Lerp(start.a, targetVolume, Mathf.Clamp01(currentTime / duration));
            img.color = start;
            yield return null;
        }
        yield break;
    }


    public static IEnumerator StartFadeWithFunction(UnityEngine.UI.Image img, float duration, float targetVolume,Action method)
    {
        float currentTime = 0;
        UnityEngine.Color start = img.color;
        /*        float start = color;*/
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            start.a = Mathf.Lerp(start.a, targetVolume, Mathf.Clamp01(currentTime / duration) * 0.1f);
            img.color = start;
            yield return null;
        }
        method.Invoke();
        yield break;
    }
    public static IEnumerator Flash(SpriteRenderer sprite, float duration) 
    {
        float currentTime = 0;
        Material originalMaterial = sprite.material;
        Material flashMaterial = UnityEngine.Object.Instantiate(sprite.material);
        Color flashColor = flashMaterial.color * 4;
        flashMaterial.color = flashColor;
        bool flash = true;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (flash)
            {
                sprite.material= flashMaterial;
                flash = false;
            }
            else 
            {
                sprite.material = originalMaterial;
                flash = true;
            }
            yield return null;
        }
        sprite.material = originalMaterial;
        yield break;
    }
    
    public static IEnumerator FlashStaminaEffect(UnityEngine.UI.Image image, float duration, Action callback) 
    {
        float currentTime = 0;
        Color originalColor = image.color;
        Color flashColor = Color.red;
        bool flash = true;
        //int counter = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (flash)
            {
                image.color = flashColor;
                flash = false;
                //counter++;
            }
            else //if (counter > 1)
            {
                image.color = originalColor;
                flash = true;
            }
            yield return null;
        }

        image.color = originalColor;

        callback();

        yield break;
    }

}
