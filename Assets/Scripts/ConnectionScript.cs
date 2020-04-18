using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionScript : MonoBehaviour
{
    public GameObject StarterObject;
    public Material ConnectedMaterial;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script initialized");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger is triggered");

        // only triggered by objects of type connector
        if (other.gameObject.CompareTag("Connector"))
        {
            Debug.Log("I am currently connected to a connector");
            StarterObject.GetComponent<Renderer>().material = ConnectedMaterial;
            other.gameObject.GetComponent<Renderer>().material = ConnectedMaterial;
        }
    }
}
