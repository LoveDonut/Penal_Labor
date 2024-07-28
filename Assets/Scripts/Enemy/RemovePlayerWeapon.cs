using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlayerWeapon : MonoBehaviour
{
    #region PrivateVariables
    [SerializeField] bool _isChasingEnemy;
    ManageStage _stageManager;
    PlayerWeaponManagement _weaponManagement;
    PlayerResourceManagement _resourceManagement;
    Weapon[] weapons;
    #endregion

    #region PrivateMethods

    void Awake()
    {
        _weaponManagement = FindObjectOfType<PlayerWeaponManagement>();
        _resourceManagement = FindObjectOfType<PlayerResourceManagement>();
        weapons = transform.parent.transform.parent.GetComponentsInChildren<Weapon>();
        _stageManager = transform.parent.transform.parent.GetComponentInParent<ManageStage>();
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
                _weaponManagement.LoseWeapon((int)weapon.GetWeaponType());
                UpdateEnemyWeapon(weapon.GetWeaponType());
            }
            else
            {
                _resourceManagement.RemoveResources();
                collision.transform.position = Vector3.zero;
                if(_isChasingEnemy)
                {
                    _stageManager.EndChaseAfterCatching();
                }
                _stageManager.PlayerGoOut();
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
