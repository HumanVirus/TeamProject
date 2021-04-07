using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Gun1 currentGun1;

    private float currentFireRate;

    private AudioSource audioSource;

    private bool isReload=false;
    [HideInInspector]
    private bool isfineSightmode=false;
    
    
    private Vector3 originPos;
    private RaycastHit hitInfo;
    [SerializeField]
    private Camera thecam;
    private CrossHair crossHair;
    //피격이펙트
    [SerializeField]
    private GameObject hit_effect_1;

    public static bool isActive = true;
    
    void Start()
    {
        originPos = Vector3.zero;
        audioSource = GetComponent<AudioSource>();
        crossHair = FindObjectOfType<CrossHair>();

        WeaponManager.currentWeapon = currentGun1.GetComponent<Transform>();
        WeaponManager.currentWeaponAni = currentGun1.ani;
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            GunFireRateCalc();
            TryFire();
            TryReload();
            TryfineSightmode();
        }
    }
    private void GunFireRateCalc()
    {
        if(currentFireRate>0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }
    private void TryFire()
    {
        if(Input.GetButton("Fire1")&&currentFireRate <=0 && !isReload)
        {
            Fire();
        }
    }
    private void Fire()
    {
        if (!isReload)
        {
            if (currentGun1.currentBulletCount > 0)
                Shoot();
            else
            {
                CancelFineSight();
                StartCoroutine(ReloadCoroutine());
            }
        }

    }
    private void Shoot()
    {
        crossHair.FrieAni();
        currentGun1.currentBulletCount--;
        currentFireRate = currentGun1.fireRate;
        currentGun1.Flash.Play();
        playsound(currentGun1.fire_sound);
        Hit();
        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());
    }

    private void Hit()
    {
        if (Physics.Raycast(thecam.transform.position, thecam.transform.forward +
            new Vector3(Random.Range(-crossHair.GetAccuracy() - currentGun1.accuracy, crossHair.GetAccuracy() + currentGun1.accuracy),
                        Random.Range(-crossHair.GetAccuracy() - currentGun1.accuracy, crossHair.GetAccuracy() + currentGun1.accuracy), 0)
            , out hitInfo, currentGun1.range))
        {
            GameObject clone = Instantiate(hit_effect_1, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(clone, 0.2f);

        }
    }
    private void TryReload()
    {
        if(Input.GetKeyDown(KeyCode.R)&&!isReload&&currentGun1.currentBulletCount<currentGun1.reloadBulletCount)
        {
            CancelFineSight();
            StartCoroutine(ReloadCoroutine());
        }
    }

    public void CancelReload()
    {
        if(isReload)
        {
            StopAllCoroutines();
            isReload = false;
        }
    }

    IEnumerator ReloadCoroutine()
    {
        if(currentGun1.carryBulletCount>0)
        {
            isReload = true;
            currentGun1.ani.SetTrigger("Reload");

            currentGun1.carryBulletCount += currentGun1.currentBulletCount;
            currentGun1.currentBulletCount = 0;

            yield return new WaitForSeconds(currentGun1.reloadTime);
            if(currentGun1.carryBulletCount>=currentGun1.reloadBulletCount)
            {
                currentGun1.currentBulletCount = currentGun1.reloadBulletCount;
                currentGun1.carryBulletCount -= currentGun1.reloadBulletCount;
            }
            else
            {
                currentGun1.currentBulletCount = currentGun1.carryBulletCount;
                currentGun1.carryBulletCount = 0;
            }
            isReload = false;
        }
        else
        {
            Debug.Log("총알이 없습니다");
        }    
    }

    private void TryfineSightmode()
    {
        if(Input.GetButtonDown("Fire2")&&!isReload)
        {
            FineSight();
        }
    }

    public void CancelFineSight()
    {
        if (isfineSightmode)
            FineSight();
    
    }
    IEnumerator RetroActionCoroutine()
    {
        Vector3 recoiBack = new Vector3(currentGun1.retroActionForce, originPos.y, originPos.z);
        Vector3 retroActionRecoiBack = new Vector3(currentGun1.retroActionFineForce, currentGun1.fineOriginPos.y, currentGun1.fineOriginPos.z);

        if (!isfineSightmode)
        {
            currentGun1.transform.localPosition = originPos;

            while (currentGun1.transform.localPosition.x <= currentGun1.retroActionForce - 0.02f)
            {
                currentGun1.transform.localPosition = Vector3.Lerp(currentGun1.transform.localPosition, recoiBack, 0.4f);
                yield return null;
            }
            while (currentGun1.transform.localPosition != originPos)
            {
                currentGun1.transform.localPosition = Vector3.Lerp(currentGun1.transform.localPosition, originPos, 0.1f);
                yield return null;
            }
        }
        else
        {
            currentGun1.transform.localPosition = currentGun1.fineOriginPos;

            while (currentGun1.transform.localPosition.x <= currentGun1.retroActionFineForce - 0.02f)
            {
                currentGun1.transform.localPosition = Vector3.Lerp(currentGun1.transform.localPosition, retroActionRecoiBack, 0.2f);
                yield return null;
            }
            while (currentGun1.transform.localPosition != currentGun1.fineOriginPos)
            {
                currentGun1.transform.localPosition = Vector3.Lerp(currentGun1.transform.localPosition, currentGun1.fineOriginPos, 0.2f);
                yield return null;
            }
        }
    }

    private void FineSight()
    {
        isfineSightmode = !isfineSightmode;
        currentGun1.ani.SetBool("Zoom", isfineSightmode);
        crossHair.FineSightAni(isfineSightmode);
        if(isfineSightmode)
        {
            StopAllCoroutines();
            StartCoroutine(FinesightActiveCorousine());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(FinesightDeActiveCorousine());
        }
    }

    IEnumerator FinesightActiveCorousine()
    {
        while(currentGun1.transform.localPosition != currentGun1.fineOriginPos)
        {
            currentGun1.transform.localPosition = Vector3.Lerp(currentGun1.transform.localPosition, currentGun1.fineOriginPos, 0.2f);
            yield return null;
        }
    }

    IEnumerator FinesightDeActiveCorousine()
    {
        while (currentGun1.transform.localPosition != originPos)
        {
            currentGun1.transform.localPosition = Vector3.Lerp(currentGun1.transform.localPosition, originPos, 0.2f);
            yield return null;
        }
    }



    private void playsound(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
    public Gun1 GetGun1()
    {
        return currentGun1;
    }
    public bool GetFineSightmode()
    {
        return isfineSightmode;
    }
    public void RunningAni(bool _flag)
    {
        currentGun1.ani.SetBool("Run", _flag);
    }

    public void GunChange(Gun1 _gun)
    {
        if (WeaponManager.currentWeapon != null)
        {
            WeaponManager.currentWeapon.gameObject.SetActive(false);

            currentGun1 = _gun;
            WeaponManager.currentWeapon = currentGun1.GetComponent<Transform>();
            WeaponManager.currentWeaponAni = currentGun1.ani;

            currentGun1.transform.localPosition = Vector3.zero;
            currentGun1.gameObject.SetActive(true);
            isActive = true;
        }
    }
}
