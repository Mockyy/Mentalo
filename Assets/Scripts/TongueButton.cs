using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueButton : MonoBehaviour
{
    [SerializeField]
    private GameObject floatingText = null;

    public void Active()
    {
        if (floatingText != null)
        {
            Instantiate(floatingText, transform.position, Quaternion.identity, transform);
        }

        print("Touché monsieur le chat");
    }
}
