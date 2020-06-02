using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectPushable : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            print("lol");
            RaycastHit hit;
            Physics.Raycast(collision.transform.position, transform.position, out hit);

            transform.Translate(-hit.normal);
        }
    }
}
