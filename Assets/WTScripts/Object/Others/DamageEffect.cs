using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DamageEffect : ObjectPoolBase
{
    [SerializeField]
    private SkeletonAnimation anim;


    public override void Setup()
    {
        base.Setup();
    }
    public void Setup(GameObject obj)
    {
        SetActive(true);
        TrackEntry trackEntry = anim.AnimationState.SetAnimation(0, "Monster_hit", false);
        trackEntry.Complete += OffEffect;
        transform.position = obj.transform.position;
    }

    public void OffEffect(TrackEntry trackEntry)
    {
        WTPoolManager.Instance.Release(this);
    }

}
