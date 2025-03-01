using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Lobby : MonoBehaviour
{
    public Image continueDisableImg;
    private void OnEnable()
    {
        WTGlobal.RegisterEventDelegate(WTEventType.SaveDataLoaded, EnableContinueBtn);
    }

    private void OnDisable()
    {
        WTGlobal.UnregisterEventDelegate(WTEventType.SaveDataLoaded, EnableContinueBtn);
    }
    private void EnableContinueBtn(int val)
    {
        //����� ������ ���� �� �̾��ϱ� ��ư ��Ȱ��ȭ
        bool enable = val == 1 ? true : false;
        continueDisableImg.enabled = enable;
    }
    public void OnClickBtn_StartGame()
    {
        WTMain main = WTMain.Instance;
        main.ChangeGameState(WTGameState.Game);
        //ó������
    }

    public void OnClickBtn_ContinueGame()
    {
        WTMain main = WTMain.Instance;
        main.ChangeGameState(WTGameState.Game);
        //�̾��ϱ�(���丮 �Ⱥ�����)
    }

    public void OnClickBtn_QuitGame()
    {
        Application.Quit();
        //��������
    }

    public void OnClickBtn_OptionMenu()
    {
        //�ɼ� ��ư�� ���� �ӽ÷� �־�����!
        WTMain main = WTMain.Instance;
        main.SaveData();
    }
}
