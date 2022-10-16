using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{

    public bool moveLeft = false;
    public bool moveRight = false;
    public bool moveUp = false;
    public bool moveDown = false;

    public GameObject nodeLeft;
    public GameObject nodeRight;
    public GameObject nodeUp;
    public GameObject nodeDown;

    public bool isWarpRight = false;
    public bool isWarpLeft = false;

    //if node has peelet when the game starts
    public bool pelletNode = false;
    //if node still has the pellet
    public bool hasPellet = false;

    public SpriteRenderer pelletSprite;

    public GameManager gameManager;
    public bool isGhostStart = false;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (transform.childCount > 0)
        {
            hasPellet = true;
            pelletNode = true;
            pelletSprite = GetComponentInChildren<SpriteRenderer>();
        }

        RaycastHit2D[] hitsDown;
        // have a raycast line going down
        hitsDown = Physics2D.RaycastAll(transform.position, -Vector2.up);

        //loop
        for (int i =0; i < hitsDown.Length; i++)
        {
            float distance = Mathf.Abs(hitsDown[i].point.y - transform.position.y);
            if (distance < 0.4f && hitsDown[i].collider.tag == "Node")
            {
                moveDown = true;
                nodeDown = hitsDown[i].collider.gameObject;

            }
        }

        RaycastHit2D[] hitsUp;
        // have a raycast line going down
        hitsUp = Physics2D.RaycastAll(transform.position, Vector2.up);

        //loop
        for (int i = 0; i < hitsUp.Length; i++)
        {
            float distance = Mathf.Abs(hitsUp[i].point.y - transform.position.y);
            if (distance < 0.4f && hitsUp[i].collider.tag == "Node")
            {
                moveUp = true;
                nodeUp = hitsUp[i].collider.gameObject;

            }
        }

        RaycastHit2D[] hitsRight;
        // have a raycast line going down
        hitsRight = Physics2D.RaycastAll(transform.position, Vector2.right);

        //loop
        for (int i = 0; i < hitsRight.Length; i++)
        {
            float distance = Mathf.Abs(hitsRight[i].point.x - transform.position.x);
            if (distance < 0.4f && hitsRight[i].collider.tag == "Node")
            {
                moveRight = true;
                nodeRight = hitsRight[i].collider.gameObject;

            }
        }

        RaycastHit2D[] hitsLeft;
        // have a raycast line going down
        hitsLeft = Physics2D.RaycastAll(transform.position, -Vector2.right);

        //loop
        for (int i = 0; i < hitsLeft.Length; i++)
        {
            float distance = Mathf.Abs(hitsLeft[i].point.x - transform.position.x);
            if (distance < 0.4f && hitsLeft[i].collider.tag == "Node")
            {
                moveLeft = true;
                nodeLeft = hitsLeft[i].collider.gameObject;

            }
        }
        if(isGhostStart)
        {
            moveDown = true;
            nodeDown = gameManager.ghostNodeCenter;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetNodeFromDirection(string direction)
    {
        if (direction == "left" && moveLeft)
        {
            return nodeLeft;
        }
        else if (direction == "right" && moveRight)
        {
            return nodeRight;
        }
        if (direction == "up" && moveUp)
        {
            return nodeUp;
        }
        if (direction == "down" && moveDown)
        {
            return nodeDown;
        }
        else
        {
            return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && hasPellet)
        {
            hasPellet = false;
            pelletSprite.enabled = false;
            gameManager.CollectedPellet(this);
        }
    }

}
