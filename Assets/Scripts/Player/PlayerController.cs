using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;
using UnityEngine.VFX;


//Pithano Onoma PlayerController
public class PlayerController : MonoBehaviour, Hittable
{
    private CharacterController _player;
    public Vector3 _moveDir;
    private Vector3 _velocity;

    public float regenRate = 7f;
    /*    private float lastRegenTime =0;*/

    private float defense=1.0f;
    AudioSource MovementSound;
    AudioSource AttackSound;
    AudioSource EquipSound;
    AudioSource BreathingSound;

    AudioSource[] soundChannels;


    [Header("Controller")]
    public float _currentSpeed;
    public float _maxVelocity;
    public float _speed = 5f;
    public float _runningSpeed;
    public float _tiredSpeed;
    public float _runningStaminaConsumption;
    public float _jumpHeight = 2.5f;
    public float _gravity = -6f;
    private bool _waitForRegen;
    private bool _noStaminaEffect;

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
    [SerializeField] AudioClip tiredEffect;
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
        EventManager.CanAttackEvent += canAttack;

        HitEffect = HitEffectEmmiter.GetComponent<ParticleSystem>();
        BloodEffect = BloodEffectEmmiter.GetComponent<VisualEffect>();
        
        EventManager.EquipWeaponEvent += PlayEquipSound;
        _player = GetComponent<CharacterController>();
        PlayerMovementManager._isGrounded = true;
        PlayerMovementManager._isRunning = false;

        soundChannels = gameObject.GetComponents<AudioSource>();
        MovementSound = soundChannels[0];
        AttackSound = soundChannels[1];
        EquipSound = soundChannels[2];
        BreathingSound = soundChannels[3];
        
        MovementSound.enabled = false;
        AttackSound.enabled = false;
        EquipSound.enabled = false;
        BreathingSound.enabled = false;

        _velocity = Vector3.zero;

        _currentSpeed = 0f;
        _jumpHeight = 2.5f;
        _gravity = -8f;
        _runningSpeed = 12f;
        _tiredSpeed = 2f;
        _runningStaminaConsumption = 0.2f;

        _waitForRegen = false;
        _noStaminaEffect = false;

        _maxVelocity = _currentSpeed;
    }
    private void OnDisable()
    {
        EventManager.PlayerHitEvent -= TakeDamage;
        EventManager.EquipWeaponEvent -= PlayEquipSound;
        EventManager.TogglePause -= PauseSound;
        EventManager.CanAttackEvent -= canAttack;

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

        if (_moveDir.x == 0 && _moveDir.z == 0)
        {
            StartCoroutine(Effects.StartFade(MovementSound, 0.2f, 0.0f));
        }
        else if (PlayerMovementManager._isRunning && player.stamina > 0 && !_waitForRegen)
        {
            _currentSpeed = _runningSpeed;
            player.stamina -= _runningStaminaConsumption;
            PlaySound(runEffect);
        }
        else
        {
            if (player.stamina == 0)
            {
                PlayBreathingSound();
                _waitForRegen = true;
            }
            else if (player.stamina == player.maxStamina)
            {
                _noStaminaEffect = false;
                _waitForRegen = false;
            }

            if (_waitForRegen) _currentSpeed = _tiredSpeed;
            else
            {
                _currentSpeed = _speed;
            }
            
            PlaySound(walkEffect);
        }

        if (_waitForRegen && !_noStaminaEffect && 
           PlayerMovementManager._isRunning && (_moveDir.x != 0 || _moveDir.z != 0))
        {
            _noStaminaEffect = true;
            StartCoroutine(
                Effects.FlashStaminaEffect(
                    GameObject.Find("Fill Stamina").GetComponent<Image>(),
                    0.2f,
                    () => { _noStaminaEffect = false; } 
                )
            );
        }


        _velocity.y += _gravity * Time.deltaTime;
        if (PlayerMovementManager._isGrounded && _velocity.y < 0f)
        {
            _velocity.y = -2f;
        }

        _maxVelocity = _currentSpeed;

        if (_moveDir.x == 0)
        {
            if (Mathf.Abs(_velocity.x) > 0)
            {
                _velocity.x += (-_velocity.normalized.x) * _currentSpeed * Time.deltaTime * 8f;
            }
            else
            {
                _velocity.x = 0f;
            }
        }
        else 
        {
            if (Mathf.Abs(_velocity.x) < _maxVelocity)
            {
                _velocity.x += _moveDir.x * _currentSpeed * Time.deltaTime * 2f;
            }
            else
            {
                _velocity.x = _moveDir.x * _maxVelocity;
            }
        }

        if (_moveDir.z == 0)
        {
            if (Mathf.Abs(_velocity.z) > 0)
            {
                _velocity.z += (-_velocity.normalized.z) * _currentSpeed * Time.deltaTime * 8f;
            }
            else
            {
                _velocity.z = 0f;
            }
        }
        else 
        {
            if (Mathf.Abs(_velocity.z) < _maxVelocity)
            {
                _velocity.z += _moveDir.z * _currentSpeed * Time.deltaTime * 2f;
            }
            else
            {
                _velocity.z = _moveDir.z * _maxVelocity;
            }
        }

        //Debug.Log("Velocity: " + _velocity);

        _player.Move(transform.TransformDirection(_velocity) * Time.deltaTime);

        PlayerProfile.UpdatePosition(gameObject.transform.position);

    }

    public void canAttack()
    {
        //enable for stamina consumption while punching
        if (player.stamina > WeaponManager._currentWeapon.GetStaminaConsumption()) //&& !_waitForRegen)
        {
            //Move Attack Sound Effects to animation
            player.stamina -= WeaponManager._currentWeapon.GetStaminaConsumption();
            PlayerMovementManager.setIsStaminaEnough(true);
        }
        else
        {
            //enable for stamina consumption while punching
            //bug: no stamina consumption when player stays still and punches, need to move!
            /*
            if (_waitForRegen && !_noStaminaEffect)
            {
                _noStaminaEffect = true;
                StartCoroutine(
                    Effects.FlashStaminaEffect(
                        GameObject.Find("Fill Stamina").GetComponent<Image>(),
                        0.2f,
                        () => { _noStaminaEffect = false; }
                    )
                );
            }
            */

            PlayerMovementManager.setIsStaminaEnough(false);
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
        if (MovementSound.enabled || AttackSound.enabled || EquipSound.enabled  || BreathingSound.enabled)
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
        if (!WeaponManager._changeWeapon) 
        {
            EquipSound.clip = EquipEffect;
            EquipSound.enabled = true;
            EquipSound.Play();
        }
    }
    public void PlayImpactSound() 
    {
        EquipSound.clip = impactEffect;
        EquipSound.enabled = true;
        EquipSound.Play();
    }

    public void PlayBreathingSound()
    {
        BreathingSound.clip = tiredEffect;
        BreathingSound.enabled = true;
        BreathingSound.volume = 0.9f;
        BreathingSound.Play();
    }

    //Tha ginete meso animation
    private void PlayAttackSound()
    {
        AttackSound.clip = attackEffect;
        AttackSound.enabled = true;
        AttackSound.Play();
    }

    private void PlaySound(AudioClip clip, float volume=0.7f)
    {
        MovementSound.enabled = true;
        MovementSound.volume = volume;
        MovementSound.clip = clip;
        if (!MovementSound.isPlaying)
        {
            MovementSound.Play();
        }


    }
}
