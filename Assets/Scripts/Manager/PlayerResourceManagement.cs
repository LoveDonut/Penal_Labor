using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class PlayerResourceManagement : MonoBehaviour
{
    #region PriavteVariables
    PlayerWeaponManagement _weaponManagement;
    [SerializeField] List<TextMeshProUGUI> resourceTexts;
    [SerializeField] float _bigEyeTime;
    [SerializeField] List<CinemachineVirtualCamera> _virtualCameras;
    [SerializeField] int[] _bigEyeballPrice = new int[3];
    [SerializeField] int[] _gameClearPrice = new int[3];
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

    IEnumerator UseBigEyeBall()
    {
        foreach(CinemachineVirtualCamera vCamera in _virtualCameras)
        {
            vCamera.m_Lens.OrthographicSize *= 1.5f;
        }
        yield return new WaitForSeconds(_bigEyeTime);
        foreach (CinemachineVirtualCamera vCamera in _virtualCameras)
        {
            vCamera.m_Lens.OrthographicSize /= 1.5f;
        }

    }

    void GameClear()
    {
        Debug.Log("Game Clear!");
    }

    #region PublicMethods

    public void BuyItem(int index)
    {
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
                StartCoroutine(UseBigEyeBall());
            }
            else
            {
                GameClear();
            }
        }
    }

    public void SetResourcesCount(int coal, int tree, int iron, int gold)
    {
        _coalCount += coal;
        _treeCount += tree;
        _ironCount += iron;
        _goldCount += gold;
        DisplayResourceUI();
    }
    #endregion
}
