using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    #region PrivateVariables
    [SerializeField] GameObject _sightPoint;
    [SerializeField] GameObject _tie;
    [SerializeField] float _flashlightRotationSpeed = 1000f;
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] Tuple<int, int> _randomTimeRange;

    PlayerAttack _player;
    Rigidbody2D _enemyRigidbody;
    Coroutine _moveRoutine;
    Vector3 _destination;
    Vector3 _originalLocation;

    bool _isChasing = false;
    bool _isReturning = false;
    #endregion

    #region PrivateMethods

    void Awake()
    {
        _player = FindObjectOfType<PlayerAttack>();
        _enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _originalLocation = transform.position;
    }
    void Update()
    {
        StartChasing();
    }

    void StartChasing()
    {
        if(_player.GetIsGathering() && !_isChasing)
        {
            if (_moveRoutine != null && _isReturning)
            {
                StopCoroutine(_moveRoutine);
            }
            _destination = _player.transform.position;
            _isChasing = true;
            _moveRoutine = StartCoroutine(Chase());
        }
    }

    IEnumerator Chase()
    {
        bool isArrived = false;

        // move to destination
        while(!isArrived)
        {
            MoveTowards(_destination, ref isArrived);
            RotateEnemyByDirection(_destination);
            yield return new WaitForFixedUpdate();
        }

        _isChasing = false;
        _isReturning = true;
        isArrived = false;

        // return original location
        while (!isArrived)
        {
            Debug.Log("Returning!");
            MoveTowards(_originalLocation, ref isArrived);
            RotateEnemyByDirection(_originalLocation);
            yield return new WaitForFixedUpdate();
        }

        _isReturning = false;

    }

    void MoveTowards(Vector3 destination, ref bool isArrived)
    {
        Vector2 toTargetDirection = (destination - transform.position).normalized;

        _enemyRigidbody.velocity = toTargetDirection * _moveSpeed;

        if(Vector3.Distance(destination, transform.position) < 1f)
        {
            isArrived = true;
            Debug.Log("Arrived to Destination!");
        }
    }

    void RotateEnemyByDirection(Vector3 destination)
    {
        if (_enemyRigidbody.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(destination.y - transform.position.y, destination.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
            _sightPoint.transform.rotation = Quaternion.RotateTowards(_sightPoint.transform.rotation, targetRotation, _flashlightRotationSpeed * Time.deltaTime);
        }
    }
    #endregion

    #region PublicMethods

    public void SetIsChasing(bool ischasing)
    {
        _isChasing = ischasing;
    }

    #endregion
}
