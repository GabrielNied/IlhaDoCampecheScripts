using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoitataBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speedRotation = 1.5f;

    void Start()
    {
        
    }

 
    void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        transform.Rotate(0, -1 * speedRotation * Time.deltaTime, 0, 0);
    }
}
