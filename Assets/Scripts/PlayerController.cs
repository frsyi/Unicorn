using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 5.0f;
    public float maxSpeed = 15.0f;
    private float speed = 10.0f;
    private float boundary = 3.0f;
    private float horizontalinput;
    private Rigidbody playerRb;
    private bool isOnGround = true;
    public bool isGameOver = false;
    private Animator playerAnimator;
    private CapsuleCollider playerCollider;

    public bool hasMagnet = false;
    private float magnetTimer;
    public float magnetRange = 5f;
    
    public AudioClip gameClip;
    public AudioClip jumpClip;
    private AudioSource playerAudio;

    private Vector3 originalColliderCenter;
    private float originalColliderHeight;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider>();

        originalColliderCenter = playerCollider.center;
        originalColliderHeight = playerCollider.height;

        if (gameClip != null)
        {
            playerAudio.clip = gameClip;
            playerAudio.loop = true;
            playerAudio.Play();
        }
    }

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

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isOnGround)
        {
            playerAnimator.SetTrigger("Jump");
            playerRb.AddForce(Vector3.up * 8, ForceMode.Impulse);
            isOnGround = false;

            if (jumpClip != null)
            {
                playerAudio.PlayOneShot(jumpClip, 1.0f);
            }
        }

        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && isOnGround)
        {
            playerAnimator.SetTrigger("Slide");
            StartCoroutine(SlideCollider());
        }

        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
        }

        if (hasMagnet)
        {
            magnetTimer -= Time.deltaTime;
            if (magnetTimer <= 0)
            {
                hasMagnet = false;
            }

            GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
            foreach (GameObject coin in coins)
            {
                float distance = Vector3.Distance(transform.position, coin.transform.position);
                if (distance < magnetRange)
                {
                    coin.transform.position = Vector3.MoveTowards(coin.transform.position, transform.position, Time.deltaTime * 10);
                }
            }
        }
    }

    private IEnumerator SlideCollider()
    {
        playerCollider.height = originalColliderHeight / 2;
        playerCollider.center = new Vector3(originalColliderCenter.x, originalColliderCenter.y / 2, originalColliderCenter.z);

        yield return new WaitForSeconds(1.0f);

        playerCollider.height = originalColliderHeight;
        playerCollider.center = originalColliderCenter;
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

    public void ActivateMagnet(float duration)
    {
        hasMagnet = true;
        magnetTimer = duration;
    }
}