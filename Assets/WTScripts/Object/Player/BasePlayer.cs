using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine.UIElements;
using DG.Tweening;

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

    public SpriteRenderer[] effects;

    #endregion

    #region ���� Ŭ����
    public PlayerFSM fsm;
    public PlayerInput input;
    public PlayerStatHandler stat;
    public RotationTarget rotObj;
    public Transform projectileTr;
    public SkeletonAnimation floorAnim;
    WTMain main => WTMain.Instance;
    WTGameData data => main.playerData;
    #endregion

    string floorAnimName = "floor";
    #region ����Ƽ �Լ�
    protected override void Awake()
    {
        base.Awake();
        Init();
        fsm.Init();
        floorAnim.AnimationState.SetAnimation(0, floorAnimName, true);
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
        floorAnim.AnimationState.SetAnimation(0, floorAnimName, true);
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

        if (isDead || data.currentHP <= 0) return;
        data.currentHP -= damage;
        Debug.LogWarning(data.currentHP);
        WTGlobal.CallEventDelegate(WTEventType.PlayerHPControl, data.currentHP);
        if (data.currentHP <= 0)
        {
            DeathEvt();
        }
        else
        {

        }
    }

    public void PlayCritical()
    {
        PlayEffect(0);
    }

    public void PlayShield()
    {
        PlayEffect(1);
    }

    private void PlayEffect(int val)
    {
        effects[val].enabled = true;
        effects[val].color = Color.white;
        effects[val].DOColor(Color.clear, 1);
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
