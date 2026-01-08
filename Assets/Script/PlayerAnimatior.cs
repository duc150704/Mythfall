using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationName
{
    public const string Idle = "idle";
    public const string Run = "run";
    public const string JumpUp = "jumpUp";
    public const string FallDown = "fallDown";
    public const string Attack1 = "attack1";
    public const string Attack2 = "attack2";
    public const string Dash = "dash";
    public const string EndDash = "endDash";
}

public class PlayerAnimatior : MonoBehaviour
{
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAnimationState(EPlayerState state)
    {
        switch (state)
        {
            case EPlayerState.Idle:
                _animator.Play(AnimationName.Idle);
                break;
            case EPlayerState.Run:
                _animator.Play(AnimationName.Run);
                break;
            case EPlayerState.JumpUp:
                _animator.Play(AnimationName.JumpUp);
                break;
            case EPlayerState.FallDown:
                _animator.Play(AnimationName.FallDown);
                break;            
            case EPlayerState.Attack1:
                _animator.Play(AnimationName.Attack1);
                break;            
            case EPlayerState.Attack2:
                _animator.Play(AnimationName.Attack2);
                break;            
            case EPlayerState.Dash:
                _animator.Play(AnimationName.Dash);
                break;            
            case EPlayerState.EndDash:
                _animator.Play(AnimationName.EndDash);
                break;
        }
    }
}
