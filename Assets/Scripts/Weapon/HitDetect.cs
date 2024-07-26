using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetect : MonoBehaviour
{
    #region PrivateVariables

    Collider2D _myCollider;

    #endregion

    #region PublicVariables

    public bool _istoucingResource;
    [HideInInspector] public Resource _touchedResource;

    #endregion

    #region PublicMethods

    void Awake()
    {
        _myCollider = GetComponent<Collider2D>();
    }
    void Start()
    {

    }

    void Update()
    {

    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Resource"))
        {
            _istoucingResource = true;
            _touchedResource = collision.GetComponent<Resource>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _istoucingResource = false;
        _touchedResource = null;
    }
    */
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Resource"))
        {
            _istoucingResource = true;
            _touchedResource = collision.gameObject.GetComponent<Resource>();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        _istoucingResource = false;
        _touchedResource = null;
    }
    
    #endregion
}
