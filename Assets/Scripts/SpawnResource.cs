using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnResource : MonoBehaviour
{
    public void Respawn(GameObject resource, float time)
    {
        StartCoroutine(RespawnAfterTime(resource, time));
    }

    IEnumerator RespawnAfterTime(GameObject resource, float time)
    {
        yield return new WaitForSeconds(time);
        resource.gameObject.SetActive(true);
        resource.GetComponent<Resource>().RecoverHp();
    }
}
