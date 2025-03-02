using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DamageEffect : ObjectPoolBase
{
    GameObject target;
    [SerializeField]
    private SkeletonAnimation anim;


    public override void Setup()
    {
        base.Setup();
    }
    public void Setup(GameObject obj)
    {
        Setup();
        SetActive(true);
        transform.position = obj.transform.position;
        target = obj;
        Invoke("OffEffect", 2);
    }
    public void OffEffect()
    {
        Debug.Log($"{gameObject.name} - OffEffect() »£√‚µ !");
        WTPoolManager.Instance.Release(this);
    }


}
