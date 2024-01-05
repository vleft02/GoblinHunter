using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraHolder;

    [SerializeField]
    private float rayMaxDistance = 2f;

    [SerializeField]
    private LayerMask mask;

    private PlayerUI _playerUI;
    private Player _player;

    private OutlineEffect outline;

    GameObject target;
    private void Awake()
    {
        _playerUI = GetComponent<PlayerUI>();
        _player = GetComponent<Player>();
    }


    private void checkInteraction()
    {
        _playerUI.updateText(string.Empty);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, rayMaxDistance, mask))
        {

            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {

                Debug.DrawRay(ray.origin, ray.direction * rayMaxDistance, Color.red);
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();


                // 2D object
                if (interactable.GetComponent<OutlineEffect>() != null)
                {
                    outline = interactable.GetComponent<OutlineEffect>();
                    outline._isSelected = true;
                }


                // Player UI Update
                if (!interactable.useEvents)
                {
                    _playerUI.crosshairInteraction();
                    _playerUI.updateText(interactable.GetPromptMessage());
                }

                if (_player._onFoot.Interact.triggered)
                {
                    interactable.Interact();
                }
            }
        }
        else
        {
            if (outline != null)
            {
                outline._isSelected = false;
                outline = null;
            }

            _playerUI.crosshairNoInteraction();
            Debug.DrawRay(ray.origin, ray.direction * rayMaxDistance, Color.green);
        }

    }

    private void CheckHit(float weaponRange)
    {
        if (_player._onFoot.Attack.triggered)
        {
            if (_player._playerController.canAttack())
            {
                EventManager.AttackPerformed();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, weaponRange, mask))
                {
                    if (hitInfo.collider.GetComponent<Hittable>() != null)
                    {
                        Debug.DrawRay(ray.origin, ray.direction * rayMaxDistance, Color.cyan);


                        target = hitInfo.collider.gameObject;

                        _playerUI.crosshairInteraction();
                        
                        Invoke("InvokeEnemyHit", WeaponManager._currentWeapon.GetTimeTillHit());
                    }
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * rayMaxDistance, Color.yellow);
                }
            }

        }


    }


    void Update()
    {
        // Create raycast & check collisions with interactables
        this.checkInteraction();
        this.CheckHit(WeaponManager._currentWeapon.GetWeaponRange());
        if (_player._instantiateWeapon)
        {
            _player._instantiateWeapon = false;
            // TO apo kato mporei na ginei methodos pou kaleitai apo to player animation

            //Kane to hdh yparxon _weapon tou trapezioy paidi kai metakinise to 
            //! den exoume ksekatharisei ean to weapon to spawn einai teli
        }


    }
    public void InvokeEnemyHit()
    {
        EventManager.EnemyHitPerformed(target.GetComponent<Hittable>());
        StartCoroutine(Effects.Flash(target.GetComponentInChildren<SpriteRenderer>(), 0.5f));
        GetComponent<PlayerController>().PlayVFX(target.GetComponent<Transform>());
        GetComponent<PlayerController>().PlayImpactSound();
    }

}

