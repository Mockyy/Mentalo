using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void onTriggerEnter(Collision other)
    {
        print("Next");
        SceneManager.LoadScene(sceneName);
    }
}
