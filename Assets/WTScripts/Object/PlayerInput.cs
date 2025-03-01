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
    private Vector2 moveInput;
    private Vector2 lastMousePos;
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
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        switch (context.phase)
        {
            case InputActionPhase.Started:
                Debug.Log("플레이어 공격");
                //if(!player.fsm.isAttack)
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
        moveInput = context.ReadValue<Vector2>();
        switch (context.phase)
        {
            case InputActionPhase.Started:


                break;
            case InputActionPhase.Performed:

                break;
            case InputActionPhase.Canceled:

                break;
            default:
                break;
        }
    }

    void Update()
    {
        // 항상 마우스 위치 업데이트
        lastMousePos = Mouse.current.position.ReadValue();

        // 이동 입력이 있거나 마우스가 움직일 때만 갱신
        if (moveInput != Vector2.zero || Mouse.current.delta.ReadValue() != Vector2.zero)
        {
            screenToWorldPos.Set(lastMousePos.x, lastMousePos.y, 0);
            player.fsm.shootDir = cam.ScreenToWorldPoint(screenToWorldPos);
            player.fsm.targetPos = player.fsm.shootDir;
        }
    }
}
