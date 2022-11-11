using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{    
    void OnCollisionEnter(Collision other)
    {
        Debug.Log($"{this.gameObject.name} bumped into {other.gameObject.name}");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.gameObject.name} triggered {other.gameObject.name}");
    }
}
