using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    #region PrivateVariables
    [SerializeField] Transform _route;
    [SerializeField] GameObject _sightPoint;
    [SerializeField] GameObject _tie;
    [SerializeField] float _flashlightRotationSpeed = 1000f;
    [SerializeField] float _moveSpeed = 7f;
    [SerializeField] float _waitTime = 1f;

    PolygonCollider2D _sightCollider;
    Rigidbody2D _enemyRigidbody;
    List<Transform> _waypoints;

    int _currentWaypointIndex = 0;
    bool _isWaiting = false;
    Vector2 _movement;
    Vector2 _lastDirection;

    #endregion 

    #region PrivateMethods

    void Awake()
    {
        _sightCollider = GetComponentInChildren<PolygonCollider2D>();
        _enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _waypoints = new List<Transform>();
        for (int i = 0; i < _route.childCount; i++)
        {
            if (_route.GetChild(i) != null)
            {
                _waypoints.Add(_route.GetChild(i));
            }
        }
        if (_waypoints.Count > 0)
        {
            MoveTowardsWaypoint();
        }
    }

    void MoveTowardsWaypoint()
    {
        Transform targetWaypoint = _waypoints[_currentWaypointIndex];
        Vector2 direction = (targetWaypoint.position - transform.position).normalized;

        _enemyRigidbody.velocity = direction * _moveSpeed; // 물리적 이동
        _lastDirection = direction; // 마지막 이동 방향 저장

        // Waypoint에 도달했는지 체크
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            _enemyRigidbody.velocity = Vector2.zero; // Waypoint 도착 시 이동 멈춤
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count; // 다음 지점으로 이동
            StartCoroutine(WaitAtWaypoint()); // 지점에서 대기
        }
    }
    private IEnumerator WaitAtWaypoint()
    {
        _isWaiting = true;
        yield return new WaitForSeconds(_waitTime);
        _isWaiting = false;
    }

    void FixedUpdate()
    {
        if (_isWaiting) return;
        MoveTowardsWaypoint();
        RotateEnemyByDirection();
    }

    void RotateEnemyByDirection()
    {
        Transform targetWaypoint = _waypoints[_currentWaypointIndex];
        if (_enemyRigidbody.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(targetWaypoint.position.y - transform.position.y, targetWaypoint.position.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
            _sightPoint.transform.rotation = Quaternion.RotateTowards(_sightPoint.transform.rotation, targetRotation, _flashlightRotationSpeed * Time.deltaTime);
        }
    }
    #endregion


}
