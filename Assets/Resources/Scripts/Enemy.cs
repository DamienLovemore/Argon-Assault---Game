using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Score Points")]
    [Tooltip("How much score points the player gains for destroying this enemy")]
    [SerializeField] private int scoreValue = 15;

    private ScoreBoard scoreManager;

    //Gathers all the GameObjects references it needs to work
    void Start() 
    {
        scoreManager = FindObjectOfType<ScoreBoard>();
    }

    void OnParticleCollision(GameObject other)
    {
        //If the particle that is hiting is a laser coming from the player,
        //and not a particle from a explosion, it should increase the score
        if (other.tag == "Laser Cannon")
        {
            scoreManager.IncreaseScore(this.scoreValue);
            //Fixes problem where multiple lasers hit the enemy and it
            //increases the score more then one time
            this.scoreValue = 0;
        }        
    }
}
