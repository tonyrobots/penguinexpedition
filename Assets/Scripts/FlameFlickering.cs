using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameFlickering : MonoBehaviour
{
    public float range = 1;

    private float timedelay;

    // Start is called before the first frame update
    void Start()
    {
        timedelay = Random.Range(.3f,1f);
        InvokeRepeating("Flicker", 0f, timedelay);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Flicker()
    {
        transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-range, range));

    }
}
