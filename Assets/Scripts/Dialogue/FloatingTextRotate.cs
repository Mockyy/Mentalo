using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Permet à un texte GameObject de toujours faire face à l'ecran
public class FloatingTextRotate : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}
