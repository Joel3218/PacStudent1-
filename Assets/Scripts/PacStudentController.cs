
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    MovementController movementController;

    public SpriteRenderer sprite;
    public Animator animator;

    public Vector3 position = new Vector3(-4.0f, 4.237f, 0.0f);
    // Start is called before the first frame update
    void Awake()
    {
       transform.position = position;
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        movementController = GetComponent<MovementController>();
       // movementController.lastMovingDirection = "left";
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("moving", true);
        if (Input.GetKey(KeyCode.A))
        {
            movementController.SetDirection("left");
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementController.SetDirection("right");
        }
        if (Input.GetKey(KeyCode.W))
        {
            movementController.SetDirection("up");
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementController.SetDirection("down");
        }

        bool flipX = false;
        bool flipY = false;
        if (movementController.lastMovingDirection == "left")
        {
            animator.SetInteger("direction", 0);
        }
        else if (movementController.lastMovingDirection == "right")
        {
            animator.SetInteger("direction", 0);
            flipX = true;
        }
        else if (movementController.lastMovingDirection == "up")
        {
            animator.SetInteger("direction", 1);
        }
        else if (movementController.lastMovingDirection == "down")
        {
            animator.SetInteger("direction", 1);
            flipY = true;
        }

        sprite.flipY = flipY;
        sprite.flipX = flipX;

    }
}