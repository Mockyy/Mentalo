using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEvent : MonoBehaviour
{

    [SerializeField] private Transform chat;
    [SerializeField] private Transform block;

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
        if(other.gameObject.tag == "Player" && Input.GetButtonDown("Interact"))
        {
            gameObject.transform.Translate(new Vector3(3, 0));
            Destroy(chat, 1f);
            Destroy(block, 2f);
        }
    }
}
