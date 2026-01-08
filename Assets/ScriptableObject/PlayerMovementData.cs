using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "data", menuName = "Movement Data")]
public class PlayerMovementData : ScriptableObject
{
    [Header("Physic")]
    public float Gravity;
    public float FallGravity;
    public float LinearDrag;
    public float AttackDrag;

    [Header("Run")]
    public float MoveSpeed;
    public float MaxSpeed;

    [Header("Jump")]
    public float JumpForce;

    [Header("Dash")]
    public float DashForce;
}
