using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public enum EResource
    {
        Coal,
        Tree,
        Iron,
        Gold
    }

    #region PrivateVariables
    [SerializeField] GameObject _sprites;
    [SerializeField] EResource _resourceType;
    [SerializeField] Slider _hpProgressBar;
    //[SerializeField] float _respawnTime = 5f;
    [SerializeField] float _shakeDuration = 0.5f;
    [SerializeField] float _shakeMagnitude = 0.1f;
    [SerializeField] float _maxHp = 100;
    [SerializeField] ParticleSystem _goldParticle;
    
    ManageStage _stageManager;
    PlayerResourceManagement _resourceManagement;
    SpawnResource _spawnManager;
    Vector3 _originalPosition;
    float _hp;
    //float _resourceCriteria;
    int goldRate = 5;
    #endregion

    #region PrivateMethods
    void Awake()
    {
        _resourceManagement = FindObjectOfType<PlayerResourceManagement>();
        _spawnManager = FindAnyObjectByType<SpawnResource>();
        _stageManager = GetComponentInParent<ManageStage>();
    }

    void Start()
    {
        _originalPosition = _sprites.transform.localPosition;
        //_resourceCriteria = 0.9f;
        _hp = _maxHp;
        _hpProgressBar.maxValue = _maxHp;
    }

    void Update()
    {
        _hpProgressBar.value = _hp;
    }
    void GiveResource(int num)
    {
        int percent = UnityEngine.Random.Range(0, goldRate);
        if (percent == 0)
        {
            _resourceManagement.GatherResources(0, 0, 0, 1);
            ParticleSystem instance = Instantiate(_goldParticle, transform.position, _goldParticle.transform.rotation, transform);
            Destroy(instance, instance.main.duration + instance.main.startLifetime.constantMax);
        }
        switch (_resourceType)
        {
            case EResource.Coal:
                _resourceManagement.GatherResources(num, 0, 0, 0);
                break;
            case EResource.Tree:
                _resourceManagement.GatherResources(0, num, 0, 0);
                break;
            case EResource.Iron:
                _resourceManagement.GatherResources(0, 0, num, 0);
                break;
        }
    }

    IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;

        while (elapsed < _shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * _shakeMagnitude;
            float y = Random.Range(-1f, 1f) * _shakeMagnitude;

            _sprites.transform.localPosition = new Vector3(_originalPosition.x + x, _originalPosition.y + y, _originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        _sprites.transform.localPosition = _originalPosition;
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Player"))
    //    {
    //        RecoverHp();
    //    }
    //}

    #endregion

    #region PublicMethods
    public void ShakeResource()
    {
        StartCoroutine(ShakeCoroutine());
    }

    public void Damaged(float damage)
    {
        _hp -= damage;

        //while(_hp / _maxHp <= _resourceCriteria && _resourceCriteria > 0f)
        //{
        //    _resourceCriteria -= 0.1f;
        //    GiveResource(1);
        //}

        if (_hp <= 0) 
        {
            GiveResource(10);
            _stageManager.DisappearOneResoource(gameObject);
        }
    }

    public void RecoverHp()
    {
        _hp = _maxHp;
        //_resourceCriteria = 0.9f;
    }

    public EResource GetResourceType()
    {
        return _resourceType;
    }
    #endregion
}
