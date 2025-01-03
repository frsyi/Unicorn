using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 5.0f;
    public float maxSpeed;
    private float speed = 10.0f;
    private float boundary = 3.0f;
    private float horizontalinput;
    private Rigidbody playerRb;
    private bool isOnGround = true;
    public bool isGameOver = false;

    public AudioClip gameClip;
    private AudioSource playerAudio;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
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
            playerRb.AddForce(Vector3.up * 8, ForceMode.Impulse);
            isOnGround = false;
        }

        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * 15, ForceMode.Impulse);
            isOnGround = false;
            playerAudio.PlayOneShot(gameClip, 1.0f);      
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            MainSceneManager.Instance.SaveCoinData();
            SceneManager.LoadScene(2);
        }
    }
}
