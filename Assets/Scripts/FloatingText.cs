using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField]
    private float destroyTime = 3f;

    private float randomizeIntensity = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);

        transform.localPosition += new Vector3(
            Random.Range(-randomizeIntensity, randomizeIntensity), Random.Range(-randomizeIntensity, randomizeIntensity), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
