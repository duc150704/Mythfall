using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerMovementData _movementData;
    [SerializeField] Transform _groundCheckPoint;
    [SerializeField] float _groundCheckSize;
    [SerializeField] LayerMask _groundLayerMask;

    Rigidbody2D _rb;
    PlayerAttack _attack;
    PlayerAnimatior _animator;

    int _moveDirection;
    bool _canJump;
    bool _isFacingRight = true;
    bool _isDashing = false;

    public bool IsDashing => _isDashing;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _attack = GetComponent<PlayerAttack>();
        _animator = GetComponent<PlayerAnimatior>();
    }

    private void Start()
    {
        PhysicSetup();
    }

    private void Update()
    {
        GroudCheck();
        TurnCheck();
    }

    private void FixedUpdate()
    {
        Move();
        HandleGravity();
        HandleDrag();
    }

    private void PhysicSetup()
    {
        _rb.gravityScale = _movementData.Gravity;
        _rb.drag = _movementData.LinearDrag;
    }

    private void HandleGravity()
    {
        if (_isDashing)
        {
            _rb.gravityScale = 0;
            return;
        }

        if(_rb.velocity.y < 0)
        {
            _rb.gravityScale = _movementData.FallGravity;
        } else
        {
            _rb.gravityScale = _movementData.Gravity;
        }
    }

    private void HandleDrag()
    {
        if(_attack.IsAttacking)
        {
            _rb.drag = _movementData.AttackDrag;
        }
        else
        {
            _rb.drag = _movementData.LinearDrag;
        }
    }

    public void Dash()
    {
        if (_isDashing == true)
            return;
        StartCoroutine(DashIE());
    }

    IEnumerator DashIE()
    {
        _isDashing = true;
        //_rb.AddForce(_movementData.DashForce * new Vector2(Mathf.Sign(transform.localScale.x), 0f), ForceMode2D.Impulse);
        _rb.velocity = new Vector2(_movementData.DashForce * Mathf.Sign(transform.localScale.x), _rb.velocity.y);
        yield return new WaitForSeconds(1f);

        if (IsGround())
        {
            EndDash();
            yield return new WaitForSeconds(0.1f); // de anim endDash chay xong
        }
        _isDashing = false;
    }

    private void EndDash()
    {
        _animator.SetAnimationState(EPlayerState.EndDash);
    }

    public void SetMoveDirection(int direction)
    {
        _moveDirection = direction;
    }

    private void Move()
    {
        if (Mathf.Abs(_rb.velocity.x) < _movementData.MaxSpeed)
        {
            _rb.AddForce((_movementData.MoveSpeed)* _moveDirection * Vector2.right, ForceMode2D.Force);
        }
    }

    public void Jump()
    {
        if (!_canJump)
            return;

        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(_movementData.JumpForce * Vector2.up, ForceMode2D.Impulse);
        _canJump = false;
    }

    public void JumpCut()
    {
        if(_rb.velocity.y > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
        }
    }

    private void GroudCheck()
    {
        bool _col = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckSize, _groundLayerMask);
        _canJump = (_col) ? true : false;
    }

    private void TurnCheck()
    {
        if(_moveDirection != 0 && Mathf.Sign(_moveDirection) != Mathf.Sign(transform.localScale.x) && IsGround() && !_isDashing)
        {
            Turn();
        }
    }

    private void Turn()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        _isFacingRight = !_isFacingRight;
    }

    public bool IsGround() => _canJump;
    public Vector2 GetVelocity() => _rb.velocity;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(_groundCheckPoint.position, _groundCheckSize);
    }
}
