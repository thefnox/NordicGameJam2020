using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingScript : MonoBehaviour
{
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 up = transform.TransformDirection(Vector3.up) * 10;
        Debug.DrawRay(transform.position, up, Color.green);
    }

    void FixedUpdate()
    {
        Vector3 up = transform.TransformDirection(Vector3.up);

        if (Physics.Raycast(transform.position, up, out hit)) 
        {
            var GameObject = hit.collider.gameObject;
            GameObject.transform.localScale = new Vector3(5,5,5);
            print("There is something in front of the object!");
        }
            
    }
}
