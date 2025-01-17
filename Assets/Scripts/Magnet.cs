using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float duration = 5f;
    private AudioSource magnetAudio;

    private void Start()
    {
        magnetAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ActivateMagnet(duration);

            if (magnetAudio != null)
            {
                magnetAudio.Play();

                GetComponent<Renderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

                Destroy(gameObject, magnetAudio.clip.length);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
