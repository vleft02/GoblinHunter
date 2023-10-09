using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAnimator : BaseAnimator
{
/*    private WeaponAnimationFactory _weaponFactory;*/
    private Weapon _weapon;
    private Stack<AnimationClip> _animationStack;
    private bool _equipNextWeapon = false;

    private Animator _animator;

    private float _lockedStateTime;

    private int _currentState;

    private PlayerMove _playerMove;

    private Player _player;

    AnimationClip currentClip;

    private bool _spawnWeapon = false;
    private Weapon _weaponToBeEquiped;

    private void updateWeaponAnimation()
    {
/*        _weapon = _weaponFactory.getWeaponAnimation(_player._currentWeapon);*/
    }

    protected override int LockState(int state, float lt)
    {
        _lockedStateTime = Time.time + lt;
        return state;
    }

    /*
    protected override int GetState()
    {
        if (Time.time < _lockedStateTime) return _currentState;

        if (_playerMove._isGrounded)
        {
            if (_player._changeWeapon)
            {
                switch (_player._currentWeapon)
                {
                    //case PlayerWeapon.HANDS:
                    case PlayerWeapon.SWORD:

                        if (_currentAnimInSeq < _handsToSwordSequence.Count - 1)
                        {
                            if (_currentAnimInSeq == 0) _player._instantiateWeapon = true;
                            _currentAnimInSeq++;
                            return LockState(_handsToSwordSequence.ElementAt(_currentAnimInSeq).Key, _handsToSwordSequence.ElementAt(_currentAnimInSeq).Value);
                        }
                        else
                        {
                            _player._changeWeapon = false;
                            _currentAnimInSeq = -1;
                            break;
                        }
                }
            }


            if (_playerMove._isAttacking)
            {
                //return LockState(_weaponAnimation.getAttack(), )

                //_animationStack.Push(_weaponAnimation.)

                if (_animationStack.Count > 0)
                {
                    AnimationClip currentClip = _animationStack.Pop();
                    return LockState(currentClip.State, _animationStack.Peek().Duration);
                }

                if (_player._currentWeapon == PlayerWeapon.HANDS)
                {
                    resetAttack();
                    return LockState(PUNCH, 0.3f);
                }
                else
                {
                    if (_currentAnimInSeq < _swordAttackSequence.Count - 1)
                    {
                        _currentAnimInSeq++;
                        return LockState(_swordAttackSequence.ElementAt(_currentAnimInSeq).Key, _swordAttackSequence.ElementAt(_currentAnimInSeq).Value);
                    }
                    else
                    {
                        resetAttack();
                    }
                }
            }

            if (_player._currentWeapon == PlayerWeapon.SWORD) return IDLE_SWORD;
            else return IDLE;

            //if (_playerMove._moveDir.Equals(Vector2.zero)) {
            //    if (_player._currentWeapon == PlayerWeapon.SWORD) return IDLE_SWORD;
            //    else return IDLE;
            //}
            //else return this.playerMotor.isRunning ? FastRunning : Running;
        }


        return IDLE;
    }
    */

    protected override int GetState()
    {
        if (Time.time < _lockedStateTime) return _currentState;

        currentClip = _animationStack.Pop();

        return LockState(currentClip.State, currentClip.Duration);
    }

    protected void UpdateAnimationStack()
    {
        if (_playerMove._isGrounded)
        {
            if (_playerMove._isAttacking)
            {
                resetAttack();

                // var finalStack = itemsToAdd.Aggregate(stack, (currentStack, item) => Push(currentStack, item));
                for (var i = _weapon._attackSequence.Count() - 1; i >= 0; i--)
                {
                    _animationStack.Push(_weapon._attackSequence.ElementAt(i));
                }
            }
            else
            {
  
                if (_player._changeWeapon)
                {
                    //First We want to push the Unequip animation of the current weapon
                    //to the animation stack the first time and run the equip 
                    //animation of the weapon that is equipped next
                    if (_equipNextWeapon)
                    {
                        _equipNextWeapon = false;
                        _player._changeWeapon = false;

                        /*animationStack.Push(_weapon.EQUIP);*/
                    }
                    else
                    {
                        _equipNextWeapon = true;

                        _animationStack.Push(_weaponToBeEquiped.EQUIP);
                        _animationStack.Push(_weapon.UNEQUIP);

                        _spawnWeapon = true;

                    }
                }
                else
                {
                    _animationStack.Push(_weapon.IDLE);
                }
            }

        }


    }
    public void ChangeWeapon(Weapon _weaponToBeEquipped) 
    {
        _player._changeWeapon = true;
        _weaponToBeEquiped = _weaponToBeEquipped;
    }

    protected override void ChangeAnimationState(int state)
    {
        if (state == _currentState) return;


        if (_spawnWeapon)
        {
            //When we intend to spawn the next weapon we first check if 
            //the next weapon equip animation has begun
            if (currentClip == _weaponToBeEquiped.EQUIP)
            {
                _spawnWeapon = false;
                _player.WeaponPickUp(_weaponToBeEquiped);
                _player._instantiateWeapon = true;
                _weapon = _weaponToBeEquiped;
                _weaponToBeEquiped = null;
            }
        }

        _animator.CrossFadeInFixedTime(state, 0, 0);
        _currentState = state;
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMove = GetComponent<PlayerMove>();
        _player = GetComponent<Player>();
        _animationStack = new Stack<AnimationClip>();
        _weapon = WeaponAnimationFactory.getWeaponAnimation(_player._currentWeapon);
        Debug.Log(_weapon.GetPromptMessage());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_animationStack.Count == 0)
        {
            UpdateAnimationStack();
            if (_player._changeWeapon)
            {
                updateWeaponAnimation();
            }
        }

        var state = GetState();
        ChangeAnimationState(state);

    }

    private void resetAttack()
    {
        _playerMove._isAttacking = false;
    }

}
