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

    PlayerController _player;
    Rigidbody2D _enemyRigidbody;
    Coroutine _moveRoutine;

    bool _isChasing = true;
    #endregion

    #region PrivateMethods

    void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
    }
    void FixedUpdate()
    {
        if (!_isChasing) return;
        MoveTowardsPlayer();
        RotateEnemyByDirection();
    }
    void MoveTowardsPlayer()
    {
        Vector2 toTargetDirection = (_player.transform.position - transform.position).normalized;

        _enemyRigidbody.velocity = toTargetDirection * _moveSpeed;
    }



    void RotateEnemyByDirection()
    {
        if (_enemyRigidbody.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(_player.transform.position.y - transform.position.y, _player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
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
