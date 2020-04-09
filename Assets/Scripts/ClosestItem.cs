using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestItem : MonoBehaviour
{
    [Header("Closest Item")]
    [Tooltip("The tag associated with the wanted gameObjects")]
    [SerializeField]
    protected string wantedTag;

    [Tooltip("Base distance at which the objects are scanned")]
    [SerializeField]
    protected float distance = 5f;

    [Tooltip("Debugging : show the lines between all the objects")]
    [SerializeField]
    protected bool showLines = false;


    protected GameObject GetClosestItem()
    {
        GameObject[] objectsCanBePickedUp;

        objectsCanBePickedUp = GameObject.FindGameObjectsWithTag(wantedTag);

        GameObject closestObject = null;

        float distance = Mathf.Infinity;

        foreach (GameObject prop in objectsCanBePickedUp)
        {
            if (Vector3.Distance(transform.position, prop.transform.position) < distance)
            {
                closestObject = prop;
                distance = Vector3.Distance(transform.position, closestObject.transform.position);
            }
        }

        return closestObject;
    }
}
