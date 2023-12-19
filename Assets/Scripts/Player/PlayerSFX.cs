using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerSFX
{
    private AudioSource MovementSound;
    private AudioSource AttackSound;
    private AudioSource EquipSound;
    private AudioSource BreathingSound;
    private AudioSource PlayerHurtSound;

    public PlayerSFX(AudioSource movementSoundSource, AudioSource attackSoundSource, AudioSource equipSoundSource, AudioSource breathingSoundSource,AudioSource playerHurtSoundSource)
    {
        MovementSound = movementSoundSource;
        MovementSound.enabled = false;
        AttackSound = attackSoundSource;
        AttackSound.enabled = false;
        EquipSound = equipSoundSource;
        EquipSound.enabled = false;
        BreathingSound = breathingSoundSource;
        BreathingSound.enabled = false;
        PlayerHurtSound = playerHurtSoundSource;
        PlayerHurtSound.enabled = false;
    }

    public void Initialize(AudioSource movementSoundSource, AudioSource attackSoundSource, AudioSource equipSoundSource, AudioSource breathingSoundSource)
    {
        MovementSound = movementSoundSource;
        MovementSound.enabled = false;
        AttackSound = attackSoundSource;
        AttackSound.enabled = false;
        EquipSound = equipSoundSource;
        EquipSound.enabled = false;
        BreathingSound = breathingSoundSource;
        BreathingSound.enabled = false;
    }

    public AudioSource GetMovementSound()
    {
        return MovementSound;
    }

    public void TogglePauseSounds() 
    {
        if (MovementSound.enabled || AttackSound.enabled || EquipSound.enabled || BreathingSound.enabled)
        {
            MovementSound.enabled = false;
            AttackSound.enabled = false;
            EquipSound.enabled = false;
            BreathingSound.enabled = false;
        }
        else
        {
            MovementSound.enabled = true;
        }
    }

    public void PlayFromEquipSound(AudioClip clip) 
    {
        EquipSound.clip = clip;
        EquipSound.enabled = true;
        EquipSound.Play();
    }
    public void PlayFromAttackSound(AudioClip clip)
    {
        AttackSound.clip = clip;
        AttackSound.enabled = true;
        AttackSound.Play();
    }
    public void PlayFromMovementSound(AudioClip clip,float volume)
    {
        MovementSound.enabled = true;
        MovementSound.volume = volume;
        MovementSound.clip = clip;
        if (!MovementSound.isPlaying)
        {
            MovementSound.Play();
        }
    }
    public void PlayFromBreathingSound(AudioClip clip)
    {
        BreathingSound.clip = clip;
        BreathingSound.enabled = true;
        BreathingSound.volume = 0.9f;
        BreathingSound.Play();
    }
    public void PlayFromHurtSound(AudioClip clip)
    {
        PlayerHurtSound.enabled = true;
        PlayerHurtSound.clip = clip;
        PlayerHurtSound.Play();
    }


}

