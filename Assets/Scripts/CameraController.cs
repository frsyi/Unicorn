using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player; 
    private Vector3 offset;

    void Start()
    {
        player = FindActivePlayer();
        offset = transform.position - player.position;
    }

    void Update()
    {
        Transform activePlayer = FindActivePlayer();
        if (activePlayer != player)
        {
            player = activePlayer;
        }

        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + player.position.z);
        transform.position = newPosition;
    }

    private Transform FindActivePlayer()
    {
        foreach (GameObject character in FindObjectOfType<PlayerSelector>().characters)
        {
            if (character.activeSelf)
            {
                return character.transform;
            }
        }
        return null;
    }
}
