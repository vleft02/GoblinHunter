using UnityEngine;
using static PlayerAnimationFSM;

public class PlayerAnimationFSM : AnimationStateManager<PlayerAnimation>
{
    public enum PlayerAnimation
    {
        IDLE, EQUIP, UNEQUIP, ATTACK, HIT, DEATH
    }

    void InitStates()
    {
        States.Add(PlayerAnimation.IDLE, new IdleState());
        States.Add(PlayerAnimation.EQUIP, new EquipState());
        States.Add(PlayerAnimation.UNEQUIP, new UnequipState());
        States.Add(PlayerAnimation.ATTACK, new AttackState());
        States.Add(PlayerAnimation.HIT, new HitState());
        States.Add(PlayerAnimation.DEATH, new DeathState());
    }

    private void Awake()
    {
        InitStates();
        CurrentState = States[PlayerAnimation.IDLE];
        EventManager.AttackEvent += Attack;
        EventManager.EquipWeaponEvent += Equip;
        EventManager.PlayerDeathEvent += ToDeathAnimation;
    }



    private void OnDisable()
    {
        EventManager.AttackEvent -= Attack;
        EventManager.EquipWeaponEvent -= Equip;
        EventManager.PlayerDeathEvent -= ToDeathAnimation;
    }

    private void Attack()
    {
        if (CurrentState == States[PlayerAnimation.IDLE])
        {
            TransitionToState(PlayerAnimation.ATTACK);
        }
    }

    private void Equip() 
    {
        if (CurrentState == States[PlayerAnimation.IDLE] && WeaponManager._changeWeapon == true) 
        {
            TransitionToState(PlayerAnimation.UNEQUIP);
        }
    }

    private void GetHit() 
    {
        TransitionToState(PlayerAnimation.HIT);
    }

    public void ToDeathAnimation()
    {
        if (!TerminateFSM)
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
            TransitionToState(PlayerAnimation.DEATH);
            TerminateFSM = true;
        }

    }
}
