using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun1 : MonoBehaviour
{
    public string gunName;
    public float range;//�����Ÿ�
    public float accuracy;//��Ȯ��
    public float fireRate;//����
    public float reloadTime;//����
    
    public int damage;

    public int reloadBulletCount;//������ ����
    public int currentBulletCount;//���� ź����
    public int carryBulletCount;//���� �Ѿ�
    public int MaxBulletCount;//�ִ����
    

    public float retroActionForce;//�ݵ�
    public float retroActionFineForce;//������ �ݵ�
    public Vector3 fineOriginPos;

    public Animator ani;

    public ParticleSystem Flash;

    public AudioClip fire_sound;

}
