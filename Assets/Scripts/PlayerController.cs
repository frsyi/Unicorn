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
    private Animator playerAnimator;

    public AudioClip gameClip;
    private AudioSource playerAudio;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);

        horizontalinput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalinput);

        if (transform.position.x > boundary)
        {
            transform.position = new Vector3(boundary, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -boundary)
        {
            transform.position = new Vector3(-boundary, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerAnimator.SetTrigger("Jump");
            playerRb.AddForce(Vector3.up * 8, ForceMode.Impulse);
            isOnGround = false;
            playerAudio.PlayOneShot(gameClip, 1.0f);
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow) && isOnGround)
        {
            playerAnimator.SetTrigger("Slide");
        }

        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
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
            GameManager.Instance.SaveCoinData();
            SceneManager.LoadScene(2);
        }
    }
}
