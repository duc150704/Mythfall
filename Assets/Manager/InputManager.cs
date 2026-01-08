using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private void Awake()
    {
        if( Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool OnJumpButtonPressed()
    {
        return Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.Space);
    }

    public bool OnJumpButtonReleased()
    {
        return Input.GetKeyUp(KeyCode.K) || Input.GetKeyUp(KeyCode.Space);
    }

    public bool OnAttackButtonPressed()
    {
        return Input.GetKeyDown(KeyCode.J);
    }

    public bool OnDashButtonPressed()
    {
        return Input.GetKeyDown(KeyCode.L);
    }
}
