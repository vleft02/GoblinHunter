using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;


//Pithano Onoma PlayerController
public class PlayerController : MonoBehaviour, Hittable
{
    private CharacterController _player;
    public Vector3 _moveDir;
    private Vector3 _velocity;

    private float regenRate = 5;
    /*    private float lastRegenTime =0;*/

    private float defense=1.0f;
    AudioSource MovementSound;
    AudioSource AttackSound;
    AudioSource EquipSound;

    AudioSource[] soundChannels;


    [Header("Controller")]
    [SerializeField] private float _speed = 5f;
    public float _runningSpeed;
    public float _jumpHeight = 2.5f;
    public float _gravity = -6f;

    public PlayerInput _playerInput;
    public PlayerInput.OnFootActions _onFoot;
    public ParticleSystem HitEffect;
    public VisualEffect BloodEffect;
    public GameObject HitEffectEmmiter;
    public GameObject BloodEffectEmmiter;
    public PlayerLogic player;
    [Header("Sound Effects")]
    [SerializeField] AudioClip walkEffect;
    [SerializeField] AudioClip runEffect;
    [SerializeField] AudioClip attackEffect;
    [SerializeField] AudioClip EquipEffect;
    [SerializeField] AudioClip impactEffect;
    AudioClip currentClip;

    public void Awake()
    {
        _player = GetComponent<CharacterController>();
        EventManager.PlayerHitEvent += TakeDamage;

    }

    public void Start()
    {

        player = new PlayerLogic();
        player.health = PlayerProfile.gameData.playerData.health;

        EventManager.TogglePause += PauseSound;

        HitEffect = HitEffectEmmiter.GetComponent<ParticleSystem>();
        BloodEffect = BloodEffectEmmiter.GetComponent<VisualEffect>();
        EventManager.AttackEvent += Attack;
        
        EventManager.EquipWeaponEvent += PlayEquipSound;
        _player = GetComponent<CharacterController>();
        PlayerMovementManager._isGrounded = true;
        PlayerMovementManager._isRunning = false;

        soundChannels = gameObject.GetComponents<AudioSource>();
        MovementSound = soundChannels[0];
        AttackSound = soundChannels[1];
        EquipSound = soundChannels[2];
        
        MovementSound.enabled = false;
        AttackSound.enabled = false;
        EquipSound.enabled = false;

        _jumpHeight = 2.5f;
        _gravity = -8f;
        _runningSpeed = 12f;
    }
    private void OnDisable()
    {
        EventManager.PlayerHitEvent -= TakeDamage;
        EventManager.EquipWeaponEvent -= PlayEquipSound;
        EventManager.TogglePause -= PauseSound;
        EventManager.AttackEvent -= Attack;
    }
    void Update()
    {
        /*       _playerController.Move(_onFoot.Movement.ReadValue<Vector2>());
               _playerController.ReplenishStamina();
               if (PlayerMovementManager._isGrounded)
               {
                   if (_onFoot.Attack.triggered)
                   {
                       if (PlayerMovementManager.CanAttack())
                       {
                          PlayerMovementManager.AttackPerformed(WeaponManager._currentWeapon.ATTACK.Duration);
                          _playerController.Attack();
                       }
                   }
               }*/

        PlayerMovementManager._isGrounded = _player.isGrounded;
    }

    public virtual void Move(Vector2 input)
    {
        _moveDir.x = input.x;
        _moveDir.z = input.y;
        _moveDir.y = _velocity.y;
        float _movementSpeed = 1f;

        if (_moveDir.x == 0 && _moveDir.z == 0)
        {
            StartCoroutine(Effects.StartFade(MovementSound, 0.2f, 0.0f));

        }
        else if (PlayerMovementManager._isRunning)
        {
            _movementSpeed = _runningSpeed;
            PlaySound(runEffect);
        }
        else
        {
            _movementSpeed = _speed;
            PlaySound(walkEffect);
        }

        _velocity.y += _gravity * Time.deltaTime;
        if (PlayerMovementManager._isGrounded && _velocity.y < 0f)
        {
            _velocity.y = -2f;
        }

        _moveDir.x *= _movementSpeed;
        _moveDir.z *= _movementSpeed;
        _moveDir.y *= _speed;

        _player.Move(transform.TransformDirection(_moveDir) * Time.deltaTime);

        PlayerProfile.UpdatePosition(gameObject.transform.position);

        /*Debug.Log("velocity.y: " + _velocity.y);*/
    }


    public void Attack()
    {
        if (player.stamina > WeaponManager._currentWeapon.GetStaminaConsumption())
        {
            //Move Attack Sound Effects to animation
            player.stamina -= WeaponManager._currentWeapon.GetStaminaConsumption();
        }

    }


    public void Jump()
    {
        if (PlayerMovementManager._isGrounded)
        {
            _velocity.y = Mathf.Sqrt((-0.4f) * _gravity * _jumpHeight);
        }
    }

    public void TakeDamage(float amount)
    {
        if (player.health > amount)
        {
            Debug.Log("Health Before: " + player.health);
            player.health -= amount*defense;
            PlayerProfile.UpdatePlayerHealth(player.health);
        }
        else
        {
            PlayerProfile.UpdatePlayerHealth(player.health);
            player.health = 0;
            EventManager.PlayerDeath();
        }
        Debug.Log("Health After: " + player.health);
    }

    public bool HasZeroHealth()
    {
        return player.health == 0;
    }

    
    public void ReplenishStamina() 
    {
        if (player.stamina<player.maxStamina) 
        {
            //frame rate independent stamina regen
            player.stamina = Mathf.Clamp(player.stamina + Time.deltaTime*regenRate , 0, player.maxStamina);
        }
    }

    public void PauseSound()
    {
        if (MovementSound.enabled || AttackSound.enabled || EquipSound.enabled)
        {
            MovementSound.enabled = false;
            AttackSound.enabled = false;
            EquipSound.enabled = false;
        }
        else 
        {
            MovementSound.enabled = true;
        }
    }

    //metafora sto player Interact isos i ylopoihsh se kapoio manager
    public void PlayVFX(Transform HittableTransform)
    {
        HitEffectEmmiter.transform.position = HittableTransform.position+ new Vector3(0,0.5f,0);
        HitEffect.Stop(); HitEffect.Play();
        BloodEffectEmmiter.transform.position = HittableTransform.position+ new Vector3(0, 0.5f, 0);
        BloodEffectEmmiter.transform.rotation = HittableTransform.rotation;
        BloodEffect.Stop(); BloodEffect.Play();
    }

    public void PlayEquipSound() 
    {
        EquipSound.clip = EquipEffect;
        EquipSound.enabled = true;
        EquipSound.Play();
    }
    public void PlayImpactSound() 
    {
        EquipSound.clip = impactEffect;
        EquipSound.enabled = true;
        EquipSound.Play();
    }

    //Tha ginete meso animation
    private void PlayAttackSound()
    {
        AttackSound.clip = attackEffect;
        AttackSound.enabled = true;
        AttackSound.Play();
    }

    private void PlaySound(AudioClip clip)
    {
        MovementSound.enabled = true;
        MovementSound.volume = 0.7f;
        MovementSound.clip = clip;
        if (!MovementSound.isPlaying)
        {
            MovementSound.Play();
        }


    }
}
