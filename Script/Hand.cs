using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public string handName;
    public float Range;//����
    public int damage;
    public float workspeed;
    public float attDelay;//�������ݼӵ�
    public float attDelay1;//�������� Ȱ��
    public float attDelay2;//�������� ��Ȱ��

    public Animator ani;
    public AudioClip swing_sound;
}
