using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetect : MonoBehaviour
{
    #region PrivateVariables

    CircleCollider2D _myCollider;

    #endregion

    #region PublicVariables

    public bool _istoucingResource;
    public Resource _touchedResource;

    #endregion

    #region PublicMethods

    void Awake()
    {
        _myCollider = GetComponent<CircleCollider2D>();
    }
    void Start()
    {

    }

    void Update()
    {
        //if (_myCollider.IsTouchingLayers(LayerMask.GetMask("Resource")))
        //{
        //    _istoucingResource = true;
        //}
        //else
        //{
        //    _istoucingResource = false;
        //}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _istoucingResource = true;
        _touchedResource = collision.gameObject.GetComponent<Resource>();
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        _istoucingResource = false;
        _touchedResource = null;
    }
    #endregion
}
