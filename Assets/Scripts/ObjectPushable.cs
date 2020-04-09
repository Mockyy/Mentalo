using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectPushable : MonoBehaviour
{
    [SerializeField]
    private GameObject feedback;

    [HideInInspector]
    public bool isPushable;

    // Start is called before the first frame update
    void Start()
    {
        isPushable = false;
    }
}
