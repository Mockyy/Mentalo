using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPickUp : ClosestItem
{
    [Header("Player Pick Up")]

    [Tooltip("Position of the hands")]
    [SerializeField]
    private Transform handsTransform = null;


    [SerializeField]
    [Tooltip("Debug : Is the player holding something")]
    private bool holding = false;
    private GameObject objectPickedUp = null;


    // Update is called once per frame
    void Update()
    {
        GameObject closestObject = GetClosestItem();

        if (Vector3.Distance(handsTransform.position, closestObject.transform.position) < 2f)
        {
            if (showLines)
            {
                Debug.DrawLine(handsTransform.position, closestObject.transform.position, Color.magenta);
            }

            if (Input.GetButtonDown("Interact"))
            {
                holding = !holding;

                if (holding)
                {
                    objectPickedUp = closestObject;

                    objectPickedUp.transform.parent = handsTransform;
                    objectPickedUp.transform.position = handsTransform.position;
                    objectPickedUp.GetComponent<Rigidbody>().useGravity = false;
                    objectPickedUp.GetComponent<BoxCollider>().enabled = false;
                }
                else
                {
                    objectPickedUp.transform.parent = null;
                    objectPickedUp.GetComponent<Rigidbody>().useGravity = true;
                    objectPickedUp.GetComponent<BoxCollider>().enabled = true;
                }
            }
        }
        else
        {
            if (showLines)
            {
                Debug.DrawLine(handsTransform.position, closestObject.transform.position, Color.red);
            }
        }
    }

   
}
