using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{
    public EquipSwap activeCollider;
    public Collider2D weaponCollider;

    void Update(){
        weaponCollider =activeCollider.WeaponCollider[activeCollider.currentWeaponLocInCollider].GetComponent<Collider2D>();
    }
    public void EnableCollider(){
        weaponCollider.gameObject.SetActive(true);
    }

    public void DisableCollider(){
        weaponCollider.gameObject.SetActive(false);
    }
}
