using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [Header("Explosion Effect")]
    [Tooltip("The ParticleSystem used for the explosion visual effect")]
    [SerializeField] private GameObject explosionVFX;

    //When the enemy is hit by a laser it play an
    //explosion effect  and then it is destroyed
    void OnParticleCollision(GameObject other)
    {
        GameObject explosionEffect =  Instantiate(explosionVFX, this.transform.position, Quaternion.identity, this.transform);
        Destroy(gameObject, 1f);
    }
}
