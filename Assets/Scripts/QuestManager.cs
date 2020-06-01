using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public bool isCompleted = false;

    [SerializeField] private Transform objectNeeded;

    private void OnTriggerEnter(Collider other)
    {
        print("Bonjour");

        if (other.transform.name == objectNeeded.name)
        {
            print("Quest");
            Destroy(other);
            objectNeeded = null;
            isCompleted = true;
        }
    }
}
