using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IState
{
    void Enter();
    void Execute();
    void Exit();
}
public abstract class BaseState : IState
{
    protected BaseFSM fsm;
    public string animName { get; protected set; } = string.Empty;
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();

    public BaseState(BaseFSM stateMachine)
    {
        this.fsm = stateMachine;
    }

    protected void StartAnimation(string animName, int track = 0, bool isRepeat = true)
    {
        //fsm.anim.AnimationState.SetAnimation(0, animName, isRepeat);
    }

    protected void StopAnimation(int track = 0, float delayTime = 0.5f)
    {
        if(delayTime != 0)
        {
            fsm.anim.AnimationState.SetEmptyAnimation(track, delayTime);
        }
        else
        {
            fsm.anim.AnimationState.ClearTrack(track);
        }
    }

    protected void SetAnimSpeed(float speed = 1f)
    {
        fsm.anim.timeScale = speed;
    }
}
