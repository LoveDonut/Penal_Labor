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
        Vector2 newPosition = _myRigidbody.position + _moveInput * _moveSpeed * Time.fixedDeltaTime;
        _myRigidbody.MovePosition(newPosition);
    }

    void OnMove(InputValue input)
    {
        _moveInput = input.Get<Vector2>().normalized;
    }

    #endregion

}
