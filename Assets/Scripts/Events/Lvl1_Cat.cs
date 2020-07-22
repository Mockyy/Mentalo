using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Permet de gérer l'évenement du chat dans le niveau 1
public class Lvl1_Cat : MonoBehaviour
{

    [SerializeField] private GameObject chat = default;   //Le chat qui disparaitera
    [SerializeField] private GameObject block = default;  //Le mur qui bloque la progression

    //Quand le joueur entre dans le trigger de l'objet, affiche la commande d'action
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    //Quand le joueur quitte le trigger de l'objet, enlève la commande d'action
    private void OnTriggerExit(Collider other)
    { 
        if (other.transform.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Si le joueur est dans le trigger et appuie sur la touche d'interaction
        if (other.transform.tag == "Player" && Input.GetButtonDown("Interact"))
        {
            gameObject.transform.Translate(new Vector3(3, 0));  //Déplace l'objet dans le vide
            Destroy(chat, 1f);                                  //Supprime le chat et son mur
            Destroy(block, 2f);
            GetComponent<AudioSource>().PlayDelayed(1);         //Joue le son de bris de verre
        }
    }
}
