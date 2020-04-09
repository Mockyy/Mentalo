using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoupDeLangue : MonoBehaviour
{
    [Tooltip("Transform the tongue comes out of")]
    [SerializeField]
    private Transform tongueTransform = null;


    [Tooltip("Max range of the tongue")]
    [SerializeField]
    private float range = 5f;

    [SerializeField]
    private GameObject soundLangue;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Tongue"))
        {
            soundLangue.GetComponent<AudioSource>().Play();

            RaycastHit hit;

            float offset = Camera.main.transform.rotation.x;

            Vector3 camDirection = new Vector3(0, -offset, 0);
            camDirection += transform.forward;

            Debug.DrawRay(tongueTransform.position, camDirection * range, Color.black);

            if (Physics.Raycast(tongueTransform.position, camDirection, out hit, range))
            {

                if (hit.transform.GetComponent<TongueButton>() != null)
                {
                    hit.transform.GetComponent<TongueButton>().Active();
                }
            }
        }
    }
}
