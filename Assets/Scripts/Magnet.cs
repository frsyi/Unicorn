using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float duration = 5f;

    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ActivateMagnet(duration);
            Destroy(gameObject);
        }
    }
}
