using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class BasePlayer : Singleton<BasePlayer>, BaseObject
{
    #region 변수 
    public bool isPlaying;
    public bool isDead;
    #endregion

    #region 하위 클래스
    public PlayerFSM fsm;
    public PlayerInput input;
    public RotationTarget rotObj;
    #endregion

    #region 유니티 함수
    protected virtual void Start()
    {
        fsm.Init();
    }

    protected virtual void Update()
    {
        if (fsm.currentState != null)
            fsm.currentState?.Execute();
        if (rotObj != null)
            rotObj.RotateTowards(fsm.shootDir);
    }
    #endregion

    #region 메소드


    public void Init()
    {
        
    }

    public void Setup()
    {
       
    }

    public void OnAttack()
    {
        
    }

    public void OnTakeDamaged()
    {
        
    }

    public void DeathEvt()
    {
       
    }
    #endregion


}
