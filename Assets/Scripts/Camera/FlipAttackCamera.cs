using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAttackCamera : MonoBehaviour
{
    #region PrivateVariables

    CinemachineVirtualCamera _virtualCamera;
    PlayerController _playerController;

    #endregion

    #region PrivateMethods

    void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();    
        _playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        Flip();
    }

    void Flip()
    {
        _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = new Vector3(_playerController.transform.localScale.x * 3, 0f, 0f);
    }


    #endregion
}
