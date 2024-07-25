using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region PrivateVariables

    [SerializeField] float _moveSpeed = 5f;
    Vector2 _moveInput;
    Rigidbody2D _myRigidbody;


    #endregion

    #region PrivateMethods

    void Awake()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();    
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        _myRigidbody.velocity = _moveInput * _moveSpeed;
    }

    void OnMove(InputValue input)
    {
        _moveInput = input.Get<Vector2>().normalized;
        FlipPlayer(_moveInput.x);
    }

    void FlipPlayer(float valueX)
    {
        if (valueX == 0) return;
        if(Mathf.Sign(transform.localScale.x) != Mathf.Sign(valueX))
        {
            transform.localScale = new Vector3(Mathf.Sign(valueX), 1f, 1f);
        }
    }

    #endregion

}
