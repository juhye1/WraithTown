using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithTile : MonoBehaviour
{
    public GameObject wraith;
    public SpriteRenderer sr;
    public SkeletonAnimation skel;
    public MeshRenderer meshRenderer;
    public bool isSpecial;
    public int index;

    private void Awake()
    {
        skel.enabled = false;
        meshRenderer.enabled = false;
    }

    public void SetTileSpecialColor()
    {
        isSpecial = true;
        sr.color = Color.cyan;
    }
    public void SetTileNormalColor()
    {
        isSpecial = false;
        sr.color = Color.white;
    }

    public void SetUnit(SkeletonDataAsset data, string key)
    {
        skel.enabled = true;
        meshRenderer.enabled = true;
        skel.skeletonDataAsset = data;
        skel.skeleton.SetSkin("default");
        skel.Skeleton.SetSlotsToSetupPose();
        skel.Initialize(true);
        skel.AnimationState.SetAnimation(0, key, true);
    }

    public void RemoveUnit()
    {
        skel.enabled = false;
        meshRenderer.enabled = false;
    }
}
