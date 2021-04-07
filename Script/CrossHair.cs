using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [SerializeField]
    private Animator ani;

    private float gunAccuracy;

    [SerializeField]
    private GameObject go_CrosshairHUD;
    [SerializeField]
    private GunController gunController;

    public void WalkingAni(bool _flag)
    {
        WeaponManager.currentWeaponAni.SetBool("Walk",_flag);
        ani.SetBool("Walking", _flag);
    }
    public void RunningAni(bool _flag)
    {
        WeaponManager.currentWeaponAni.SetBool("Run", _flag);
        ani.SetBool("Running", _flag);
    }
    public void CrunchingAni(bool _flag)
    {
        WeaponManager.currentWeaponAni.SetBool("Crunch", _flag);
        ani.SetBool("Crunching", _flag);
    }
    public void JumpingAni(bool _flag)
    {
        ani.SetBool("Jumping", _flag);
    }
    public void FineSightAni(bool _flag)
    {
        WeaponManager.currentWeaponAni.SetBool("Zoom", _flag);
        ani.SetBool("FineSight", _flag);
    }
    public void FrieAni()
    {
        if (ani.GetBool("Walking"))
            ani.SetTrigger("WalkFire");
        else if (ani.GetBool("Crunching"))
            ani.SetTrigger("CrunchFire");
        else
            ani.SetTrigger("IdelFire");
    }
    public float GetAccuracy()
    {
        if (ani.GetBool("Walking"))
            gunAccuracy = 0.07f;
        else if (ani.GetBool("Crunching"))
            gunAccuracy = 0.02f;
        //else if (gunController.GetFineSightmode())
        //    gunAccuracy = 0.001f;
        else
            gunAccuracy = 0.04f;

        return gunAccuracy;

    }
}
