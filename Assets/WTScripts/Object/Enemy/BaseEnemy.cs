using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseObject
{
    #region 변수 

    #endregion

    #region 하위 클래스

    #endregion

    #region 유니티 함수
    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
    #endregion

    #region 일반 메서드
    public void DeathEvt()
    {
        throw new System.NotImplementedException();
    }

    public void Init()
    {
        throw new System.NotImplementedException();
    }

    public void OnAttack()
    {
        throw new System.NotImplementedException();
    }

    public void OnTakeDamaged()
    {
        throw new System.NotImplementedException();
    }

    public void Setup()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
