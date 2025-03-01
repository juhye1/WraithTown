using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour
{
    BasePlayer player;
    public bool isPress;
    private Camera cam;
    private Vector3 screenToWorldPos = Vector3.zero;

    void Start()
    {
        if(player == null)
            player = GetComponent<BasePlayer>();
        cam = Camera.main;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var dir = context.ReadValue<Vector2>();
        Debug.Log(dir);
        switch (context.phase)
        {
            case InputActionPhase.Started:
                if(player.isPlaying && !player.isDead)
                {
                    player.fsm.ChangeState(player.fsm.MoveState);
                }
                isPress = true;
                break;
            case InputActionPhase.Performed:
                if (player.isPlaying && !player.isDead)
                {
                    player.fsm.moveDir = dir;
                }
                else
                {
                    player.fsm.moveDir = Vector2.zero;
                }
                break;
            case InputActionPhase.Canceled:
                if (player.isPlaying && !player.isDead)
                {
                    player.fsm.moveDir = Vector2.zero;
                    player.fsm.ChangeState(player.fsm.IdleState);
                }
                isPress = false;
                break;
            default:
                break;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                Debug.Log("플레이어 공격");
                player.fsm.ChangeState(player.fsm.AttackState);
                break;
            case InputActionPhase.Canceled:

                break;
            default:
                break;
        }
    }
    public void OnAiming(InputAction.CallbackContext context)
    {
        var pos = context.ReadValue<Vector2>();
        switch (context.phase)
        {
            case InputActionPhase.Started:


                break;
            case InputActionPhase.Performed:
                screenToWorldPos.Set(pos.x, pos.y, 0);
                player.fsm.shootDir = cam.ScreenToWorldPoint(screenToWorldPos);
                break;
            case InputActionPhase.Canceled:

                break;
            default:
                break;
        }
    }
}
