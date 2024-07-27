using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    #region PrivateVariables
    [SerializeField] GameObject _sprites;
    [SerializeField] float _shakeDuration = 0.5f;
    [SerializeField] float _shakeMagnitude = 0.1f;
    [SerializeField] float _maxHp = 100;

    Vector3 _originalPosition;
    float _hp;
    #endregion

    #region PrivateMethods
    void Awake()
    {
    }

    void Start()
    {
        _originalPosition = _sprites.transform.localPosition;
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
    #endregion

    #region PublicMethods
    public void ShakeResource()
    {
        StartCoroutine(ShakeCoroutine());
    }

    public void Damaged(float damage)
    {
        _hp -= damage;

        if (_hp <= 0) 
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
