using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerResourceManagement : MonoBehaviour
{
    #region PriavteVariables
    PlayerWeaponManagement _weaponManagement;
    [SerializeField] List<TextMeshProUGUI> resourceTexts;
    #endregion

    #region PublicVariables

    public int _coalCount;
    public int _treeCount;
    public int _ironCount;

    #endregion

    void Awake()
    {
        _weaponManagement = FindObjectOfType<PlayerWeaponManagement>();
    }

    // Start is called before the first frame update
    void Start()
    {
//        resourceTexts = new List<TextMeshProUGUI>();
        DisplayResourceUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayResourceUI()
    {
        if (resourceTexts.Count == 3)
        {
            Debug.Log("Update Resource display!");
            resourceTexts[0].SetText($": {_coalCount}");
            resourceTexts[1].SetText($": {_treeCount}");
            resourceTexts[2].SetText($": {_ironCount}");
        }
        else
        {
            Debug.Log("Not Update Resource display...");
        }
    }

    #region PublicMethods

    public void BuyItem(int index)
    {
        int[] costs = _weaponManagement._weapons[index].GetComponent<Weapon>().GetCosts();
        if (_coalCount >= costs[0] && _treeCount >= costs[1] && _ironCount >= costs[2])
        {
            _coalCount-= costs[0];
            _treeCount-= costs[1];
            _ironCount-= costs[2];
            DisplayResourceUI();
            _weaponManagement.AcquireWeapon(index);
        }
    }

    public void SetResourcesCount(int coal, int tree, int iron)
    {
        _coalCount += coal;
        _treeCount += tree;
        _ironCount += iron;
        DisplayResourceUI();
    }
    #endregion
}
