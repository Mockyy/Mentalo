using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupParticules : MonoBehaviour
{
    private Rigidbody rb;

    private ParticleSystem dust;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dust = GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            print("dust");
            dust.Play();
        }
    }
}
