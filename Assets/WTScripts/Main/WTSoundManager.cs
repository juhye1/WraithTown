using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource[] bgm;
    [SerializeField] AudioSource sfx;
    public Dictionary<string, AudioClip> bgmClip = new();

    private int activeChannel = 0;
    private Tween[] fadeInTweens = new Tween[3];
    private Tween[] fadeOutTweens = new Tween[3];
    private int sfxCount = 0;

    protected override void Awake()
    {
        base.Awake();
        InitTweens();   
    }

    public void Init()
    {
       var datas = Resources.LoadAll<AudioClip>("");
        foreach (var data in datas)
        {
            bgmClip.Add(data.name, data);
            Debug.LogWarning(data.name);
        }
    }

    #region ????

    public void PlayBGM(AudioClip clip)
    {
        int nextChannel = 1 - activeChannel;

        bgm[nextChannel].clip = clip;

        bgm[nextChannel].Play();
        bgm[activeChannel].Stop();

        activeChannel = nextChannel;
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (sfxCount > 10) return;

        sfx.PlayOneShot(clip, volume);
        sfxCount++;

        DOTween.To(() => 0f, x => { }, 0f, clip.length)
            .OnComplete(() => sfxCount--)
            .SetAutoKill(true);
    }
    #endregion


    private void InitTweens()
    {
        if (bgm == null || bgm.Length == 0)
        {
            Debug.LogError("bgm 배열이 비어 있습니다!");
            return;
        }

        //bgm 크기에 맞춰 배열 초기화
        fadeInTweens = new Tween[bgm.Length];
        fadeOutTweens = new Tween[bgm.Length];

        for (int i = 0; i < bgm.Length; i++)
        {
            if (bgm[i] == null)
            {
                Debug.LogError($"bgm[{i}]가 null입니다! Inspector에서 확인하세요.");
                continue;
            }

            fadeInTweens[i] = bgm[i].DOFade(1f, 1f)
                .SetAutoKill(false)
                .Pause()
                .OnPlay(() => bgm[i].Play());

            fadeOutTweens[i] = bgm[i].DOFade(0f, 1f)
                .SetAutoKill(false)
                .Pause()
                .OnComplete(() => bgm[i].Stop());
        }
    }


    #region 
    public void SetBGMAudioMixerValue(float value)
    {
        if (value == 0)
            audioMixer.SetFloat("BGM", -80f);
        else
            audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
    }

    public void SetSFXAudioMixerValue(float value)
    {
        if (value == 0)
            audioMixer.SetFloat("SFX", -80f);
        else
            audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }
    #endregion
}

