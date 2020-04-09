using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Transform objectNeeded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == objectNeeded)
        {
            if (Input.GetButtonDown("Interact"))
            {
                Destroy(other);
                objectNeeded = null;
            }
        }
    }
}
