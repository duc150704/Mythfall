using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerState
{
    None,
    Idle,
    Run,
    JumpUp,
    FallDown,
    Attack1,
    Attack2,
    Dash,
    EndDash,
}
public class PlayerController : MonoBehaviour
{
    PlayerMovement _movement;
    PlayerAnimatior _animatior;
    PlayerAttack _attack;

    int _moveDirection = 0;
    bool _isJumpPressed = false;
    bool _isJumpReleased = false; 
    bool _isDashPressed = false;
    bool _isAttackPressed = false;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _animatior = GetComponent<PlayerAnimatior>();
        _attack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        HandleInput();
        OnPlayerMove();
        SetAnimator();
    }

    private void FixedUpdate()
    {
        OnPlayerJump();
        OnPlayerDash();
    }

    private void OnPlayerDash()
    {
        if(_isDashPressed)
        {
            _movement.Dash();
        }
    }

    private void OnPlayerMove()
    {
        _movement.SetMoveDirection(_moveDirection);
    }

    private void OnPlayerJump()
    {
        if (_isJumpPressed)
        {
            Debug.Log("2");
            _movement.Jump();
        }
        if(_isJumpReleased)
        {
            Debug.Log("1");
            _movement.JumpCut();
        }
    }

    private void SetAnimator()
    {
        if (_attack.IsAttacking || _movement.IsDashing)
        {
            return;
        }

        if (_isAttackPressed)
        {
            _attack.Attack1();
            _animatior.SetAnimationState(EPlayerState.Attack1);
            return;
        }

        if (_isDashPressed)
        {
            _movement.Dash();
            if (_movement.IsDashing)
            {
                _animatior.SetAnimationState(EPlayerState.Dash);
            }
            return;
        }

        if (_movement.IsGround() && !_movement.IsDashing)
        {
            if (_moveDirection != 0)
            {
                _animatior.SetAnimationState(EPlayerState.Run);
            }
            else
            {
                _animatior.SetAnimationState(EPlayerState.Idle);
            }
            return;
        }

        if(_movement.GetVelocity().y > 0)
        {
            _animatior.SetAnimationState(EPlayerState.JumpUp);
        }
        else if(_movement.GetVelocity().y < 0)
        {
            _animatior.SetAnimationState(EPlayerState.FallDown);
        }
    }

    private void HandleInput()
    {
        if (_movement.IsDashing)
        {
            _moveDirection = 0;
            return;
        }
            
        if (Input.GetKey(KeyCode.A))
        {
            _moveDirection = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _moveDirection = 1;
        }
        else
        {
            _moveDirection = 0;
        }

        _isJumpPressed = InputManager.Instance.OnJumpButtonPressed();
        _isJumpReleased = InputManager.Instance.OnJumpButtonReleased();
        _isDashPressed = InputManager.Instance.OnDashButtonPressed();
        _isAttackPressed = InputManager.Instance.OnAttackButtonPressed();   
    }
}
