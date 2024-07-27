using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    enum EResource
    {
        Coal,
        Tree,
        Iron
    }

    #region PrivateVariables
    [SerializeField] GameObject _sprites;
    [SerializeField] EResource _resourceType;
    [SerializeField] float _shakeDuration = 0.5f;
    [SerializeField] float _shakeMagnitude = 0.1f;
    [SerializeField] float _maxHp = 100;

    PlayerResourceManagement _resourceManagement;
    Vector3 _originalPosition;
    float _hp;
    float _resourceCriteria;
    #endregion

    #region PrivateMethods
    void Awake()
    {
        _resourceManagement = FindObjectOfType<PlayerResourceManagement>();
    }

    void Start()
    {
        _originalPosition = _sprites.transform.localPosition;
        _resourceCriteria = 0.9f;
        _hp = _maxHp;
    }

    void Update()
    {

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

    void GiveResource(int num)
    {
        switch (_resourceType)
        {
            case EResource.Coal:
                _resourceManagement.SetResourcesCount(num, 0, 0);
                break;
            case EResource.Tree:
                _resourceManagement.SetResourcesCount(0, num, 0);
                break;
            case EResource.Iron:
                _resourceManagement.SetResourcesCount(0, 0, num);
                break;
        }
    }

    #endregion

    #region PublicMethods
    public void ShakeResource()
    {
        StartCoroutine(ShakeCoroutine());
    }

    public void Damaged(float damage)
    {
        _hp -= damage;

        while(_hp / _maxHp <= _resourceCriteria && _resourceCriteria > 0f)
        {
            _resourceCriteria -= 0.1f;
            GiveResource(1);
        }

        if (_hp <= 0) 
        {
            GiveResource(11);
//            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
    #endregion
}
