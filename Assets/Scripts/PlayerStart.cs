using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.SetPositionAndRotation(transform.position, Quaternion.identity);    
    }
}
