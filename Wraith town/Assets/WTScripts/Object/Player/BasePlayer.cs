using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class BasePlayer : Singleton<BasePlayer>, BaseObject
{
    #region ���� 
    public bool isPlaying;
    public bool isDead;
    #endregion

    #region ���� Ŭ����
    public PlayerFSM fsm;
    public PlayerInput input;
    public RotationTarget rotObj;
    #endregion

    #region ����Ƽ �Լ�
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

    #region �޼ҵ�


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
