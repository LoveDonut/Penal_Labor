using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageStage : MonoBehaviour
{
    #region PrivateVariables
    [SerializeField] GameObject[] _enemys;
    [SerializeField] ScreenFlash _screenFlash;

    Resource[] _resources;
    ChasePlayer[] _chaseEnemys;
    PlayerResourceManagement _resourceManagement;


    int _resourcesCount;
    #endregion

    public bool _isGatherEverything = false;

    void Awake()
    {
        _resourceManagement = FindObjectOfType<PlayerResourceManagement>();
        _chaseEnemys = GetComponentsInChildren<ChasePlayer>();
        _resources = GetComponentsInChildren<Resource>();
    }

    void Start()
    {
        _resourcesCount = _resources.Length;
    }

    void WakeupChaseEnemy()
    {
        _isGatherEverything = true;
        _screenFlash.FlashScreen();
    }

    void WakeupMovingEnemys()
    {
        foreach (GameObject enemy in _enemys)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
            }
        }
    }

    public void PlayerGoOut()
    {
        _isGatherEverything = false;
        _resourcesCount = _resources.Length;
        _resourceManagement.ResetTemporaryResources();
        foreach (ChasePlayer chaseEnemy in  _chaseEnemys)
        {
            chaseEnemy.ResetChase();
        }
        _resourceManagement.TriggerBigEyeBall(false);
        foreach (GameObject enemy in _enemys)
        {
            enemy.SetActive(false);
        }
        foreach (Resource resource in _resources)
        {
            resource.gameObject.SetActive(true);
            resource.RecoverHp();
        }
    }

    public void StartChaseAfterGathering()
    {
        _isGatherEverything = true;
    }

    public void EndChaseAfterCatching()
    {
        _isGatherEverything = false;
    }

    public void DisappearOneResoource(GameObject resource)
    {
        resource.SetActive(false);
        _resourcesCount--;

        WakeupMovingEnemys();

        if (_resourcesCount <= 0)
        {
            WakeupChaseEnemy();
        }
    }

    public void GiveStageClearReward()
    {
        _resourceManagement.GiveResourcesTwice();
    }
}
