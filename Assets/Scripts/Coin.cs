using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GManager gManagerScript;

    void Start()
    {
        gManagerScript = GameObject.Find("GManager").GetComponent<GManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(20 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gManagerScript.addCoin();
            Destroy(gameObject);
        }
    }
}
