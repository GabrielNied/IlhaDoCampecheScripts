using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runas : MonoBehaviour
{

    BoxCollider bx;
    public bool clicked = false;

    public GameObject[] runas = new GameObject[5];

    void Start()
    {
        bx = GetComponent<BoxCollider>();
        foreach (GameObject item in runas)
        {
            Rigidbody rb = item.transform.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
  
    void Update()
    {
        if (clicked)
        {
            bx.enabled = false;

            foreach (var item in runas)
            {
                Rigidbody rb = item.transform.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.velocity += (Vector3.left + Vector3.back + Vector3.up) * Random.Range(300,600) * Time.deltaTime;
                rb.mass = Random.Range(1, 4);
                
            }
            foreach (var item in runas)
            {
                Rigidbody rb = item.transform.GetComponent<Rigidbody>();
                rb.velocity += Vector3.down * Random.Range(300, 600) * Time.deltaTime;



            }
            clicked = false;


        }
    }



}
