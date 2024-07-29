using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseDoor : MonoBehaviour
{
    #region PrivateVariables
    [SerializeField] GameObject _portal;
    [SerializeField] ManageStage _DestinationStage;
    bool _isDoorInStage;
    ManageStage _stageManager;
    #endregion


    #region PrivateMethods

    void Awake()
    {
        _stageManager = GetComponentInParent<ManageStage>();
    }

    void Start()
    {
        if(_stageManager != null)
        {
            _isDoorInStage = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (_isDoorInStage)
            {
                if (_stageManager._isGatherEverything) // if player gather everything, then give resources twice
                {
                    _stageManager.GiveStageClearReward();
                }
                collision.transform.position = Vector3.zero;
                _stageManager.PlayerGoOut();
            }
            else if(!_isDoorInStage)
            {
                collision.transform.position = _portal.transform.position + new Vector3(0, -5f, 0);
                _DestinationStage._isInRoom = true;
            }
        }
    }

    #endregion
}
