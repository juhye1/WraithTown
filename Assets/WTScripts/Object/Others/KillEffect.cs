using Spine.Unity;
using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class KillEffect : ObjectPoolBase
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
        SetActive(true);
        anim.AnimationState.SetAnimation(0, "Monster_die", false);
        target = obj;
        transform.position = obj.transform.position;
        Invoke("OffEffect", 1);
    }
    public void OffEffect()
    {
        Debug.Log($"{gameObject.name} - OffEffect() »£√‚µ !");
        target = null;
        WTPoolManager.Instance.Release(this);
    }
}
