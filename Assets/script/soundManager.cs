using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static soundManager I;
    //Controller
    private GameObject cont_sfx;
    private GameObject cont_vibe;
    private GameObject cont_bgm;

    //kind of sounds(bgm,sfx)
    private List<AudioSource> _audio = new List<AudioSource>();

    //sounds
    public AudioClip[] _clips = new AudioClip[16];

    //sounds dic
    Dictionary<string, AudioClip> sound_clips = new Dictionary<string, AudioClip>();

    public AudioSource originSource;
    public AudioSource bgmSource;

    private void Awake()
    {
        I = this;

        init_sounds();

        cont_bgm = GameObject.Find("controllBGM");
        cont_sfx = GameObject.Find("controllSFX");
        cont_vibe = GameObject.Find("vibeManager");
    }
    private void init_sounds()
    {
        bgmSource.loop = true;

        sound_clips.Add("BGM", _clips[0]);
        sound_clips.Add("bubbleStart", _clips[1]);
        sound_clips.Add("bubblePop", _clips[2]);
        sound_clips.Add("bubbleMiss", _clips[3]);
        sound_clips.Add("bubbleMeow", _clips[4]);
        sound_clips.Add("clothBbop", _clips[5]);
        sound_clips.Add("textBbap", _clips[6]);
        sound_clips.Add("catBasic", _clips[7]);
        sound_clips.Add("tutoPop", _clips[8]);
        sound_clips.Add("textDraw", _clips[9]);
        sound_clips.Add("btnClick", _clips[10]);
        sound_clips.Add("nodeBubble", _clips[11]);
        sound_clips.Add("btnFalse", _clips[12]);
        sound_clips.Add("buySuccess", _clips[13]);
        sound_clips.Add("buyFailed", _clips[14]);
        sound_clips.Add("score", _clips[15]);
    }

    //씬 전환등 상태일때 재생되던 소리 초기화
    public void clear()
    {
        for (int i = 0; i < _audio.Count; i++)
        {
            _audio[i].gameObject.SetActive(false);            
        }
        bgmSource.gameObject.SetActive(false);
    }

    private void createdAudioSource()
    {
        AudioSource audioSource = Instantiate(originSource);
        audioSource.transform.parent = cont_sfx.transform;
        _audio.Add(audioSource);
    }

    public void play_BGM(string name,float volume, float pitch = 1.0f)
    {
        bgmSource.gameObject.SetActive(true);
        bgmSource.clip = sound_clips[name];
        bgmSource.volume = volume;
        bgmSource.pitch = pitch;
        bgmSource.loop = true; ;

        bgmSource.Play();
    }

    public void play_sfx(string name, float volume,float pitch = 1.0f)
    {        
        int sourceIndex = getPlaySourceIndex();

        _audio[sourceIndex].gameObject.SetActive(true);
        _audio[sourceIndex].clip = sound_clips[name];
        _audio[sourceIndex].volume = volume;
        _audio[sourceIndex].pitch = pitch;
        _audio[sourceIndex].loop = false;

        _audio[sourceIndex].Play();
    }

    int getPlaySourceIndex()
    {
        for (int i = 0; i < _audio.Count; ++i)
        {
            if (!_audio[i].isPlaying)
            {
                return i;
            }
        }

        createdAudioSource();
        return _audio.Count - 1;
    }

    public void clear_sfx()
    {
        for (int i = 0; i < _audio.Count; i++)
        {
            _audio[i].gameObject.SetActive(false);
        }
    }
    public void stop_sfx()
    {
        for (int i = 0; i < _audio.Count; i++)
        {
            _audio[i].Stop();
        }
    }
    public void on_vibe()
    {
        cont_vibe.SetActive(true);
    }
    public void off_vibe()
    {
        cont_vibe.SetActive(false);
    }
    public void on_sfx()
    {
        cont_sfx.SetActive(true);
    }
    public void off_sfx()
    {
        cont_sfx.SetActive(false);
    }
    public void on_BGM()
    {
        cont_bgm.SetActive(true);
    }
    public void off_BGM()
    {
        cont_bgm.SetActive(false);
    }
}
