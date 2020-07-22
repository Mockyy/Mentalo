using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gère l'event de l'ouverture de porte dans le tutoriel
public class Tutorial_Door : MonoBehaviour
{
    [SerializeField] DialogueTrigger quest = default;

    // Update is called once per frame
    void Update()
    {
        if (quest.questIsCompleted)
        {
            Destroy(gameObject);
        }
    }
}
