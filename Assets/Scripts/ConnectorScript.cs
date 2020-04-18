using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorScript : MonoBehaviour
{
    public bool ConnectorActiveState = false;
    public Material ConnectedMaterial;
    public Material DisconnectedMaterial;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if (ConnectorActiveState == true)
        {
            // Debug.Log("Active state of connector " + ConnectorActiveState);
            this.gameObject.GetComponent<Renderer>().material = ConnectedMaterial;
        }
        else if (true)
            this.gameObject.GetComponent<Renderer>().material = DisconnectedMaterial;
    }

    /* private void OnTriggerStay(Collider other)
    {
        Debug.Log("Connector Trigger is triggered");
    }
    */
}
