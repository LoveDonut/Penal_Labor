using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitDetect : MonoBehaviour
{
    #region PrivateVariables

    Collider2D _myCollider;
    PlayerWeaponManagement.EWeaponType _weaponType;

    #endregion

    #region PublicVariables

    public bool _istouchingResource;
    public bool _istouchingShopKeeper;
    [HideInInspector] public Resource _touchedResource;

    #endregion

    #region PublicMethods

    void Awake()
    {
        _myCollider = GetComponent<Collider2D>();
        _weaponType = GetComponentInParent<Weapon>().GetWeaponType();
    }
    void Start()
    {

    }

    void Update()
    {

    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Resource"))
        {
            if(CheckCanGather(collision.gameObject.GetComponent<Resource>().GetResourceType()))
            {
                _istouchingResource = true;
            }
            _touchedResource = collision.gameObject.GetComponent<Resource>();
        }
        else if(collision.gameObject.CompareTag("ShopKeeper"))
        {
            _istouchingShopKeeper = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Resource"))
        {
            _istouchingResource = false;
            _touchedResource = null;
        }
        else if (collision.gameObject.CompareTag("ShopKeeper"))
        {
            _istouchingShopKeeper = false;
        }

        Weapon weapon = GetComponentInParent<Weapon>();
        if (weapon != null)
        {
            weapon.SetDrillAnimation(false);
        }
    }
    
    bool CheckCanGather(Resource.EResource resourceType)
    {
        switch (resourceType)
        {
            case Resource.EResource.Coal:
                if(_weaponType == PlayerWeaponManagement.EWeaponType.Axe) return false;
                break;
            case Resource.EResource.Tree:
                if(_weaponType == PlayerWeaponManagement.EWeaponType.Pickax) return false;
                break;
            case Resource.EResource.Iron:
                if (_weaponType == PlayerWeaponManagement.EWeaponType.Hands || _weaponType == PlayerWeaponManagement.EWeaponType.Axe) return false;
                break;
            default:
                return true;
        }
        return true;
    }
    #endregion
}
