using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CharacterDirection
{
    Left,
    Right
}

public class BaseFSM : MonoBehaviour
{
    public IState currentState;
    public BasePlayer player => BasePlayer.Instance;
    public GameObject modelParent;
    public Vector2 targetPos;
    public Rigidbody2D rb;

    [Header("Animations")]
    public SkeletonAnimation anim;
    protected CharacterDirection Direction;

    public bool isAttack;
    public bool isCooltime;
    public float cooltime = 1f;
    protected virtual void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (anim == null)
            anim = GetComponentInChildren<SkeletonAnimation>();
    }

    public virtual void ChangeState(IState newState)
    {
        //Debug.Log(newState);
        if (currentState != null)
            currentState?.Exit();
        //Debug.Log(currentState);
        currentState = newState;
        //Debug.Log(currentState);
        currentState?.Enter();
    }


    public virtual void Flip()
    {
        SetDirection(targetPos);
        SetFlip();
    }

    public virtual void SetDirection(Vector3 destination)
    {
        Vector3 direction = destination - transform.position;
        Direction = direction.x < 0 ? CharacterDirection.Left : CharacterDirection.Right;
    }

    //_animator가 붙어있는 오브젝트가 모델 오브젝트
    public virtual void SetFlip()
    {
        if (anim.skeleton == null)
            return;
        anim.skeleton.ScaleX = Direction == CharacterDirection.Left ? Mathf.Abs(anim.skeleton.ScaleX) : -Mathf.Abs(anim.skeleton.ScaleX);
    }


}
