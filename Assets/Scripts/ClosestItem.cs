using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Récupère l'objet le plus proche parmi ceux qui ont le même tag
public class ClosestItem : MonoBehaviour
{
    [Header("Closest Item")]
    [Tooltip("The tag associated with the wanted gameObjects")]
    [SerializeField]
    protected string wantedTag; //Le tag des objets que l'on cherche

    [Tooltip("Base distance at which the objects are scanned")]
    [SerializeField]
    protected float distance = 5f;  //La distance maximale à laquelle les objets sont repérés

    [Tooltip("Debugging : show the lines between all the objects")]
    [SerializeField]
    protected bool showLines = false;   //Debug : Affiche les lignes entre cet objet et les objets repérés
    
    protected GameObject GetClosestItem()
    {
        GameObject[] objectsCanBePickedUp;  //Un tableau de gameObjects

        objectsCanBePickedUp = GameObject.FindGameObjectsWithTag(wantedTag);    //Mets tout les objets avec le tag recherché 
                                                                                //dans le tableau

        GameObject closestObject = null;    //L'objet le plus proche est pour l'instant vide

        float distance = Mathf.Infinity;    //La distance avec l'objet le plus proche est pour l'instant infinie

        //Pour chaque objet dans le tableau
        foreach (GameObject prop in objectsCanBePickedUp)
        {
            //Si la distance avec cet objet est plus petite que la distance avec l'objet le plus proche
            if (Vector3.Distance(transform.position, prop.transform.position) < distance)
            {
                closestObject = prop;   //L'objet le plus proche est celui-là
                distance = Vector3.Distance(transform.position, closestObject.transform.position);  //La distance maximale  
                                                                                                    //prend celle avec l'objet
                                                                                                    //le plus proche
            }
        }

        //Renvoie l'objet le plus proche
        return closestObject;
    }
}
