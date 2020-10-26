using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text winText;
    public Text livesText;
    private int scoreValue = 0;
    private int livesValue = 0;
    private SpriteRenderer spriteRenderer;
    public AudioClip music;
    public AudioClip victorySound;
    public AudioSource musicSource;

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        livesValue = 3;
        SetLivesText();
        score.text = scoreValue.ToString();
        winText.text = "";
        musicSource.clip = music;
        musicSource.Play();
    }

    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(horizontalMovement * speed, verticalMovement * speed));
    
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if(scoreValue == 4)
            {
                transform.position = new Vector2(82.0f, 0.12f);
                livesValue = 3;
                SetLivesText();
            }
            else if(scoreValue == 8)
            {
                musicSource.Stop();
                musicSource.clip = victorySound;
                musicSource.Play();
                winText.text = "You win! Game created by Eddy Elguezabal.";
            }
        }

        if(collision.collider.tag == "Enemy")
        {   
            livesValue = livesValue - 1;
            SetLivesText2(collision);
            Destroy(collision.collider.gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
            }
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + livesValue.ToString();
    }

    void SetLivesText2(Collision2D collision)
    {
        livesText.text = "Lives: " + livesValue.ToString();
        if (livesValue == 0)
        {
            winText.text = "You Lose!";
            spriteRenderer.enabled = false;
            rd2d.isKinematic = true;
        }
    }
}
