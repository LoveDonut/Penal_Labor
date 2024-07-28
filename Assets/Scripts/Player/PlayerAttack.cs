using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    #region PrivateVariables
    Weapon _weapon;
    HitDetect _hitDetect;
    UIController _uiController;
    PlayerController _playerController;
    GameObject _camera;

    private bool _isGathering = false;
    #endregion

    #region PublicVariables

    #endregion

    #region PrivateMethods

    void Awake()
    {
        _uiController = FindObjectOfType<UIController>();    
        _playerController = FindObjectOfType<PlayerController>();
        _camera = FindObjectOfType<CinemachineBrain>().gameObject;
    }

    void OnAttack(InputValue input)
    {
        if (!_playerController._isActive) return;

        if (_hitDetect._istouchingResource && _hitDetect._touchedResource != null)
        {
            if(_weapon.GetWeaponType() != PlayerWeaponManagement.EWeaponType.Hands)
            {
                _isGathering = true;
            }
            // for drill
            if (_weapon.CompareTag("Drill"))
            {
                StartCoroutine(AttackWithDrill());
            }
            else
            {

                _hitDetect._touchedResource.Damaged(_weapon.GetPower());
                MakeFX();
            }
        }
        else if(_hitDetect._istouchingShopKeeper)
        {
            _uiController.SetItemBuyUI(true);
            _playerController._isActive = false;
        }
    }

    void MakeFX()
    {
        // Particle Effects
        ParticleSystem instance = Instantiate(_weapon.GetDamagedFX(), _weapon.GetHitPoint().position, _weapon.GetDamagedFX().transform.rotation, transform);
        FlipParticleRotation(instance);

        if(!_weapon.CompareTag("Drill"))
        {
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
            Invoke("TurnOffIsGathering", instance.main.duration + instance.main.startLifetime.constantMax);

            // Resource Shaking
            if(_hitDetect._touchedResource != null)
            {
                _hitDetect._touchedResource.ShakeResource();
            }
        }


        // Play Attack Animation
        _weapon.PlayAttackAnimation();
    }
    void FlipParticleRotation(ParticleSystem instance)
    {
        var shape = instance.shape;
        if(GetComponentInParent<PlayerController>().transform.localScale.x < 0)
        {
            shape.rotation = new Vector3(0f, 0f, 180f);
        }
    }

    IEnumerator AttackWithDrill()
    {
//        Debug.Log("Coroutine Start!");

        ParticleSystem instance = Instantiate(_weapon.GetDamagedFX(), _weapon.GetHitPoint().position, _weapon.GetDamagedFX().transform.rotation, transform);
        FlipParticleRotation(instance);

        // Play Attack Animation
        _weapon.PlayAttackAnimation();

        while (Input.GetMouseButton(0) && _hitDetect._istouchingResource)
        {
            // Resource Shaking
            if (_hitDetect._touchedResource != null)
            {
                _hitDetect._touchedResource.ShakeResource();
            }

            // Damaged
            _hitDetect._touchedResource.Damaged(_weapon.GetPower());

            yield return new WaitForEndOfFrame();
        }

//        Debug.Log("MouseButtonUp!");

        Destroy(instance);
        _isGathering = false;
        _weapon.SetDrillAnimation(false);
        _camera.transform.rotation = Quaternion.identity;
    }

    void TurnOffIsGathering()
    {
        _isGathering = false;
    }

    #endregion

    #region PublicMethods

    public void SetWeapon(Weapon weapon, HitDetect hitDetect)
    {
        _weapon = weapon;
        _hitDetect = hitDetect;
    }

    public bool GetIsGathering()
    {
        return _isGathering;
    }

    

    #endregion
}
