using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject Node;
    public float speed = 4f;

    public string direction = "";
    public string lastMovingDirection = "";

    public bool canWarp = true;

    public bool Ghost = false;
    // Start is called before the first frame update
    void Awake()
    {
      
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        NodeController currentNodeController = Node.GetComponent<NodeController>();

        transform.position = Vector2.MoveTowards(transform.position, Node.transform.position, speed * Time.deltaTime);

        bool reverseDirection = false;
        if (
            (direction == "left" && lastMovingDirection == "right")
            || (direction == "right" && lastMovingDirection == "left")
            || (direction == "up" && lastMovingDirection == "down")
            || (direction == "down" && lastMovingDirection == "up")
            )
            {
            reverseDirection = true;
            }

        

        if ((transform.position.x == Node.transform.position.x && transform.position.y == Node.transform.position.y) || reverseDirection)
        {
            if (Ghost)
            {
                GetComponent<EnemyController>().ReachedCenter(currentNodeController);
            }

            //if we reach centre of left warp, warp to right
            if (currentNodeController.isWarpLeft && canWarp)
            {
                Node = gameManager.rightWarpNode;
                direction = "left";
                lastMovingDirection = "left";
                transform.position = Node.transform.position;
                canWarp = false;
            }
            //if we reach centre of right warp, warp to left
            else if (currentNodeController.isWarpRight && canWarp)
            {
                Node = gameManager.leftWarpNode;
                direction = "right";
                lastMovingDirection = "right";
                transform.position = Node.transform.position;
                canWarp = false;
            }
            //otherwise find next node
            else
            {
                //if we are not a ghost, dont enter ther ghost home
                if (currentNodeController.isGhostStart && direction == "down" && (!Ghost || 
                    GetComponent<EnemyController>().ghostNodeState != EnemyController.GhostNodeStatesEnum.respawning))
                {
                    direction = lastMovingDirection;
                }

                GameObject newNode = currentNodeController.GetNodeFromDirection(direction);

                if (newNode != null)
                {
                    Node = newNode;
                    lastMovingDirection = direction;
                }
                //cant move in desired direction, keep going in last moving direction
                else
                {
                    direction = lastMovingDirection;
                    newNode = currentNodeController.GetNodeFromDirection(direction);
                    if (newNode != newNode)
                    {
                        Node = newNode;
                    }

                }
            }

            
        }
        //we arent in the centre of the node
        else
        {
            canWarp = true;
        }
    }

    public void SetSpeed(float differentSpeed)
    {
        speed = differentSpeed;
    }

    public void SetDirection(string newDirection)
    {
        direction = newDirection;
    }

}
