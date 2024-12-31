using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float forwardSpeed = 5.0f;
    private float speed = 10.0f;
    private float boundary = 5.0f; 
    private float horizontalinput;
    private Rigidbody playerRb;
    private bool isOnGround = true;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalinput = Input.GetAxis("Horizontal");
        if (transform.position.x > boundary) 
        {
            transform.position = new Vector3(boundary, transform.position.y, transform.position.z); 
        }
        if (transform.position.x < -boundary) 
        {
            transform.position = new Vector3(-boundary, transform.position.y, transform.position.z);
        }
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalinput);
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            isOnGround = true;
        }
    }
}
