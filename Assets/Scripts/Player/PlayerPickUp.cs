using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPickUp : ClosestItem
{
    [Header("Player Pick Up")]

    [Tooltip("Position des mains")]
    [SerializeField]
    private Transform handsTransform = null;

    [SerializeField]
    [Tooltip("Debug : Est-ce que le personnage tient quelque chose")]
    private bool holding = false;

    private GameObject objectPickedUp = null;   //L'objet tenu


    // Update is called once per frame
    void Update()
    {
        GameObject closestObject = GetClosestItem();    //L'objet le plus proche

        //Si le personnage est à moins de 2 unités de distance de l'objet
        if (closestObject != null && Vector3.Distance(handsTransform.position, closestObject.transform.position) < 2f)
        {
            //Si l'option pour montrer les lignes est active
            if (showLines)
            {
                //On dessine la ligne en le personnage et l'objet
                Debug.DrawLine(handsTransform.position, closestObject.transform.position, Color.magenta);
            }

            //Si le joueur appuie sur la touche d'interaction
            if (Input.GetButtonDown("Interact"))
            {
                //On change l'état de "holding"
                holding = !holding;

                //Si le personnage attrape quelque chose
                if (holding)
                {
                    //L'objet le plus proche devient l'objet tenu
                    objectPickedUp = closestObject;

                    objectPickedUp.transform.parent = handsTransform;               //L'objet devient l'enfant des mains
                    objectPickedUp.transform.position = handsTransform.position;    //L'objet se met à la position des mains
                    objectPickedUp.GetComponent<Rigidbody>().useGravity = false;    //On désactive la gravité sur l'objet
                    objectPickedUp.GetComponent<BoxCollider>().enabled = false;     //On désactive la collision de l'objet
                }
                //Si le personnage lache l'objet
                else
                {
                    objectPickedUp.transform.parent = null;                         //On le rend orphelin
                    objectPickedUp.GetComponent<Rigidbody>().useGravity = true;     //On active la gravité
                    objectPickedUp.GetComponent<BoxCollider>().enabled = true;      //On active la collision
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
