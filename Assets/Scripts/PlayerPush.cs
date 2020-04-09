using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour 
{
    [SerializeField]
    //private bool isPushing = false;

    private GameObject[] objectsCanBePushed;


    private void Update()
    {
        ScanPushableObjects();        
    }

    private void ScanPushableObjects()
    {
        objectsCanBePushed = GameObject.FindGameObjectsWithTag("Pushable");

        foreach (GameObject objectPushed in objectsCanBePushed)
        {
            if (Vector3.Distance(transform.position, objectPushed.transform.position) < 3f)
            {
                Debug.DrawLine(transform.position, objectPushed.transform.position, Color.cyan);

                if (Input.GetButtonDown("Interact"))
                {
                    Push(objectPushed);
                    
                }
            }
        }
    }

    private void Push(GameObject objectPushed)
    {
        //objectPushed.GetComponent<Rigidbody>().AddForce(transform.forward * 2);
        //objectPushed.GetComponent<Rigidbody>().velocity = transform.forward * 10;
        objectPushed.transform.Translate(transform.forward * 2f);

        print("Push");
    }
}
