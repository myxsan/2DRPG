using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerMoveSpeed = 5f;

    private Rigidbody2D playerRb;
    private Animator playerAnimator;

    private KeyCode lastKey;

    private static int vSpeedAnimID;
    private static int hSpeedAnimID;
    private static int uIdleAnimID;
    private static int dIdleAnimID;
    private static int rIdleAnimID;
    private static int lIdleAnimID;
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        StrToID();
    }

    void Update()
    {
        playerAnimator.SetFloat(hSpeedAnimID, Input.GetAxisRaw("Horizontal"));
        playerAnimator.SetFloat(vSpeedAnimID, Input.GetAxisRaw("Vertical"));

        MovePlayer();
    }

    //Gets input to move player and animate accordingly

    private void MovePlayer()
    {
        Vector2 temp = Vector2.zero;
        if(Input.GetKey(KeyCode.W))
        {
            temp.y += playerMoveSpeed * Time.deltaTime;
            AnimatePlayer("W");
        }
        else if(Input.GetKey(KeyCode.S))
        {
            temp.y -= playerMoveSpeed * Time.deltaTime;
            AnimatePlayer("S");
        }
        else if(Input.GetKey(KeyCode.D))
        {
            temp.x += playerMoveSpeed * Time.deltaTime;
            AnimatePlayer("D");
        }
        else if(Input.GetKey(KeyCode.A))
        {
            temp.x -= playerMoveSpeed * Time.deltaTime;
            AnimatePlayer("A");
        }

        playerRb.velocity += temp;
    }

    private void AnimatePlayer(string key)
    {
        switch(key)
        {
            case "W":
                if (lastKey == KeyCode.W) return;
                lastKey = KeyCode.W; // Holds the last down key to avoid triggering every frame
                playerAnimator.SetTrigger(uIdleAnimID);
                break;
            case "S":
                if (lastKey == KeyCode.S) return;
                lastKey = KeyCode.S;
                playerAnimator.SetTrigger(dIdleAnimID);
                break;
            case "D":
                if (lastKey == KeyCode.D) return;
                lastKey = KeyCode.D;
                playerAnimator.SetTrigger(rIdleAnimID);
                break;
            case "A":
                if (lastKey == KeyCode.A) return;
                lastKey = KeyCode.A;
                playerAnimator.SetTrigger(lIdleAnimID);
                break;
        }
    }

    // Used IDs instead of string names to increase performance
    private static void StrToID()
    {
        vSpeedAnimID = Animator.StringToHash("PlayerSpeed_Vertical");
        hSpeedAnimID = Animator.StringToHash("PlayerSpeed_Horizontal");
        uIdleAnimID = Animator.StringToHash("PlayerFaceUp");
        dIdleAnimID = Animator.StringToHash("PlayerFaceDown");
        rIdleAnimID = Animator.StringToHash("PlayerFaceRight");
        lIdleAnimID = Animator.StringToHash("PlayerFaceLeft");
    }
}
