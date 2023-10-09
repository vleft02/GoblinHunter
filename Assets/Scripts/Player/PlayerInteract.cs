using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraHolder;

    [SerializeField]
    private float rayMaxDistance = 5f;

    [SerializeField]
    private LayerMask mask;

    [SerializeField] public GameObject _weaponToSpawn;
    [SerializeField] public GameObject _weaponHolder;

    private PlayerUI _playerUI;
    private Player _player;

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



        if (Physics.Raycast(ray, out hitInfo, rayMaxDistance, mask) && hitInfo.collider.GetComponent<Interactable>() != null)
        {
            Debug.DrawRay(ray.origin, ray.direction * rayMaxDistance, Color.red);
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();

            // Player UI Update
            _playerUI.crosshairInteraction();
            _playerUI.updateText(interactable.GetPromptMessage());

            if (_player._onFoot.Interact.triggered)
            {
                /*                if (interactable.GetComponent<Weapon>() != null)
                                {
                                    _player._changeWeapon = true;
                                    //! allagi isos
                                    _player._currentWeapon = PlayerWeapon.SWORD;
                                }
                */
                interactable.Interact();
            }
         
        }
        else
        {
            _playerUI.crosshairNoInteraction();
            Debug.DrawRay(ray.origin, ray.direction * rayMaxDistance, Color.green);
        }
    }
        
    void Update()
    {
        // Create raycast & check collisions with interactables
        this.checkInteraction();

        if (_player._instantiateWeapon)
        {
            _player._instantiateWeapon = false;
            // TO apo kato mporei na ginei methodos pou kaleitai apo to player animation

            //Kane to hdh yparxon _weapon tou trapezioy paidi kai metakinise to 
            //! den exoume ksekatharisei ean to weapon to spawn einai teli
            
/*            
            _player._currentWeapon = PlayerWeapon.SWORD;
            _player._weapon = GetComponent<Sword>();
            _player._weapon.transform.SetParent(_weaponHolder.transform);
            _player._weapon.transform.localPosition = Vector3.zero;
            _player._weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
            _player._weapon.transform.localScale = Vector3.one;*/
        }


    }



}

