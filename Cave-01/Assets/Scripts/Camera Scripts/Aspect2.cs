using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aspect2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().aspect = 1.0f;
        GetComponent<Camera>().fieldOfView = 59.04f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
