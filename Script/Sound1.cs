using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Sound1 : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer mixer;
    public AudioSource bgm;
    public AudioClip[] Bglist;
    public static Sound1 instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnsceneLoaded;
        }
        else
            Destroy(gameObject);
    }

    private void OnsceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < Bglist.Length; i++)
        {
            if (arg0.name == Bglist[i].name)
                Bgmsound(Bglist[i]);
        }

    }
    public void BgmsoundVolum(float val)
    {
        mixer.SetFloat("MyExposedParam", Mathf.Log10(val) * 20);
    }
    public void Sfxsound(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(go, clip.length);
    }

    public void Bgmsound(AudioClip clip)
    {
        bgm.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        bgm.clip = clip;
        bgm.loop = true;
        bgm.volume = 0.1f;
        bgm.Play();
    }

}