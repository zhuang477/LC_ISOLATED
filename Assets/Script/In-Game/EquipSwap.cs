using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class EquipSwap : MonoBehaviour
{
    //two methods used in this script: the swap of the armor/weapon
    //and the swap of the weapon's colliderbox.
    public UserData save=null;
    public GameObject Player;

    public List<GameObject> Armor_Sprite;

    public GameObject weapon;
    public GameObject shield;

    public List<GameObject> WeaponCollider;
    public int currentWeaponLocInCollider =0; 
    //WeaponCollider:
    // 0: Fist
    // 1: GladSword
    // 2: Pylongs
    // 

    public bool shouldColliderActive =false;

    // Start is called before the first frame update
    void Start()
    {
        save =GameManager.Instance.currentSaving;
        //weapon's collider will be handled with AttackEvent.cs
        if(save !=null){
            for(int i=0;i<WeaponCollider.Count;i++){
                WeaponCollider[i].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldColliderActive ==false){

        }
        ArmorSwap();
        WeaponSwap();
        ShieldSwap();
    }

    void OnEnable(){
        //from draganddrop.cs
        DragAndDrop.disableCollider +=DisableColliderOnWeaponSwitch;
    }
    
    void OnDisable(){
        //from draganddrop.cs
        DragAndDrop.disableCollider -=DisableColliderOnWeaponSwitch;
    }

    void DisableColliderOnWeaponSwitch(){
        for(int i=0;i<WeaponCollider.Count;i++){
            WeaponCollider[i].gameObject.SetActive(false);
        }
    }

    void ArmorSwap(){
        if(save !=null){
            int Armor_id =save.armor_id;
            //holy crap, a coding hell.
            if(Armor_id ==2){
                foreach (GameObject child in Armor_Sprite){
                    string partName =child.name;
                    child.GetComponent<SpriteResolver>().SetCategoryAndLabel(partName,"GladFlax");
                }
            }
            if(Armor_id ==5){
                foreach (GameObject child in Armor_Sprite){
                    string partName =child.name;
                    child.GetComponent<SpriteResolver>().SetCategoryAndLabel(partName,"Relos_Hoplite");
                }
            }
        }
    }

    void WeaponSwap(){
        if(save !=null){
            int Weapon_id =save.weapon_id;
            if(Weapon_id ==999){
                weapon.GetComponent<SpriteResolver>().SetCategoryAndLabel(weapon.name,"default");
                currentWeaponLocInCollider =0;
                
            }
            if(Weapon_id ==1){
                weapon.GetComponent<SpriteResolver>().SetCategoryAndLabel(weapon.name,"GladSword");
                currentWeaponLocInCollider =1;
            }
            if(Weapon_id ==4){
                weapon.GetComponent<SpriteResolver>().SetCategoryAndLabel(weapon.name,"Pylongs_Pike");
                currentWeaponLocInCollider =2;
            }
        }
    }

    void ShieldSwap(){
        if(save !=null){
            int Shield_id =save.shield_id;
            if(Shield_id ==998){
                shield.GetComponent<SpriteResolver>().SetCategoryAndLabel(shield.name,"default");
            }
            if(Shield_id ==3){
                shield.GetComponent<SpriteResolver>().SetCategoryAndLabel(shield.name,"GladShield");
            }
        }
    }
}
