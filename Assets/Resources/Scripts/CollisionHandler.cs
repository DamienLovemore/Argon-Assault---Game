using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Header("Explosion Effect")]
    [Tooltip("The ParticleSystem used for the explosion visual effect")]
    [SerializeField] private ParticleSystem explosionVFX;  
    [Tooltip("The amount of seconds to await before restarting game on collision")]
    [SerializeField] private float delayTime = 1f;

    private PlayerControls playerInputHandler;

    void Start()
    {
        playerInputHandler = GetComponent<PlayerControls>();
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(this.PlayerShipCrash());
    }

    private IEnumerator PlayerShipCrash()
    {
        explosionVFX.Play();
        foreach(MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = false;
        }
        GetComponent<BoxCollider>().enabled = false;
        playerInputHandler.enabled = false; 
        yield return new WaitForSeconds(this.delayTime);

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
