using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerController), typeof(PlayerRotate))]
public class Player : MonoBehaviour
{
    public bool _instantiateWeapon = false;
    public PlayerWeapon _currentWeapon;
    public bool _changeWeapon = false;
    
    public DefaultInputActions uiInputActions;
    public PlayerInput _playerInput;
    public PlayerInput.OnFootActions _onFoot;
    public bool isPaused;
    public bool isDead;
    public PlayerController _playerController;
    private PlayerRotate _rotate;
    private PlayerRotate _rotateSmooth;
    private PlayerRotate _currentRotate;

    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        isDead = false;
       /*
        
        _currentWeapon = PlayerWeapon.HANDS;
*/

        PlayerProfile.UpdateCurrentArea();

        uiInputActions = new DefaultInputActions();
        _playerInput = new PlayerInput();
        _onFoot = _playerInput.OnFoot;
        _playerController = GetComponent<PlayerController>();
        _rotate = GetComponents<PlayerRotate>()[0];
        /*_rotateSmooth = GetComponents<PlayerRotate>()[1];*/

        _currentRotate = _rotate;
/*#if UNITY_EDITOR
        _currentRotate = _rotate;
#else
        _currentRotate = _rotateSmooth;
#endif*/

        AssignInputs();

    }

    private void OnEnable()
    {
       
        _onFoot.Enable();


        WeaponManager.InitWeapons(PlayerProfile.gameData.playerData.weapons, PlayerProfile.gameData.playerData.currentWeapon);
        _currentWeapon = WeaponManager._currentWeaponKey;
        EventManager.TogglePause += ChangeInput;
        EventManager.ToggleEquipMenu += ChangeInput;
        EventManager.PlayerDeathEvent += TerminateInput;
        EventManager.BossDefeatedEvent += TerminateInput;
        
    }

    private void Update()
    {
        _playerController.Move(_onFoot.Movement.ReadValue<Vector2>());

        _playerController.ReplenishStamina();

    }

    private void LateUpdate()
    {
        _currentRotate.Rotate(_onFoot.Look.ReadValue<Vector2>());

        _currentRotate.CameraTilt(_onFoot.Movement.ReadValue<Vector2>());

        _currentRotate.RotateToAngles();

    }


    private void OnDisable()
    {
        _onFoot.Disable();
        _onFoot.Jump.performed -= ctx => _playerController.Jump();
        _onFoot.QuickSave.performed -= ctx => SaveManager.SaveGame();
        _onFoot.Pause.performed -= ctx => EventManager.PauseEvent();
        _onFoot.EquipMenu.performed -= ctx => EventManager.EquipMenuEvent();
        _onFoot.Run.performed -= ctx => _playerController._isRunning = true;
        _onFoot.Run.canceled -= ctx => _playerController._isRunning = false;
        _onFoot.WeaponSlot1.performed -= ctx => WeaponManager.ChangeWeapon(1);
        _onFoot.WeaponSlot2.performed -= ctx => WeaponManager.ChangeWeapon(2);
        _onFoot.WeaponSlot3.performed -= ctx => WeaponManager.ChangeWeapon(3);
        _onFoot.WeaponSlot4.performed -= ctx => WeaponManager.ChangeWeapon(4);

        EventManager.TogglePause -= ChangeInput;
        EventManager.ToggleEquipMenu -= ChangeInput;
        EventManager.BossDefeatedEvent -= TerminateInput;
    }

    private void ChangeInput()
    {
        if (!isDead)
        {
            if (isPaused)
            {
                _onFoot.Attack.Enable();
                _onFoot.Movement.Enable();
                _onFoot.Interact.Enable();
                _onFoot.QuickSave.Enable();
                _onFoot.WeaponSlot1.Enable();
                _onFoot.WeaponSlot2.Enable();
                _onFoot.WeaponSlot3.Enable();
                _onFoot.WeaponSlot4.Enable();
/*                _onFoot.EquipMenu.Enable();*/
                DefaultInputActions.UIActions uiActions = uiInputActions.UI;
                uiActions.Disable();
            }
            else
            {
                _onFoot.Attack.Disable();
                _onFoot.Movement.Disable();
                _onFoot.Interact.Disable();
                _onFoot.QuickSave.Disable();
                _onFoot.WeaponSlot1.Disable();
                _onFoot.WeaponSlot2.Disable();
                _onFoot.WeaponSlot3.Disable();
                _onFoot.WeaponSlot4.Disable();
/*                _onFoot.EquipMenu.Disable();*/
                DefaultInputActions.UIActions uiActions = uiInputActions.UI;
                uiActions.Enable();
            }
            isPaused = !isPaused;

        }

    }

    public void CameraTransition(float wait_time)
    {
        StartCoroutine(TransitionAndWait(wait_time));
    }

    public IEnumerator TransitionAndWait(float seconds)
    {

        GameToCutSceneTransition();

        yield return new WaitForSeconds(seconds);

        CutSceneToGameTransition();

    }

    public void GameToCutSceneTransition()
    {
        _onFoot.Attack.Disable();
        _onFoot.Movement.Disable();
        _onFoot.Interact.Disable();
        _onFoot.QuickSave.Disable();
        _onFoot.WeaponSlot1.Disable();
        _onFoot.WeaponSlot2.Disable();
        _onFoot.WeaponSlot3.Disable();
        _onFoot.WeaponSlot4.Disable();
        _onFoot.EquipMenu.Disable();
        _onFoot.Pause.Disable();
        _onFoot.Look.Disable();
        _onFoot.Jump.Disable();
    }

    public void CutSceneToGameTransition()
    {
        _onFoot.Attack.Enable();
        _onFoot.Movement.Enable();
        _onFoot.Interact.Enable();
        _onFoot.QuickSave.Enable();
        _onFoot.WeaponSlot1.Enable();
        _onFoot.WeaponSlot2.Enable();
        _onFoot.WeaponSlot3.Enable();
        _onFoot.WeaponSlot4.Enable();
        _onFoot.EquipMenu.Enable();
        _onFoot.Pause.Enable();
        _onFoot.Look.Enable();
        _onFoot.Jump.Enable();
    }

    private void TerminateInput()
    {
        isDead = true;
        GameToCutSceneTransition();
        DefaultInputActions.UIActions uiActions = uiInputActions.UI;
        uiActions.Enable();

    }
    private void AssignInputs()
    {
        _onFoot.Jump.performed += ctx => _playerController.Jump();
        _onFoot.QuickSave.performed += ctx => SaveManager.SaveGame();
        _onFoot.Pause.performed += ctx => EventManager.PauseEvent();
        _onFoot.EquipMenu.performed += ctx => EventManager.EquipMenuEvent();
        _onFoot.Run.performed += ctx => _playerController._isRunning = true;
        _onFoot.Run.canceled += ctx => _playerController._isRunning = false;
        _onFoot.WeaponSlot1.performed += ctx => WeaponManager.ChangeWeapon(1);
        _onFoot.WeaponSlot2.performed += ctx => WeaponManager.ChangeWeapon(2);
        _onFoot.WeaponSlot3.performed += ctx => WeaponManager.ChangeWeapon(3);
        _onFoot.WeaponSlot4.performed += ctx => WeaponManager.ChangeWeapon(4);

    }


 



}
