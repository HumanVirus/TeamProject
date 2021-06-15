using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
public class SoundManager : MonoBehaviour
{
    static public SoundManager first;
    public AudioSource audioSourcesBGM;
    public AudioSource[] audioSourcesEffect;

    public string[] playSoundname;
    public Sound[] effectsound;
    public Sound[] bgmsound;
    void Awake()
    {
        if (first == null)
        {
            first = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    void Start()
    {
        playSoundname = new string[audioSourcesEffect.Length];
      
    }
    public void PlaySound(string _name)
    {
        for (int i = 0; i < bgmsound.Length; i++)
        {
            if(_name == bgmsound[i].name)
            {
             
                    if(!audioSourcesBGM.isPlaying)
                    {
                       
                        audioSourcesBGM.clip = bgmsound[i].clip;
                        audioSourcesBGM.Play();
                        return;
                    }

                
            }

        }
        for (int i = 0; i < effectsound.Length; i++)
        {
            if (_name == effectsound[i].name)
            {
                for (int j = 0; j < audioSourcesEffect.Length; j++)
                {
                    if (!audioSourcesEffect[j].isPlaying)
                    {
                        playSoundname[j] = effectsound[i].name;
                        audioSourcesEffect[j].clip = effectsound[i].clip;
                        audioSourcesEffect[j].Play();
                        return;
                    }

                }
            }

        }
    }
    public void StopSound(string _name)
    {
       
        for (int j = 0; j < audioSourcesEffect.Length; j++)
        {
            if (playSoundname[j] == _name)
            {
                audioSourcesEffect[j].Stop();
                break;
            }
        }
    }

    public void MusicVolum(float volum)
    {
        audioSourcesBGM.volume = volum;
    }
}

