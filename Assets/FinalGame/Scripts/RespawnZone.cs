using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RespawnZone : MonoBehaviour {
    public Transform RespawnPoint;

	void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            collider.gameObject.transform.position = RespawnPoint.position;
        }
    }
}
