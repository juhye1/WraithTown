using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGoods : ObjectPoolBase
{
    public SkeletonAnimation skeletonAnimation;  // Spine 애니메이션

    public override void Init()
    {
        base.Init();
    }

    public void Setup(Vector2 pos)
    {
        base.Setup();
        SetActive(true);
        PlayGoldAnimation(pos);
    }

    public void PlayGoldAnimation(Vector2 pos)
    {
        transform.position = pos;
        Sequence seq = DOTween.Sequence();
        skeletonAnimation.AnimationState.SetAnimation(0, "animation", true);
        seq.Append(transform.DOMoveY(transform.position.y + 1f, 0.5f)
            .SetEase(Ease.OutQuad));
        seq.AppendInterval(0.2f);
        seq.Append(transform.DOMoveY(transform.position.y - 1.5f, 0.7f)
            .SetEase(Ease.InQuad));
        seq.Join(DOTween.To(() => skeletonAnimation.skeleton.A,
                            x => skeletonAnimation.skeleton.A = x,
                            0, 0.5f));
        seq.AppendCallback(() => Release());

        seq.Play();
    }
}
