using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartCollisionScript : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public int numCollisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        numCollisionEvents = 0;
    }

    void OnParticleCollision(GameObject other)
    {
        numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        Debug.Log(numCollisionEvents + ("Number of Collision events was"));
    }
}