using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRestart : MonoBehaviour
{
    [SerializeField] private Transform restart;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Restart");
            other.transform.SetPositionAndRotation(restart.position, Quaternion.identity);
        }
    }
}
