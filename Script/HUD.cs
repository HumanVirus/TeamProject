using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private GunController gunController;
    private Gun1 currentGun;

    [SerializeField]
    private GameObject BulletHUD;

    [SerializeField]
    private Text[] texts_Bullet;

    void Update()
    {
        CheckBullet();

    }

    private void CheckBullet()
    {
        currentGun = gunController.GetGun1();
        texts_Bullet[0].text = currentGun.MaxBulletCount.ToString();
        texts_Bullet[1].text = currentGun.carryBulletCount.ToString();
        texts_Bullet[2].text = currentGun.currentBulletCount.ToString();
    }
}
