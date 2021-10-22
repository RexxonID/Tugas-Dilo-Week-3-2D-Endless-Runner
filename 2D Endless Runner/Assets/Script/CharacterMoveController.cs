using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    [Header("Movement")]
    public float moveAccel;
    public float maxSpeed;

    [Header("Jump")]
    public float jumpAccel;

    [Header("Ground Raycast")]
    public float groundRaycastDistance;
    public LayerMask groundLayerMask;

    [Header("Scoring")]
    public ScoreController score;
    public float scoringRatio;
    private float lastPositionX;

    [Header("Game Over")]
    public GameObject gameOverScreen;
    public float fallPositionY;

    [Header("Camera")]
    public CameraMoveController gameCamera;

    private bool isJumping;
    private bool isOnGround;
    private CharacterSoundController sound;
    private Rigidbody2D Rb;
    private Animator anim;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sound = GetComponent<CharacterSoundController>();
    }
    private void Update()
    {
        //raycast ground
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position, Vector2.down, groundRaycastDistance, groundLayerMask);
        if (hit)
        {
            if(!isOnGround && Rb.velocity.y <= 0)
            {
                isOnGround = true;
            }
        }
        else
        {
            isOnGround = false;
        }

        //calculate velocity vector
        Vector2 velocityVector = Rb.velocity;
        if (isJumping)
        {
            velocityVector.y += jumpAccel;
            isJumping = false;
        }

        velocityVector.x = Mathf.Clamp(
            velocityVector.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);

        Rb.velocity = velocityVector;

        //read input
        if (Input.GetMouseButtonDown(0))
        {
            if (isOnGround)
            {
                isJumping = true;

                sound.PlayJump();
            }
        }

        //change Animation
        anim.SetBool("isOnGround", isOnGround);

        //calculate score
        int distancePassed = Mathf.FloorToInt(transform.position.x - lastPositionX);
        int scoreIncerment = Mathf.FloorToInt(transform.position.x - lastPositionX);

        if (scoreIncerment > 0)
        {
            score.IncreaseCurrentScore(scoreIncerment);
            lastPositionX += distancePassed;
        }

        //Game Over
        if(transform.position.y  < fallPositionY)
        {
            GameOver();
        }
    }
    private void FixedUpdate()
    {
        Vector2 velocityVector = Rb.velocity;
        velocityVector.x = Mathf.Clamp(velocityVector.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);

        Rb.velocity = velocityVector;
    }
    private void GameOver()
    {
        //set High Score
        score.FinishScoreing();

        //stop camera movement
        gameCamera.enabled = false;

        //show gameover
        gameOverScreen.SetActive(true);

        //disable this too
        this.enabled = false;
    }
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position +
            (Vector3.down * groundRaycastDistance), Color.white);
    }
}
