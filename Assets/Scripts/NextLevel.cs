using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Charge la nouvelle scène
public class NextLevel : MonoBehaviour
{
    [Tooltip("Le nom de la scène à charger. Penser à ajouter la scène choisie dans les builds settings")]
    [SerializeField] private string sceneName = default;

    private void OnTriggerEnter(Collider other)
    {
        print("Next");
        SceneManager.LoadScene(sceneName);
    }
}
