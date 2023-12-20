using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    public static bool _isGrounded { get; set; }
    public static bool _isRunning { get; set; }
    public static bool _isAttacking { get; set; }

    //public bool IsGrounded {get => _isGrounded, }
    private static float attackCooldown = 0;
    private static float timer = 0;
    private static bool _isStaminaEnough = false;

    private void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    public static void setIsStaminaEnough(bool canAttack)
    {
        _isStaminaEnough = canAttack;
    }


    public static bool CanAttack()
    {
        
        if (timer >= attackCooldown && _isStaminaEnough)
        {
            attackCooldown = 0;
            return true;
        }
        return false;
    }
    
    public static void AttackPerformed(float time) 
    {
        timer = 0;
        attackCooldown = time+ 0.1f;
    }
}
