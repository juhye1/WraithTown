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
        
        switch (context.phase)
        {
            case InputActionPhase.Started:
                isPress = true;
                if (player.fsm.isAttack) return;
                if (player.isPlaying && !player.isDead)
                {
                    player.fsm.ChangeState(player.fsm.MoveState);
                }

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
                isPress = false;
                if (player.isPlaying && !player.isDead)
                {
                    player.fsm.moveDir = Vector2.zero;
                    if (player.fsm.isAttack) return;
                    player.fsm.ChangeState(player.fsm.IdleState);
                }
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
                if(!player.fsm.isAttack)
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

    public void OnClickEsc(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:

                break;
            case InputActionPhase.Canceled:
                Debug.Log("esc");
                break;
            default:
                break;
        }
    }

    void Update()
    {
        lastMousePos = Mouse.current.position.ReadValue();

        // 마우스 위치를 월드 좌표로 변환
        screenToWorldPos = lastMousePos;
        screenToWorldPos.z = Mathf.Abs(cam.transform.position.z - player.transform.position.z); // 올바른 깊이 설정

        Vector3 worldMousePos = cam.ScreenToWorldPoint(screenToWorldPos);

        player.fsm.Aiming(worldMousePos);
        player.fsm.targetPos = player.fsm.shootDir;
    }

}
