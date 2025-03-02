using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Spine.Unity;

public enum PlayerSkin
{
    Miho = 0,
    Kebi
}

public class BasePlayer : Singleton<BasePlayer>, BaseObject
{
    #region ���� 
    public bool isPlaying;
    public bool isDead;
    public PlayerSkin skinType;
    public SkeletonDataAsset[] newSkeletonData;
    string skinName;
    [SerializeField]
    private int sTileCount;
    public List<WraithTile> tiles = new List<WraithTile>();
    #endregion

    #region ���� Ŭ����
    public PlayerFSM fsm;
    public PlayerInput input;
    public RotationTarget rotObj;
    public Transform projectileTr;
    #endregion

    #region ����Ƽ �Լ�
    protected override void Awake()
    {
        base.Awake();
        Init();
        fsm.Init();
    }

    protected virtual void Update()
    {
        if (fsm.currentState != null)
            fsm.currentState?.Execute();
        if (rotObj != null)
            rotObj.RotateTowards(fsm.targetPos);
        fsm.WaitForCooltime();
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

    public void SetSpecialTile()
    {
        HashSet<int> uniqueNumbers = new HashSet<int>();
        while (uniqueNumbers.Count < sTileCount)
        {
            int randomNumber = Random.Range(0, tiles.Count);
            Debug.Log(randomNumber);
            uniqueNumbers.Add(randomNumber);
        }

        foreach(var idx in uniqueNumbers)
        {
            tiles[idx].isSpecial = true;
            tiles[idx].SetTileSpecialColor();
        }
    }

    public void SetSkin(PlayerSkin type)
    {
        skinType = type;
        fsm.anim.skeletonDataAsset = newSkeletonData[(int)type];
        fsm.anim.Initialize(true);
        fsm.ChangeState(fsm.IdleState);
    }

    [ContextMenu("skinMiho")]
    public void SetMiho()
    {
        SetSkin(PlayerSkin.Miho);
    }


}
