using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    bool _isAttacking = false;
    public bool IsAttacking => _isAttacking;

    public void Attack1()
    {
        _isAttacking = true;
    }

    public void OnAttackCompleted()
    {
        _isAttacking = false;
    }
}
