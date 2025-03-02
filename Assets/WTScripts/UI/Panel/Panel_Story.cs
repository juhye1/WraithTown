using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panel_Story : MonoBehaviour
{
    private string[] story = new string[] { "100년 동안 달의 정수를 하나도 모으지 못했어.\n이러다간 저주를 풀기는커녕 소멸 해버리겠는걸..\n달의 조각이라도 모으면 좋겠는데....",
    "룰루랄라~ 원석도 잔뜩 모았겠다!\n이번 주는 걱정 없이 살겠는걸!",
    "아쉽지만 오늘은 저 녀석의 혼령이라도\n잡아서 돌아가야겠는걸...?",
    "좋은 말로  할때 네 녀석의 혼령을 내놓아라!\n순순히 주면 목숨은 부지할 것이야!",
    "혼령? 너 같으면 혼령을 주겠냐?\n다른 곳이나 알아봐라~",
    "토끼 주제에 감히 날 가지고 놀아?\n좋게 말할때 줄 것을....내가 직접 빼앗아주지!"};

    public TextMeshProUGUI storyTMP;
    private int count = 0;

    private void Awake()
    {
        storyTMP.SetText(story[count]);
        count++;
    }
    public void OnClickBtn_StoryArrow()
    {
        if(count > story.Length -1)
        {
            OnClickBtn_Skip();
            Debug.Log("게임시작");
        }
        storyTMP.SetText(story[count]);
        count++;
    }

    public void OnClickBtn_Skip()
    {
        WTMain main = WTMain.Instance;
        main.ChangeGameState(WTGameState.Game);
        WTUIMain.Instance.DestroyPanel(WTUIState.Story);
    }
}
