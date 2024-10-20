using System;
using UnityEngine;

public static class EventManager 
{
    public static event Action AttackEvent;

    public static event Action EquipWeaponEvent;
    
    public static event Action<Hittable> EnemyHitEvent;

    public static event Action<GameObject> EnemyDeathEvent;

    public static event Action PlayerHitEffectEvent;

    public static event Action<float> PlayerHitEvent;

    public static event Action PlayerDeathEvent;

    public static event Action EnemyAttackEvent;

    public static event Action TogglePause;

    public static event Action ToggleEquipMenu;

    public static event Action BossDefeatedEvent;

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
    }

    public static void EnemyDeath(GameObject enemy)
    {
        EnemyDeathEvent?.Invoke(enemy);
    }

    public static void PlayerDeath()
    {
        PlayerDeathEvent?.Invoke();
    }

    internal static void PlayerTakeHit(float damage)
    {
        PlayerHitEffectEvent?.Invoke();
        PlayerHitEvent?.Invoke(damage);
    }
    
    public static void EnemyAttackPerform()
    {
        EnemyAttackEvent?.Invoke();
    }

    public static void PauseEvent()
    {
        TogglePause?.Invoke();
    }

    public static void EquipMenuEvent()
    {
        ToggleEquipMenu?.Invoke();
    }

    public static void BossDefeated()
    {
        BossDefeatedEvent?.Invoke(); 
    }

}