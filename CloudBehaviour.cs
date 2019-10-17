using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        transform.Rotate(0, 1 * 1.3f * Time.deltaTime, 0, 0);
    }
}
