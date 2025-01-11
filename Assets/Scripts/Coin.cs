using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private MainSceneManager mainSceneManagerScript;
    private AudioSource coinAudio;

    void Start()
    {
        mainSceneManagerScript = GameObject.Find("MainSceneManager").GetComponent<MainSceneManager>();
        coinAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Rotate(50 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainSceneManagerScript.addCoin();

            if (coinAudio != null)
            {
                coinAudio.Play();
            }
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false; 

            Destroy(gameObject, coinAudio.clip.length);
        }
    }
}
