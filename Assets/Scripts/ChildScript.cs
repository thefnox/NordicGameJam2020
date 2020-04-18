using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildScript : MonoBehaviour
{
    GameObject connector;
    ConnectorScript connectorScript;
    public Material ConnectedMaterial;
    public Material DisconnectedMaterial;
    private ParticleSystem[] Particles;
    private int particleCount = 0;
    public List<ParticleCollisionEvent> collisionEvents;

    // Start is called before the first frame update
    void Start()
    {
        Particles = GetComponentsInChildren<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        // only triggered by objects of type connector
        if (other.gameObject.CompareTag("Connector"))
        {
            connector = other.gameObject;
            connectorScript = connector.gameObject.GetComponent<ConnectorScript>();
            if (connectorScript.ConnectorActiveState == true)
            {
                this.gameObject.GetComponent<Renderer>().material = ConnectedMaterial;
                foreach (ParticleSystem particle in Particles)
                {
                    particle.Play();
                }
            }
            else
            {
                this.gameObject.GetComponent<Renderer>().material = DisconnectedMaterial;
                foreach (ParticleSystem particle in Particles)
                {
                    particle.Stop();
                }
            }
        }
    }
}
