using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Unit : MonoBehaviour
{
    public Image disablePlayerAbImg;
    public Image disableSupportAbImg;
    public TextMeshProUGUI playerABTMP;
    [Header("GameObject")]
    public GameObject supportUnitTMPBox;
    public GameObject playerABTMPBox;
    public Slot_TMPSupportUnit tmpUnitPrefab;
    private List<Slot_TMPSupportUnit> tmpUnitList = new();
    public RectTransform parentRT;
    private StringBuilder sb = new StringBuilder(20);

    private void OnEnable()
    {
        ShowPlayerAbility();
    }
    public void ShowRewards()
    {
        //보유한 유닛 목록 이건 전투 끝나고
    }

    public void OnClickBtn_ShuffleUnitPos()
    {
        WTGlobal.CallEventDelegate(WTEventType.ChangePoint, -10); // 테스트
        //유닛 위치 랜덤 바꾸기
    }

    private void ShowPlayerAbility()
    {
        playerABTMPBox.SetActive(true);
        supportUnitTMPBox.SetActive(false);
        WTMain main = WTMain.Instance;
        WTPlayerAbility ab = main.playerData.playerAb;
        sb.AppendLine(ab.maxHP.ToString());
        sb.AppendLine(ab.hpRecoveryPerSec.ToString());
        sb.AppendLine(ab.damage.ToString());
        sb.AppendLine(ab.moveSpeed.ToString());
        sb.AppendLine(ab.attackSpeed.ToString());
        sb.AppendLine(ab.projectileCount.ToString());
        sb.AppendLine(ab.attackRange.ToString());
        playerABTMP.SetText(sb.ToString());
        sb.Clear();
    }

    private void ShowSupportAbility()
    {
        playerABTMPBox.SetActive(false);
        supportUnitTMPBox.SetActive(true);
        WTMain main = WTMain.Instance;
        List<ushort> supList = main.playerData.supportUnits;
        if (supList.Count > 0)
        {
            for(int i=0; i<supList.Count; ++i)
            {
                Slot_TMPSupportUnit unit = Instantiate(tmpUnitPrefab, parentRT);
                WTSupportUnitAbilityTemplate data = main.GetSupportUnitAbilityTemplate(supList[i]);
                unit.SetData(data);
                tmpUnitList.Add(unit);
            }
            //유닛 먹으면 보조 기능 띄우기 ~~
        }
    }

    public void OnClickBtn_ShowPlayerAbility()
    {
        disablePlayerAbImg.enabled = false;
        disableSupportAbImg.enabled = true;
        ShowPlayerAbility();
    }

    public void OnClickBtn_ShowSupportAbility()
    {
        disablePlayerAbImg.enabled = true;
        disableSupportAbImg.enabled = false;
        ShowSupportAbility();
    }
}
