using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class EventManager /*: MonoBehaviour*/
{
    public static event Action AttackEvent;

    public static event Action EquipWeaponEvent;
    
    public static event Action<Hittable> EnemyHitEvent;

    public static event Action<Hittable> EnemyDeathEvent;

    public static event Action PlayerHitEvent;

    public static event Action PlayerDeathEvent;

    public static event Action EnemyAttackEvent;
/*    public static event Action AttackWaitInAttackEnemy;
*/
    public static event Action EnemyIdleEvent;
    public static event Action EnemyWaitInIdleEvent;

    public static event Action EnemyWalkEvent;

    public static void EnemyWaitInIdle()
    {
        EnemyWaitInIdleEvent?.Invoke();
    }

    public static void WalkEnemy()
    {
        EnemyWalkEvent?.Invoke();
    }

    public static void IdleEnemy()
    {
        EnemyIdleEvent?.Invoke();
    }

    public static void AttackPerformed()
    {
        AttackEvent?.Invoke();
    }

    public static void EquipWeapon()
    {
        EquipWeaponEvent?.Invoke();
    }

    public static void EnemyHitPerformed(Hittable enemy)
    {
        EnemyHitEvent?.Invoke(enemy);
        enemy.TakeDamage(WeaponManager._currentWeapon.GetWeaponDamage());
        if (enemy.HasZeroHealth())
        {
            EnemyDeath(enemy);
        }
    }

    public static void EnemyDeath(Hittable enemy)
    {
        EnemyDeathEvent?.Invoke(enemy);
    }

    public static void PlayerDeath()
    {
        PlayerDeathEvent?.Invoke();
    }

    internal static void PlayerTakeHit()
    {
        PlayerHitEvent?.Invoke();
    }
    
    public static void EnemyAttackPerform()
    {
        EnemyAttackEvent?.Invoke();
    }

/*    public static void EnemyWaitInAttack()
    {
        AttackWaitInAttackEnemy?.Invoke();
    }*/
}