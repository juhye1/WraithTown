using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseObject
{
    #region ���� 

    #endregion

    #region ���� Ŭ����

    #endregion

    #region ����Ƽ �Լ�
    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
    #endregion

    #region �Ϲ� �޼���
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
