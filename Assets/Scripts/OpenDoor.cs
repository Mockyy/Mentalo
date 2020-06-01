using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] QuestManager quest;

    // Update is called once per frame
    void Update()
    {
        if (quest.isCompleted)
        {
            Destroy(gameObject);
        }
    }
}
