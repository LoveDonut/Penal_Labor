using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitDetect : MonoBehaviour
{
    #region PrivateVariables

    Collider2D _myCollider;

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
            _istouchingResource = true;
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
    
    #endregion
}
