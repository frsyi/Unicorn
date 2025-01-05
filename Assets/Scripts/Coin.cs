using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private MainSceneManager mainSceneManagerScript;

    void Start()
    {
        mainSceneManagerScript = GameObject.Find("MainSceneManager").GetComponent<MainSceneManager>();
    }

    void Update()
    {
        transform.Rotate(20 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mainSceneManagerScript.addCoin();
            Destroy(gameObject);
        }
    }
}
