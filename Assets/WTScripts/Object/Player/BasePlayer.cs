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
    public PlayerStatHandler stat;
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

    public void Start()
    {
        stat.Init();
    }

    protected virtual void Update()
    {
        if (fsm.currentState != null)
            fsm.currentState?.Execute();
        if (rotObj != null)
            rotObj.RotateTowards(fsm.targetPos);
    }
    #endregion

    #region �޼ҵ�


    public void Init()
    {
        
    }

    public void Setup()
    {
        fsm.ChangeState(fsm.IdleState);
        stat.Init();
        isDead = false;
        isPlaying = true;
        WTMain.Instance.spawner.Setup();
    }

    public void OnAttack()
    {
        
    }

    public void OnTakeDamaged(int damage)
    {
        if (isDead || stat.status.hp <= 0) return;
        stat.status.hp -= damage;
        WTMain main = WTMain.Instance;
        Debug.LogWarning(stat.status.hp);
        main.playerData.currentHP = stat.status.hp;
        WTGlobal.CallEventDelegate(WTEventType.PlayerHPControl, stat.status.hp);
        if (stat.status.hp <= 0)
        {
            DeathEvt();
        }
        else
        {

        }
    }

    public void DeathEvt()
    {
        if(isDead) return;
        isDead = true;
        fsm.ChangeState(fsm.DieState);
        WTUIMain.Instance.GetPanel(WTUIState.GameOver);
    }
    #endregion

    public void SetSpecialTile()
    {
        HashSet<int> uniqueNumbers = new HashSet<int>();
        sTileCount = WTMain.Instance.dicPlayerStatTemplate[(ushort)WTMain.Instance.playerData.userUnitId].special_tile_count;
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
