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

    //Update is called once per frame
    void Update()
    {
        //Putar rotasi koin untuk animasi
        transform.Rotate(20 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Tambahkan koin
            mainSceneManagerScript.addCoin();

            if (coinAudio != null)
            {
                coinAudio.Play();
            }
            GetComponent<Renderer>().enabled = false; //Hilangkan visual koin
            GetComponent<Collider>().enabled = false; //Nonaktifkan  collider

            //Hancurkan GameObject setelah suara selesai diputar
            Destroy(gameObject, coinAudio.clip.length);
        }
    }
}
