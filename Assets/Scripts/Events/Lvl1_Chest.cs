using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Gère l'event du coffre dans le niveau 1
public class Lvl1_Chest : NextLevel
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Key")
        {
            SceneManager.LoadScene("Hub");
        }
    }
}
