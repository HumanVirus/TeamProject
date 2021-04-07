using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun1 : MonoBehaviour
{
    public string gunName;
    public float range;//사정거리
    public float accuracy;//정확도
    public float fireRate;//연사
    public float reloadTime;//장전
    
    public int damage;

    public int reloadBulletCount;//재장전 갯수
    public int currentBulletCount;//현재 탄알집
    public int carryBulletCount;//소유 총알
    public int MaxBulletCount;//최대소유
    

    public float retroActionForce;//반동
    public float retroActionFineForce;//정조준 반동
    public Vector3 fineOriginPos;

    public Animator ani;

    public ParticleSystem Flash;

    public AudioClip fire_sound;

}
