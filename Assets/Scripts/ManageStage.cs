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
    RemovePlayerWeapon[] _enemyFlashs;


    int _resourcesCount;
    #endregion

    public bool _isGatherEverything = false;
    public bool _isInRoom = false;

    void Awake()
    {
        _resourceManagement = FindObjectOfType<PlayerResourceManagement>();
        _chaseEnemys = GetComponentsInChildren<ChasePlayer>();
        _resources = GetComponentsInChildren<Resource>();
        _enemyFlashs = GetComponentsInChildren<RemovePlayerWeapon>();
    }

    void Start()
    {
        _resourcesCount = _resources.Length;
    }

    IEnumerator WakeupChaseEnemy()
    {
        yield return new WaitForSeconds(1f);
        _isGatherEverything = true;
        _screenFlash.FlashScreen(GetComponent<ManageStage>());
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

    void ResetCoolDown()
    {
        foreach (RemovePlayerWeapon enemyFlash in _enemyFlashs)
        {
            enemyFlash._isCoolDown = false;
        }
    }

    public void PlayerGoOut()
    {
        _isInRoom = false;
        _isGatherEverything = false;
        _resourcesCount = _resources.Length;
        _resourceManagement.ResetTemporaryResources();
        ResetCoolDown();
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
            StartCoroutine(WakeupChaseEnemy());
        }
    }

    public void GiveStageClearReward()
    {
        _resourceManagement.GiveResourcesTwice();
    }
}
