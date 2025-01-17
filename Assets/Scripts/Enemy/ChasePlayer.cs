using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    #region PrivateVariables
    [SerializeField] GameObject _sightPoint;
    [SerializeField] float _flashlightRotationSpeed = 1000f;
    [SerializeField] int _randomTimeMin;
    [SerializeField] int _randomTimeMax;
    
    ManageStage _stageManager;
    PlayerAttack _player;
    Rigidbody2D _enemyRigidbody;
    Coroutine _moveRoutine;

    Vector3 _destination;
    Vector3 _originalLocation;

    NavMeshAgent _agent;

    //bool _isChasing = false;
    //bool _isReturning = false;
    public bool _isChaseStart = false;
    bool _isArrive = true;
    #endregion

    #region PrivateMethods

    void Awake()
    {
        _stageManager = GetComponentInParent<ManageStage>();
        _player = FindObjectOfType<PlayerAttack>();
        _enemyRigidbody = GetComponent<Rigidbody2D>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    void Start()
    {
        _destination = _originalLocation = transform.position;
    }
    void Update()
    {
        if(!_stageManager._isInRoom)
        {
            _agent.SetDestination(_originalLocation);
            return;
        }
        if (_stageManager._isGatherEverything)
        {
            if (_moveRoutine != null)
            {
                StopCoroutine(_moveRoutine);
            }
            _destination = _player.transform.position;
        }
        else
        {
            if (!_isChaseStart && _isArrive)
            {
                StartChasing();
            }
            else if(_isChaseStart && !_isArrive)
            {
                if (Vector3.Distance(transform.position, _destination) < 2f)
                {
                    ResetChase();
                }
            }
        }
        _agent.SetDestination(_destination);
        RotateEnemyByDirection();
    }
    void StartChasing()
    {
        if(_player.GetIsGathering())
        {
            _isChaseStart = true;
            _moveRoutine = StartCoroutine(Chase());
        }
    }

    IEnumerator Chase()
    {
        yield return new WaitForSeconds(Random.Range(_randomTimeMin, _randomTimeMax));
        if (!_stageManager._isGatherEverything)
        {
            _destination = _player.transform.position;
        }
        _isArrive = false;
        Debug.Log("Save Player Position when he gathers");
    }

    public void ResetChase()
    {
        _isChaseStart = false;
        _isArrive = true;
        _destination = _originalLocation;
    }
    //void StartChasing()
    //{
    //    if(_player.GetIsGathering() && !_isChasing)
    //    {
    //        if(_moveRoutine != null && _isReturning )
    //        {
    //            _isReturning = false;
    //            StopCoroutine(_moveRoutine);
    //        }

    //        _moveRoutine = StartCoroutine(Chase());
    //    }
    //}

    //IEnumerator Chase()
    //{
    //    _isChasing = true;
    //    yield return new WaitForSeconds(UnityEngine.Random.Range(_randomTimeMin, _randomTimeMax));
    //    _destination = _player.transform.position;
    //    _agent.SetDestination(_destination);

    //    // move to destination
    //    while (Vector3.Distance(_destination, transform.position) > 2f)
    //    {
    //        yield return new WaitForFixedUpdate();
    //    }

    //    _isChasing = false;
    //    _isReturning = true;

    //    _destination = _originalLocation;
    //    _agent.SetDestination(_destination);
    //    // return original location
    //    while (Vector3.Distance(_destination, transform.position) > 0.5f)
    //    {
    //        yield return new WaitForFixedUpdate();
    //    }

    //    _isReturning = false;
    //}

    void RotateEnemyByDirection()
    {
        float angle = Mathf.Atan2(_destination.y - transform.position.y, _destination.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
        _sightPoint.transform.rotation = Quaternion.RotateTowards(_sightPoint.transform.rotation, targetRotation, _flashlightRotationSpeed * Time.deltaTime);
    }
    #endregion

    #region PublicMethods


    #endregion
}
