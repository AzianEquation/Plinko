// Coded by John Esco -Copyright 2019-
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ui text
using UnityEngine.UI;

public class Plinko : MonoBehaviour
{
    // reference to Rigidbody2D for 2D physics
    private Rigidbody2D rigid;
    private float speed;
    private bool spaceTrigger;
    private float xMin;
    private float xMax;
    private int score;
    private int playCount;
    private Vector3 initialPos;
    public Text countText;
    public Text winText;
    public Text scoreText;
    private Vector2 initialSpeedVec;
    void Start()
    {
        //Debug.Log("Hello, World!");
        // Get and store reference to Rigidbody2d
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0f;
        speed = 0.05f;
        spaceTrigger = false;
        xMin = -2.7f;
        xMax = 2.7f;
        score = 0;
        playCount = 5;
        initialPos = new Vector3(0f, 5f, 0f);
        initialSpeedVec = new Vector2(0, 0);
        winText.text = "";
        countText.text = "Plays Left: " + playCount.ToString();
        scoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // movement, trigering actions, and responding to User Input
        // allow horizontal movement (A,D, <-,->)
        //float moveHorizontal = Input.GetAxis("Horizontal");
        // float for vert movement which is zero
        //float moveVertical = 0f;
        // 2d vector
        //Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        // boolean values for keypresses
        bool keyLeft = Input.GetKey(KeyCode.LeftArrow);
        bool keyRight = Input.GetKey(KeyCode.RightArrow);
        bool space = Input.GetKey(KeyCode.Space);
        Vector2 rightVec, leftVec;
        leftVec = new Vector2(-speed, 0);
        rightVec = new Vector2(speed, 0);
        // zero motion vector
        
        if (keyLeft && transform.position.x >= xMin && spaceTrigger == false)
        {
            // check position
            // move left @ const speed
            transform.Translate(leftVec);
        }
        else if (keyRight && transform.position.x <= xMax && spaceTrigger == false)
        {
            //move right @ const speed
            transform.Translate(rightVec);
        }
        if (space)
        {
            spaceTrigger = true;
            //playCount++;
        }
        if (spaceTrigger)
        {
            rigid.gravityScale = 0.75f;

        }
        // score triggers
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        // 25 chest on right
        if (coll.gameObject.name.Equals("Chest25a"))
        {
            playCount--;
            score += 25;
            Debug.Log("You Scored 25 Points! \n Total: " + score.ToString());
            SetCountText();
            SetScoreText();
            Invoke("reset", 3);
        }
        // 25 chest on left
        else if (coll.gameObject.name.Equals("Chest25b"))
        {
            playCount--;
            score += 25;
            Debug.Log("You Scored 25 Points! \n Total: " + score.ToString());
            SetCountText();
            SetScoreText();
            Invoke("reset", 3);
        }
        // 50 score trigger
        else if (coll.gameObject.name.Equals("Chest50"))
        {
            playCount--;
            score += 50;
            Debug.Log("You Scored 50 Points! \n Total: " + score.ToString());
            SetCountText();
            SetScoreText();
            Invoke("reset", 3);
        }
        // 100 score trigger
        else if (coll.gameObject.name.Equals("Chest100"))
        {
            playCount--;
            score += 100;
            Debug.Log("You Scored 100 Points! \n Total: " + score.ToString());
            SetCountText();
            SetScoreText();
            Invoke("reset", 3);
        }

    }
    void SetCountText()
    {
        countText.text = "Plays Left: " + playCount.ToString();
        if (playCount <= 0)
        {
            winText.text = "GAME OVER! \nFinal Score: " + score.ToString();
        }
    }
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    // reset to initial position
    void reset()
    {
        // reset space bar trigger
        spaceTrigger = false;
        // stop motion
        transform.Translate(initialSpeedVec);
        // reset gravity
        rigid.gravityScale = 0f;
        // move to intial position (0,5)
        transform.position = (initialPos);
        // reset rototation
        transform.rotation = Quaternion.identity;
    }
}
