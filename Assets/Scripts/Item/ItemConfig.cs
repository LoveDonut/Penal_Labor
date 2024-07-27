using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Config", menuName = "Create Item Config SO")]
public class ItemConfig : ScriptableObject
{
    [SerializeField] float power;
    [SerializeField] int[] costs = new int[3];
    [SerializeField] int recovery;

    public float GetPower() { return  power; }
    public int GetRecovery() { return recovery; }
    public int[] GetCosts() { return costs; }

}
