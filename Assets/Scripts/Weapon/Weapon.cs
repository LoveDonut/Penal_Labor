using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region PrivateVariables
    [SerializeField] int _power;
    [SerializeField] ParticleSystem _damagedFX;
    [SerializeField] GameObject _hitPoint;

    PlayerAttack _playerAttack;
    HitDetect _hitDetect;
    Animator _animator;

    #endregion

    #region PrivateMethods

    void Awake()
    {
        _playerAttack = FindObjectOfType<PlayerAttack>();
        _hitDetect = GetComponentInChildren<HitDetect>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _playerAttack.SetWeapon(this, _hitDetect);
    }

    void Update()
    {
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
    #endregion
}
