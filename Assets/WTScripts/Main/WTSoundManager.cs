using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;


public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource[] bgm;
    [SerializeField] AudioSource sfx;
    public AudioClip[] bgmClip;

    private int activeChannel = 0;
    private Tween[] fadeInTweens = new Tween[2];
    private Tween[] fadeOutTweens = new Tween[2];
    private int sfxCount = 0;

    protected override void Awake()
    {
        base.Awake();
        InitTweens();
    }

    [ContextMenu("OnBGM")]
    public void Test1()
    {
        PlayBGM(bgmClip[0]);
    }

    #region ????
    public void PlayBGM(AudioClip clip)
    {
        int nextChannel = 1 - activeChannel;

        bgm[nextChannel].clip = clip;

        fadeInTweens[nextChannel].Restart();
        fadeOutTweens[activeChannel].Restart();

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
        for (int i = 0; i < 2; i++)
        {
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

