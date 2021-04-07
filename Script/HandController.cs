using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField]
    private Hand currentHand;

    private bool isAttack = false;
    private bool isSwing = false;

    private RaycastHit hitInfo;

    public static bool isActive = false;
    // Update is called once per frame
    void Update()
    {
        if(isActive)
        Attack();
        
    }

    private void Attack()
    {
        if(Input.GetButton("Fire1"))
        {
            if(!isAttack)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }
    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        currentHand.ani.SetTrigger("Attack");
        yield return new WaitForSeconds(currentHand.attDelay1);
        isSwing = true;

        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentHand.attDelay2);
        isSwing = false;

        yield return new WaitForSeconds(currentHand.attDelay - currentHand.attDelay1 - currentHand.attDelay2);
        isAttack = false;

        
    }
    IEnumerator HitCoroutine()
    {
        while(isSwing)
        {
            if(CheckObj())
            {
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;

        }
    }

    private bool CheckObj()
    {
        if(Physics.Raycast(transform.position,transform.forward,out hitInfo, currentHand.Range))
        {
            return true;
        }

        return false;
    }

    public void HandChange(Hand _hand)
    {
        if (WeaponManager.currentWeapon != null)
        {
            WeaponManager.currentWeapon.gameObject.SetActive(false);

            currentHand = _hand;
            WeaponManager.currentWeapon = currentHand.GetComponent<Transform>();
            WeaponManager.currentWeaponAni = currentHand.ani;

            currentHand.transform.localPosition = Vector3.zero;
            currentHand.gameObject.SetActive(true);
            isActive = true;
        }
    }
}
