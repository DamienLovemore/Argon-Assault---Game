using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
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
        playerInputHandler.enabled = false;        
        yield return new WaitForSeconds(this.delayTime);

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
