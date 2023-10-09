using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Windows;
using static UnityEditor.Experimental.GraphView.GraphView;

public enum PlayerWeapon
{
    HANDS, SWORD, AXE, SHIELD, DAGGERS
}

[RequireComponent(typeof(CharacterController), typeof(PlayerMove), typeof(PlayerRotate))]
public class Player : MonoBehaviour
{

    public Weapon _weapon;
    public bool _instantiateWeapon = false;
    public PlayerWeapon _currentWeapon;
    public bool _changeWeapon = false;

    public PlayerInput _playerInput;
    public PlayerInput.OnFootActions _onFoot;

    private PlayerMove _playerMove;
    private PlayerRotate _rotate;
    private PlayerRotate _rotateSmooth;
    private PlayerRotate _currentRotate;

    [SerializeField] public GameObject _weaponHolder;


    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _currentWeapon = PlayerWeapon.HANDS;

        _playerInput = new PlayerInput();
        _onFoot = _playerInput.OnFoot;
        _playerMove = GetComponent<PlayerMove>();
        _rotate = GetComponents<PlayerRotate>()[0];
        _rotateSmooth = GetComponents<PlayerRotate>()[1];
#if UNITY_EDITOR
        _currentRotate = _rotate;
#else
        _currentRotate = _rotateSmooth;
#endif

        AssignInputs();

    }

    private void Update()
    {
        //_rotate.Rotate(_onFoot.Look.ReadValue<Vector2>());
        _playerMove.Move(_onFoot.Movement.ReadValue<Vector2>());

        if (_onFoot.Punch.triggered)
        {
            _playerMove.Attack();
        }

    }

    private void LateUpdate()
    {
        _rotate.Rotate(_onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        _onFoot.Enable();
    }

    private void OnDisable()
    {
        _onFoot.Disable();
    }

    private void AssignInputs()
    {
        _onFoot.Jump.performed += ctx => _playerMove.Jump();

        _onFoot.Run.performed += ctx => _playerMove._isRunning = true;
        _onFoot.Run.canceled += ctx => _playerMove._isRunning = false;



    }
    public void WeaponPickUp(Weapon weapon)
    {
        _currentWeapon = weapon.GetWeapon();
        _weapon = weapon;
/*      _weapon.transform.SetParent(_weaponHolder.transform);*/
        
        //Each Weapon initializes transforms
        //gets equipped in to the appropriate weapon Holder
        //Unequips Previous Weapon
        _weapon.InitializeWeapon();
       
/*      _weapon.transform.localPosition = Vector3.zero;
        _weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        _weapon.transform.localScale = Vector3.one;*/
    }

}
