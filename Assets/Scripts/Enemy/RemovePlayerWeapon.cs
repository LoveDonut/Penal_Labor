using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlayerWeapon : MonoBehaviour
{
    #region PrivateVariables
    PlayerWeaponManagement _weaponManagement;
    Weapon[] weapons;
    #endregion

    #region PrivateMethods

    void Awake()
    {
        _weaponManagement = FindObjectOfType<PlayerWeaponManagement>();
        weapons = transform.parent.transform.parent.GetComponentsInChildren<Weapon>();
    }

    void Start()
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Weapon weapon = collision.GetComponentInChildren<Weapon>();

            if (weapon != null && weapon.GetWeaponType() != PlayerWeaponManagement.EWeaponType.Hands)
            {
//                Debug.Log($"Remove Weapon! : {weapon.GetWeaponType()}");
                _weaponManagement.LoseWeapon((int)weapon.GetWeaponType());
                UpdateEnemyWeapon(weapon.GetWeaponType());
            }
            else
            {
//                Debug.Log("Player only has hands");
            }
        }
    }

    void UpdateEnemyWeapon(PlayerWeaponManagement.EWeaponType weaponType)
    {
        foreach(Weapon weapon in weapons)
        {
            Debug.Log(weapon.GetWeaponType());
            if(weapon.GetWeaponType() == weaponType)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }

    #endregion
}
