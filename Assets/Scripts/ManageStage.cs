using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageStage : MonoBehaviour
{
    #region PrivateVariables
    [SerializeField] GameObject[] _resources;
    [SerializeField] GameObject[] _enemys;
    
    ChasePlayer _chaseEnemy;
    PlayerResourceManagement _resourceManagement;
    

    int _resourcesCount;
    #endregion

    public bool _isGatherEverything = false;

    void Awake()
    {
        _resourceManagement = FindObjectOfType<PlayerResourceManagement>();
        _chaseEnemy = GetComponentInChildren<ChasePlayer>();
    }

    void Start()
    {
        _resourcesCount = _resources.Length;
    }

    void ChasePlayer()
    {
        _isGatherEverything = true;
        foreach (GameObject enemy in _enemys)
        {
            enemy.SetActive(true);
        }
    }

    public void PlayerGoOut()
    {
        _isGatherEverything = false;
        _resourcesCount = _resources.Length;
        _resourceManagement.ResetTemporaryResources();
        _chaseEnemy.ResetChase();
        _resourceManagement.TriggerBigEyeBall(false);
        foreach (GameObject enemy in _enemys)
        {
            enemy.SetActive(false);
        }
        foreach (GameObject resource in _resources)
        {
            resource.SetActive(true);
            resource.GetComponent<Resource>().RecoverHp();
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

    public void DisappearResoource(GameObject resource)
    {
        resource.SetActive(false);
        _resourcesCount--;

        if(_resourcesCount <= 0)
        {
            ChasePlayer();
        }
    }
}
