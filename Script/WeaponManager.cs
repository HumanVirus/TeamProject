using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponManager : MonoBehaviour
{
    public static bool ischangeWeapon = false;
    public static Transform currentWeapon;
    public static Animator currentWeaponAni;

    [SerializeField]
    private string currentWeaponType;
    [SerializeField]
    private float CangeDelayTime;
    [SerializeField]
    private float CangeDelayTimeEnd;

    [SerializeField]
    private Hand[] hands;
    [SerializeField]
    private Gun1[] guns;

    private Dictionary<string, Hand> handDictionary = new Dictionary<string, Hand>();
    private Dictionary<string, Gun1> gunDictionary = new Dictionary<string, Gun1>();

    [SerializeField]
    private GunController gunController;
    [SerializeField]
    private HandController handController;




    void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            gunDictionary.Add(guns[i].gunName, guns[i]);
        }
        for (int i = 0; i < hands.Length; i++)
        {
            handDictionary.Add(hands[i].handName, hands[i]);
        }

    }

    void Update()
    {
        if (!ischangeWeapon)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                StartCoroutine(ChangeWeaponCoroutine("HAND", "¸Ç¼Õ"));
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                StartCoroutine(ChangeWeaponCoroutine("GUN", "Submachinegun")); ;
        }
    }

    public IEnumerator ChangeWeaponCoroutine(string _type, string _name)
    {
        ischangeWeapon = true;
        currentWeaponAni.SetTrigger("Weapon_out");

        yield return new WaitForSeconds(CangeDelayTime);

        CancelWeaponAction();
        WeaponChange(_type, _name);

        yield return new WaitForSeconds(CangeDelayTimeEnd);

        currentWeaponType = _type;
        ischangeWeapon = false;
    }

    private void CancelWeaponAction()
    {
        switch (currentWeaponType)
        {
            case "GUN":
                gunController.CancelFineSight();
                gunController.CancelReload();
                GunController.isActive = false;
                break;
            case "HAND":
                HandController.isActive = false;
                break;
        }
    }

    private void WeaponChange(string _type, string _name)
    {
        if (_type == "GUN")
            gunController.GunChange(gunDictionary[_name]);
        
        else if (_type == "HAND")
            handController.HandChange(handDictionary[_name]);
        
    }
}
