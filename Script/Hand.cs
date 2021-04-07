using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public string handName;
    public float Range;//범위
    public int damage;
    public float workspeed;
    public float attDelay;//근접공격속도
    public float attDelay1;//근접공격 활성
    public float attDelay2;//근접공격 비활성

    public Animator ani;
    public AudioClip swing_sound;
}
