using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFX
{
    private ParticleSystem hitEffect;
    private VisualEffect bloodEffect;
    private GameObject hitEffectEmmiter;
    private GameObject bloodEffectEmmiter;

    public PlayerVFX(GameObject hitEffectEmmiter, GameObject bloodEffectEmmiter) 
    {
        this.hitEffectEmmiter = hitEffectEmmiter;
        this.bloodEffectEmmiter = bloodEffectEmmiter;
        this.hitEffect = hitEffectEmmiter.GetComponent<ParticleSystem>();
        this.bloodEffect = bloodEffectEmmiter.GetComponent<VisualEffect>();
    }

    public void PlayAttackVFX(Transform hittableTransform) 
    {
        hitEffectEmmiter.transform.position = hittableTransform.position + new Vector3(0, 0.5f, 0);
        hitEffect.Stop(); hitEffect.Play();
        bloodEffectEmmiter.transform.position = hittableTransform.position + new Vector3(0, 0.5f, 0);
        bloodEffectEmmiter.transform.rotation = hittableTransform.rotation;
        bloodEffect.Stop(); bloodEffect.Play();
    }
}
