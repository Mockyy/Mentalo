using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerChest : NextLevel
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Key")
        {
            SceneManager.LoadScene("Hub");
        }
    }
}
