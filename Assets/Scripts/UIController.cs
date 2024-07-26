using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    #region PrivateVariables
    [SerializeField] GameObject _itemBuyUI;
    PlayerController _playerController;
    #endregion

    #region PrivateMethods

    void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    #endregion

    #region PublicMethods

    public void SetItemBuyUI(bool b)
    {
        _itemBuyUI.SetActive(b);
        _playerController._isActive = true;
    }
    #endregion
}
