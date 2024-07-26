using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region PrivateVariables
    [SerializeField] int _power;
    [SerializeField] ParticleSystem _damagedFX;
    [SerializeField] GameObject _hitPoint;
    [SerializeField] int[] costs = new int[3];

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
    public int GetPower() { return _power; }
    public ParticleSystem GetDamagedFX() { return _damagedFX; }
    public Transform GetHitPoint() { return _hitPoint.transform; }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }

    public int[] GetCosts()
    {
        return costs;
    }
    #endregion
}
