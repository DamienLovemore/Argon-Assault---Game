using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{ 
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.gameObject.name} triggered {other.gameObject.name}");
    }
}
