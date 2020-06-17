using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] DialogueTrigger quest;

    // Update is called once per frame
    void Update()
    {
        if (quest.questIsCompleted)
        {
            Destroy(gameObject);
        }
    }
}
