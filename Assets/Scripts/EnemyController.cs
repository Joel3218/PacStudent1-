using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public enum GhostNodeStatesEnum
    {
        respawning,
        leftNode,
        rightNode,
        centerNode,
        startNode,
        movingInNodes
    }

    public GhostNodeStatesEnum ghostNodeState;

    public enum GhostType { red, blue, pink, orange}

    public GhostType ghostType;

    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;
    public GameObject ghostNodeStart;
    public GameObject ghostNodeCenter;

    public MovementController movementController;
    public GameObject startNode;
    public bool leaveHome = false;

    public GameManager gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
          
        movementController = GetComponent<MovementController>();
       if (ghostType == GhostType.red)
        {
            ghostNodeState = GhostNodeStatesEnum.startNode;
            startNode = ghostNodeStart;
        }
       else if (ghostType == GhostType.pink)
        {
            ghostNodeState = GhostNodeStatesEnum.centerNode;
            startNode = ghostNodeCenter;
        }
        else if (ghostType == GhostType.blue)
        {
            ghostNodeState = GhostNodeStatesEnum.leftNode;
            startNode = ghostNodeLeft;
        }
        else if (ghostType == GhostType.orange)
        {
            ghostNodeState = GhostNodeStatesEnum.rightNode;
            startNode = ghostNodeRight;
        }
        movementController.Node = startNode;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReachedCenter(NodeController nodeController)
    {
        if (ghostNodeState == GhostNodeStatesEnum.movingInNodes)
        {
            if (ghostType == GhostType.red)
            {
                RedGhostDirection();
            }
        }
        else if (ghostNodeState == GhostNodeStatesEnum.respawning)
        {

        }
        else
        {
            if (leaveHome)
            {
                //move to center from left
                if (ghostNodeState == GhostNodeStatesEnum.leftNode)
                {
                    ghostNodeState = GhostNodeStatesEnum.centerNode;
                    movementController.SetDirection("right");
                }
                //move to center from right
                else if (ghostNodeState == GhostNodeStatesEnum.rightNode)
                {
                    ghostNodeState = GhostNodeStatesEnum.centerNode;
                    movementController.SetDirection("left");
                }
                //move to start from center
                else if (ghostNodeState == GhostNodeStatesEnum.centerNode)
                {
                    ghostNodeState = GhostNodeStatesEnum.startNode;
                    movementController.SetDirection("up");
                }
                //move to start from the other nodes
                else if (ghostNodeState == GhostNodeStatesEnum.startNode)
                {
                    ghostNodeState = GhostNodeStatesEnum.movingInNodes;
                    movementController.SetDirection("left");
                }
            }
        }
    }

    void RedGhostDirection()
    {
        string direction = ClosestDirection(gameManager.pacStudent.transform.position);
        movementController.SetDirection(direction);
    }

    void PinkGhostDirection()
    {

    }

    void BlueGhostDirection()
    {

    }

    void OrangeGhostDirection()
    {

    }

    string ClosestDirection(Vector2 target)
    {
        float shortestDistance = 0;
        string lastMovingDirection = movementController.lastMovingDirection;
        string newDirection = "";

        NodeController nodeController = movementController.Node.GetComponent<NodeController>();

        //if we can move up 
        if (nodeController.moveUp && lastMovingDirection != "down")
        {
            //Get node above us
            GameObject nodeUp = nodeController.nodeUp;
            //distance betwen top node and pacStudent
            float distance = Vector2.Distance(nodeUp.transform.position, target);
            //if shortest distance
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "up";
            }
        }

        if (nodeController.moveDown && lastMovingDirection != "up")
        {
            //Get node above us
            GameObject nodeDown = nodeController.nodeDown;
            //distance betwen top node and pacStudent
            float distance = Vector2.Distance(nodeDown.transform.position, target);
            //if shortest distance
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "down";
            }
        }

        if (nodeController.moveRight && lastMovingDirection != "left")
        {
            //Get node above us
            GameObject nodeRight = nodeController.nodeRight;
            //distance betwen top node and pacStudent
            float distance = Vector2.Distance(nodeRight.transform.position, target);
            //if shortest distance
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "right";
            }
        }

        if (nodeController.moveLeft && lastMovingDirection != "right")
        {
            //Get node above us
            GameObject nodeLeft = nodeController.nodeLeft;
            //distance betwen top node and pacStudent
            float distance = Vector2.Distance(nodeLeft.transform.position, target);
            //if shortest distance
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "left";
            }
        }
        return newDirection;
        
    }
}









