using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Health")]
    [Tooltip("The amount of hits this enemy can take")]
    [SerializeField] private int hitPoints = 4;

    [Header("Visual Effects")]
    [Tooltip("The ParticleSystem used for the explosion visual effect")]
    [SerializeField] private GameObject explosionVFX;
    [Tooltip("The ParticleSystem used for the hit visual effect")]
    [SerializeField] private GameObject hitVFX;

    [Header("Score Points")]
    [Tooltip("How much score points the player gains for destroying this enemy")]
    [SerializeField] private int scoreValue = 15;

    private ScoreBoard scoreManager;

    //Gathers all the GameObjects references it needs to work
    void Start() 
    {
        scoreManager = FindObjectOfType<ScoreBoard>();
        this.AddRigidbody();        
    }

    private void AddRigidbody()
    {
        //When adding a rigidbody to a gameobject it automatically
        //takes in cosideration all of its child colliders, without
        //us having to write to code to search all of it
        Rigidbody enemyRigidbody = this.gameObject.AddComponent<Rigidbody>();
        //Disable gravity so the enemy is able to fly
        enemyRigidbody.useGravity = false;
    }

    //Handles all the stuff that should happen when the enemy
    //is hitted by a laser
    private void ProcessHit(GameObject shooterObject)
    {
        //Plays the hit visual effect
        GameObject hitEffect =  Instantiate(hitVFX, this.transform.position, Quaternion.identity);
        Destroy(hitEffect, 1f);

        //Increases the score each time a laser from the
        //player hits
        this.LaserHitScore(shooterObject);

        //Decreases the hit points and destroy this enemy when
        //health points is zero
        if (this.hitPoints > 0)
            this.hitPoints -= 1;
        else
            this.EnemyDeath();
    }

    //Event that is triggered when a laser (particle) hits the enemy
    void OnParticleCollision(GameObject other)
    {
        this.ProcessHit(other);
    }

    //Increases the score points when a laser from the player hits
    //the enemy
    private void LaserHitScore(GameObject shooterObject)
    {
        //If the particle that is hiting is a laser coming from the player,
        //and not a particle from a explosion, it should increase the score
        if (shooterObject.tag == "Laser Cannon")
        {
            scoreManager.IncreaseScore(this.scoreValue);
        }        
    }

    //Plays an effect and destroyes the enemy and the effect created after
    private void EnemyDeath()
    {
        GameObject explosionEffect =  Instantiate(explosionVFX, this.transform.position, Quaternion.identity);
        Destroy(explosionEffect, 1f);
        Destroy(gameObject);
    }
}
