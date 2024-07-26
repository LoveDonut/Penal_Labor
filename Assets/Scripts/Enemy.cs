using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region PrivateVariables
    [SerializeField] GameObject _sightPoint;
    PolygonCollider2D _sightCollider;
    #endregion

    #region PrivateMethods

    void Awake()
    {
        _sightCollider = GetComponentInChildren<PolygonCollider2D>();    
    }

    void Start()
    {
        _sightPoint.transform.position = transform.position;
        _sightCollider.transform.localPosition = new Vector3(0,-2.5f,0);
    }

    void Update()
    {
    }
    #endregion



}
