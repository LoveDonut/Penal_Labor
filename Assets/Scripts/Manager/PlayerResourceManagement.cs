using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class PlayerResourceManagement : MonoBehaviour
{
    #region PriavteVariables
    [SerializeField] List<TextMeshProUGUI> resourceTexts;
    [SerializeField] float _bigEyeTime;
    [SerializeField] List<CinemachineVirtualCamera> _virtualCameras;
    [SerializeField] int[] _bigEyeballPrice = new int[3];
    [SerializeField] int[] _gameClearPrice = new int[3];

    PlayerWeaponManagement _weaponManagement;
    int _temporaryCoal;
    int _temporaryTree;
    int _temporaryIron;
    int _temporaryGold;
    bool _isOpenEye = false;

    #endregion

    #region PublicVariables

    public int _coalCount;
    public int _treeCount;
    public int _ironCount;
    public int _goldCount;

    #endregion

    void Awake()
    {
        _weaponManagement = FindObjectOfType<PlayerWeaponManagement>();
    }

    void Start()
    {
        DisplayResourceUI();
    }

    void DisplayResourceUI()
    {
        if (resourceTexts.Count == 4)
        {
            resourceTexts[0].SetText($": {_coalCount}");
            resourceTexts[1].SetText($": {_treeCount}");
            resourceTexts[2].SetText($": {_ironCount}");
            resourceTexts[3].SetText($": {_goldCount}");
        }
    }

    void GameClear()
    {
        Debug.Log("Game Clear!");
    }

    #region PublicMethods

    public void BuyItem(int index)
    {
        if (_weaponManagement._weaponExists[(PlayerWeaponManagement.EWeaponType)index]) return;
        int[] costs = _weaponManagement._weapons[index].GetComponent<Weapon>().GetCosts();
        Debug.Log($"Price is {costs[0]} / {costs[1]} / {costs[2]} / {costs[3]}");
        if (_coalCount >= costs[0] && _treeCount >= costs[1] && _ironCount >= costs[2] && _goldCount >= costs[3])
        {
            _coalCount-= costs[0];
            _treeCount-= costs[1];
            _ironCount-= costs[2];
            _goldCount-= costs[3];
            DisplayResourceUI();
            _weaponManagement.AcquireWeapon(index);
        }
    }

    public void BuyActiveItem(int index)
    {
        int[] costs;
        if(index == 5)
        {
            if (_isOpenEye) return;
            costs = _bigEyeballPrice;
        }
        else
        {
            costs = _gameClearPrice;
        }

        Debug.Log($"Price is {costs[0]} / {costs[1]} / {costs[2]}");

        if (_coalCount >= costs[0] && _treeCount >= costs[1] && _ironCount >= costs[2])
        {
            _coalCount -= costs[0];
            _treeCount -= costs[1];
            _ironCount -= costs[2];
            DisplayResourceUI();
            if(index == 5)
            {
                TriggerBigEyeBall(true);
            }
            else
            {
                GameClear();
            }
        }
    }
    public void TriggerBigEyeBall(bool _isEat)
    {
        foreach (CinemachineVirtualCamera vCamera in _virtualCameras)
        {
            if (_isEat)
            {
                vCamera.m_Lens.OrthographicSize = 8f;
                _isOpenEye = true;
            }
            else
            {
                vCamera.m_Lens.OrthographicSize = 4f;
                _isOpenEye = false;
            }
        }
    }

    public void GatherResources(int coal, int tree, int iron, int gold)
    {
        _coalCount += coal;
        _treeCount += tree;
        _ironCount += iron;
        _goldCount += gold;
        _temporaryCoal += coal;
        _temporaryTree += tree;
        _temporaryIron += iron;
        _temporaryGold += gold;
        DisplayResourceUI();
    }

    public void RemoveResources()
    {
        _coalCount -= _temporaryCoal;
        _treeCount -= _temporaryTree;
        _ironCount -= _temporaryIron;
        _goldCount -= _temporaryGold;
        DisplayResourceUI();
    }

    public void ResetTemporaryResources()
    {
        _temporaryCoal = 0;
        _temporaryTree = 0;
        _temporaryIron = 0;
        _temporaryGold = 0;
    }

    #endregion
}
