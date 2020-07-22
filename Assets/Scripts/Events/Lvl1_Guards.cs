using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gère l'event des gardes du niveau 1
public class Lvl1_Guards : MonoBehaviour
{
    //Tout les objets à faire disparaitre
    [SerializeField] private GameObject guard1 = default;
    [SerializeField] private GameObject guard2 = default;
    [SerializeField] private Transform stableDoor1 = default;
    [SerializeField] private Transform stableDoor2 = default;
    [SerializeField] private GameObject horse1 = default;
    [SerializeField] private GameObject horse2 = default;
    [SerializeField] private GameObject guardBlock = default;
    [SerializeField] private GameObject guardDialolgue = default;

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
        //Si on appuie sur la touche d'interaction
        if (other.gameObject.tag == "Player" && Input.GetButtonDown("Interact"))
        {
            //On rotate des portes pour les ouvrir
            stableDoor1.rotation = Quaternion.Euler(0, 180, 0);
            stableDoor2.rotation = Quaternion.Euler(0, 180, 0);
            
            //On détruit tout les autres objets
            Destroy(horse1, 2f);                                
            Destroy(horse2, 2f);
            Destroy(guard1, 2f);
            Destroy(guard2, 2f);
            Destroy(guardBlock, 2f);
            Destroy(guardDialolgue, 2f);
        }
    }
}
