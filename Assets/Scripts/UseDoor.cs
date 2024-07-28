using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseDoor : MonoBehaviour
{
    #region PrivateVariables
    [SerializeField] GameObject _portal;
    [SerializeField] GameObject _chaseEnemy;
    #endregion

    #region PrivateMethods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.transform.position = _portal.transform.position + new Vector3(0,-5f,0);
            _chaseEnemy.SetActive(!_chaseEnemy.activeInHierarchy);
        }
    }

    #endregion
}
