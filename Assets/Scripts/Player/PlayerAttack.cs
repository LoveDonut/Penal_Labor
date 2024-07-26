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
    #endregion

    #region PrivateMethods
    void Start()
    {

    }

    void Update()
    {

    }

    void OnAttack(InputValue input)
    {
        if (!_hitDetect._istoucingResource && _hitDetect._touchedResource == null) return;
        MakeFX();

        _hitDetect._touchedResource.Damaged(_weapon.GetPower());
    }

    private void MakeFX()
    {
        // Particle Effects
        ParticleSystem instance = Instantiate(_weapon.GetDamagedFX(), _weapon.GetHitPoint().position, _weapon.GetDamagedFX().transform.rotation, transform);
        FlipParticleRotation(instance);

        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);

        // Resource Shaking
        _hitDetect._touchedResource.ShakeResource();

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

    #endregion

    #region PublicMethods

    public void SetWeapon(Weapon weapon, HitDetect hitDetect)
    {
        _weapon = weapon;
        _hitDetect = hitDetect;
    }

    #endregion
}
