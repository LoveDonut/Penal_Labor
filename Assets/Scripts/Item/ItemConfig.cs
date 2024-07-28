using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Config", menuName = "Create Item Config SO")]
public class ItemConfig : ScriptableObject
{
    [SerializeField] float power;
    [SerializeField] int[] costs = new int[4];
    [SerializeField] int recovery;
    [SerializeField] PlayerWeaponManagement.EWeaponType _weaponType;

    public float GetPower() { return  power; }
    public int[] GetCosts() { return costs; }

    public PlayerWeaponManagement.EWeaponType GetWeaponType() { return _weaponType; }
}
