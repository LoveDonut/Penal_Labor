using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region PrivateVariables
    [SerializeField] ItemConfig _itemConfig;
    [SerializeField] ParticleSystem _damagedFX;
    [SerializeField] GameObject _hitPoint;

    CinemachineStateDrivenCamera _stateCamera;
    PlayerAttack _playerAttack;
    Animator _animator;

    #endregion

    #region PrivateMethods

    void Awake()
    {
        _stateCamera = FindObjectOfType<CinemachineStateDrivenCamera>();
        _playerAttack = FindObjectOfType<PlayerAttack>();
        _animator = GetComponent<Animator>();
    }

    #endregion


    #region PublicMethods
    public float GetPower() { return _itemConfig.GetPower(); }
    public ParticleSystem GetDamagedFX() { return _damagedFX; }
    public Transform GetHitPoint() { return _hitPoint.transform; }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger("Attack");
        SetDrillAnimation(true);
    }

    public int[] GetCosts()
    {
        return _itemConfig.GetCosts();
    }

    public PlayerWeaponManagement.EWeaponType GetWeaponType()
    {
        return _itemConfig.GetWeaponType();
    }

    public void SetDrillAnimation(bool b)
    {
        if (CompareTag("Drill"))
        {
            _animator.SetBool("isAttacking", b);
        }
    }
    #endregion
}
