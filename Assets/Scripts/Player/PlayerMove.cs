using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Pithano Onoma PlayerController
public class PlayerMove : MonoBehaviour
{
    private CharacterController _player;
    public Vector3 _moveDir;
    private Vector3 _velocity;

    public PlayerAnimator _playerAnimator;

    public bool _isGrounded = true;

    [Header("Controller")]
    [SerializeField] private float _speed = 5f;
    public float _runningSpeed = 1000f;
    public float _jumpHeight = 2f;
    public bool _isRunning = false;
    public float _gravity = -9.8f;

    [Header("Attack Properties")]
    public bool _isAttacking = false;

    public void Awake()
    {

        _player = GetComponent<CharacterController>();
    }

    public virtual void Move(Vector2 input)
    {
        _moveDir.x = input.x;
        _moveDir.z = input.y;

        if (_isGrounded)
        {
            if (_isRunning)
            {
                _player.Move(transform.TransformDirection(_moveDir) * _runningSpeed * Time.deltaTime);
            }
            else
            {
                _player.Move(transform.TransformDirection(_moveDir) * _speed * Time.deltaTime);
            }
        }
        else
        {

        }

        _velocity.y += _gravity * Time.deltaTime;
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    }

    public void Attack()
    {
        _isAttacking = true;

    }


    public void Jump()
    {
        if (_isGrounded)
        {
            _velocity.y = Mathf.Sqrt(-3.0f * _gravity * _jumpHeight);
        }
    }

}
