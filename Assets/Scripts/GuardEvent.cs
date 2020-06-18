using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEvent : MonoBehaviour
{
    [SerializeField] private GameObject guard1;
    [SerializeField] private GameObject guard2;
    [SerializeField] private Transform stableDoor1;
    [SerializeField] private Transform stableDoor2;
    [SerializeField] private GameObject horse1;
    [SerializeField] private GameObject horse2;
    [SerializeField] private GameObject guardBlock;
    [SerializeField] private GameObject guardDialolgue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {      
        if (other.gameObject.tag == "Player" && Input.GetButtonDown("Interact"))
        {
            stableDoor1.rotation = Quaternion.Euler(0, 180, 0);
            stableDoor2.rotation = Quaternion.Euler(0, 180, 0);
            Destroy(horse1, 2f);
            Destroy(horse2, 2f);
            Destroy(guard1, 2f);
            Destroy(guard2, 2f);
            Destroy(guardBlock, 2f);
            Destroy(guardDialolgue, 2f);
        }
    }
}
