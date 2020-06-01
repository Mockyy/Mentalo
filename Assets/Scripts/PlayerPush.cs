using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : ClosestItem 
{
    [SerializeField]
    //private bool isPushing = false;

    private GameObject pushObject;


    private void Update()
    {
        ScanPushableObjects();        
    }

    private void ScanPushableObjects()
    {
        pushObject = GetClosestItem();

        if (pushObject != null)
        {
            float distanceToPushObject = Vector3.Distance(transform.position, pushObject.transform.position);

            if (distanceToPushObject < 3f)
            {
                float newDistance = distanceToPushObject;

                Debug.DrawLine(transform.position, pushObject.transform.position, Color.cyan);

                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, newDistance) && Input.GetButtonDown("Interact"))
                {
                    Vector3 newDirection = -hit.normal;
                    Push(pushObject, newDirection);
                }
            }
        }

        
    }

    private void Push(GameObject objectPushed, Vector3 direction)
    {
        //objectPushed.GetComponent<Rigidbody>().AddForce(transform.forward * 2);
        //objectPushed.GetComponent<Rigidbody>().velocity = transform.forward * 10;
        objectPushed.transform.Translate(transform.forward * 2f);
    }
}
