using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour, Hittable
{
    private CharacterController _player;
    public Vector3 _moveDir;
    private Vector3 _velocity;

    public float regenRate = 7f;

    private float defense=1.0f;

    PlayerSFX playerSFX;
    PlayerVFX playerVFX;
    public PlayerLogic player;

    private static float _attackCooldown = 0;
    private static float _timer = 0;

    [Header("Controller")]
    public float _currentSpeed;
    public float _maxVelocity;
    public float _speed = 5f;
    public float _runningSpeed;
    public float _tiredSpeed;
    public float _staminaConsumptionRate;
    public float _jumpHeight = 2.5f;
    public float _gravity = -6f;
    private bool _waitForRegen;
    private bool _noStaminaEffect;

    public bool _isGrounded;
    public bool _isRunning;

    public PlayerInput _playerInput;
    public PlayerInput.OnFootActions _onFoot;
    
    [Header("Visual Effects")]
    public GameObject HitEffectEmmiter;
    public GameObject BloodEffectEmmiter;
    
    
    [Header("Sound Effects")]
    [SerializeField] AudioClip walkEffect;
    [SerializeField] AudioClip runEffect;
    [SerializeField] AudioClip attackEffect;
    [SerializeField] AudioClip EquipEffect;
    [SerializeField] AudioClip impactEffect;
    [SerializeField] AudioClip tiredEffect;
    [SerializeField] AudioClip HurtEffect;

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

        playerVFX = new PlayerVFX(HitEffectEmmiter, BloodEffectEmmiter);

        EventManager.EquipWeaponEvent += PlayEquipSound;
        _player = GetComponent<CharacterController>();
        
        AudioSource[] soundChannels = gameObject.GetComponents<AudioSource>();
        playerSFX = new PlayerSFX(soundChannels[0], soundChannels[1], soundChannels[2], soundChannels[3], soundChannels[4]);

        _velocity = Vector3.zero;

        _currentSpeed = 0f;
        _jumpHeight = 10f;
        _gravity = -20f;
        _runningSpeed = 12f;
        _tiredSpeed = 2f;
        _staminaConsumptionRate = 12f;
        _waitForRegen = false;
        _noStaminaEffect = false;

        regenRate = 8f;

        _maxVelocity = _currentSpeed;

        _isGrounded = true;
        _isRunning = false;
    }
    private void OnDisable()
    {
        EventManager.PlayerHitEvent -= TakeDamage;
        EventManager.EquipWeaponEvent -= PlayEquipSound;
        EventManager.TogglePause -= PauseSound;
    }

    void Update()
    {
        _isGrounded = _player.isGrounded;
        _timer += Time.deltaTime;
    }

    public virtual void Move(Vector2 input)
    {
        _moveDir.x = input.x;
        _moveDir.z = input.y;
        _moveDir.y = _velocity.y;

        if (_moveDir.x == 0 && _moveDir.z == 0)
        {
            FadeOutMovementSound();
        }
        else if (_isRunning && player.stamina > 0 && !_waitForRegen)
        {
            _currentSpeed = _runningSpeed;
            player.stamina = Mathf.Clamp(player.stamina - Time.deltaTime * _staminaConsumptionRate, -1f, player.maxStamina);
            PlayMovementSound(runEffect);
        }
        else
        {
            if (player.stamina <= 0)
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
            
            PlayMovementSound(walkEffect);
        }

        if (_waitForRegen && !_noStaminaEffect && 
           _isRunning && (_moveDir.x != 0 || _moveDir.z != 0))
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
        if (_isGrounded && _velocity.y < 0f)
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


        _player.Move(transform.TransformDirection(_velocity) * Time.deltaTime);

        PlayerProfile.UpdatePosition(gameObject.transform.position);

    }

    public bool canAttack()
    {

        //enable for stamina consumption while punching
        if (_timer >= _attackCooldown && 
            player.stamina > WeaponManager._currentWeapon.GetStaminaConsumption()) //&& !_waitForRegen)
        {
            _attackCooldown = 0;
            //Move Attack Sound Effects to animation
            player.stamina -= WeaponManager._currentWeapon.GetStaminaConsumption();

            _timer = 0;
            _attackCooldown = WeaponManager._currentWeapon.ATTACK.Duration + 0.1f;

            return true;
        }
        else
        {
            return false;
        }

    }

    public void Jump()
    {
        if (_isGrounded)
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
            playerSFX.PlayFromHurtSound(HurtEffect);
        }
        else
        {
            PlayerProfile.UpdatePlayerHealth(player.health);
            player.health = 0;
            EventManager.PlayerDeath();
        }
        Debug.Log("Health After: " + player.health);
    }

    public void Heal(int healingAmount)
    {
        player.health = Mathf.Clamp(player.health + healingAmount, 0, 100);
    }


    
    public void ReplenishStamina()
    {
        if (player.stamina<player.maxStamina) 
        {
            player.stamina = Mathf.Clamp(player.stamina + Time.deltaTime*regenRate , 0, player.maxStamina);
        }
    }

    public void PauseSound()
    {
        playerSFX.TogglePauseSounds();
    }

    public void PlayVFX(Transform HittableTransform)
    {
        playerVFX.PlayAttackVFX(HittableTransform);
    }

    public void PlayEquipSound() 
    {
        playerSFX.PlayFromEquipSound(EquipEffect);
    }
    public void PlayImpactSound() 
    {
        playerSFX.PlayFromEquipSound(impactEffect);
    }

    public void PlayBreathingSound()
    {
        playerSFX.PlayFromBreathingSound(tiredEffect);
    }

    private void PlayAttackSound()
    {
        playerSFX.PlayFromAttackSound(attackEffect);
    }

    private void PlayMovementSound(AudioClip clip, float volume=0.7f)
    { 
        playerSFX.PlayFromMovementSound(clip,volume);
    }

    public void FadeOutMovementSound() 
    {
        StartCoroutine(Effects.StartFade(playerSFX.GetMovementSound(), 0.2f, 0.0f));
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.collider.GetComponent<Interactable>() != null)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable.useEvents && !interactable.triggerEvent)
            {
                interactable.triggerEvent = true;
                interactable.Interact();
                interactable.GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Collide!!!");
            }
        }
    }

 
}
