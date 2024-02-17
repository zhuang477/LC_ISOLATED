using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{
    UserData save =null;
    ItemDatabase itemDatabase;
    public EquipSwap activeCollider;
    public Collider2D weaponCollider;

    public Animator animator;

    public delegate void AttackAction();
    //send stanima cost to stanimabar.cs
    public static event AttackAction Attack;

    void Update(){
        save =GameManager.Instance.currentSaving;
        itemDatabase =GameManager.Instance.itemDatabase;
        if(save !=null){
            weaponCollider =activeCollider.WeaponCollider[activeCollider.currentWeaponLocInCollider].GetComponent<Collider2D>();
            
        }
    }

    void OnEnable(){
        Control.Phan_Attack +=Phalanxing_EnableCollider;
        Control.Stop_Phan_Attack +=DisableCollider;
    }

    void OnDisable(){
        Control.Phan_Attack -=Phalanxing_EnableCollider;
        Control.Stop_Phan_Attack -=DisableCollider;
    }
    
    //this is use by animation's event. Enable only attacking.
    public void EnableCollider(){
        weaponCollider.gameObject.SetActive(true);
        save.current_stanima -=(float)itemDatabase.getItem(save.weapon_id).stats_stringToint["Attack_Stanima_Cost"];
        if(Attack !=null){
            Attack();
        }
    }

    //this is use by animation's event. Enable only attacking.
    public void DisableCollider(){
        weaponCollider.gameObject.SetActive(false);
        animator.SetBool("Phalanxing",false);
    }

    //in Control.cs, when holding polearm and press mouse1.
    public void Phalanxing_EnableCollider(){
        if(itemDatabase.getItem(save.weapon_id).stats_stringTostring["Weapon Type"].Equals("Polearm")){
            weaponCollider.gameObject.SetActive(true);
            animator.SetBool("Phalanxing",true);
        }
    }
}
