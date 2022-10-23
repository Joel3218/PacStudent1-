

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

    public enum GhostType { red, blue, pink, orange }

    public GhostType ghostType;

    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;
    public GameObject ghostNodeStart;
    public GameObject ghostNodeCenter;

    public GhostNodeStatesEnum respawn;

    public MovementController movementController;
    public GameObject startNode;
    public bool leaveHome = false;

    public GameManager gameManager;
    public bool testingRespawn = false;

    public bool isfrighten = false;

    public GameObject[] scatterNodes;
    public int scatterNodesIndex;

    public bool alreadyLeftHome = false;

    // Start is called before the first frame update
    void Awake()
    {
        scatterNodesIndex = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        movementController = GetComponent<MovementController>();
        if (ghostType == GhostType.red)
        {
            ghostNodeState = GhostNodeStatesEnum.startNode;
            respawn = GhostNodeStatesEnum.centerNode;
            startNode = ghostNodeStart;
            leaveHome = true;
            alreadyLeftHome = true;
        }
        else if (ghostType == GhostType.pink)
        {
            ghostNodeState = GhostNodeStatesEnum.centerNode;
            startNode = ghostNodeCenter;
            respawn = GhostNodeStatesEnum.centerNode;
        }
        else if (ghostType == GhostType.blue)
        {
            ghostNodeState = GhostNodeStatesEnum.leftNode;
            startNode = ghostNodeLeft;
            respawn = GhostNodeStatesEnum.leftNode;
        }
        else if (ghostType == GhostType.orange)
        {
            ghostNodeState = GhostNodeStatesEnum.rightNode;
            startNode = ghostNodeRight;
            respawn = GhostNodeStatesEnum.rightNode;
        }
        movementController.Node = startNode;
        transform.position = startNode.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (testingRespawn == true)
        {
            leaveHome = false;
            ghostNodeState = GhostNodeStatesEnum.respawning;
            testingRespawn = false;
        }

        if (movementController.Node.GetComponent<NodeController>().isWarpNode)
        {
            movementController.SetSpeed(1);
        }
        else
        {
            movementController.SetSpeed(1);
        }
    }

    public void ReachedCenter(NodeController nodeController)
    {
        if (ghostNodeState == GhostNodeStatesEnum.movingInNodes)
        {
            alreadyLeftHome = true;
            //scatter mode
            if (gameManager.ghostMode == GameManager.GhostMode.scatter)
            {

                GhostScatterDirection();

            }


            else if (isfrighten)
            {
                string direction = RandomDirection();
                movementController.SetDirection(direction);
            }
            //chase mode
            else
            {
                if (ghostType == GhostType.red)
                {
                    RedGhostDirection();
                }
                else if (ghostType == GhostType.pink)
                {
                    PinkGhostDirection();
                }
                else if (ghostType == GhostType.blue)
                {
                    BlueGhostDirection();
                }
                else if (ghostType == GhostType.orange)
                {
                    OrangeGhostDirection();
                }
            }
        }

        else if (ghostNodeState == GhostNodeStatesEnum.respawning)
        {
            string direction = "";

            //move center from start node
            if (transform.position.x == ghostNodeStart.transform.position.x && transform.position.y == ghostNodeStart.transform.position.y)
            {
                direction = "down";
            }
            //move to left or right node from center
            else if (transform.position.x == ghostNodeCenter.transform.position.x && transform.position.y == ghostNodeCenter.transform.position.y)
            {
                if (respawn == GhostNodeStatesEnum.centerNode)
                {
                    ghostNodeState = respawn;
                }
                else if (respawn == GhostNodeStatesEnum.leftNode)
                {
                    direction = "left";

                }
                else if (respawn == GhostNodeStatesEnum.rightNode)
                {
                    direction = "right";
                }
            }
            //leave home once respawned
            else if ((transform.position.x == ghostNodeLeft.transform.position.x && transform.position.y == ghostNodeLeft.transform.position.y)
                || (transform.position.x == ghostNodeRight.transform.position.x && transform.position.y == ghostNodeRight.transform.position.y))
            {
                ghostNodeState = respawn;
            }
            //find starting node
            else
            {
                direction = ClosestDirection(ghostNodeStart.transform.position);
            }

            movementController.SetDirection(direction);
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

    string RandomDirection()
    {
        List<string> possibleDirections = new List<string>();
        NodeController nodeController = movementController.Node.GetComponent<NodeController>();

        if (nodeController.moveDown && movementController.lastMovingDirection != "up")
        {
            possibleDirections.Add("down");
        }
        if (nodeController.moveUp && movementController.lastMovingDirection != "down")
        {
            possibleDirections.Add("up");
        }
        if (nodeController.moveRight && movementController.lastMovingDirection != "left")
        {
            possibleDirections.Add("right");
        }
        if (nodeController.moveLeft && movementController.lastMovingDirection != "right")
        {
            possibleDirections.Add("left");

        }
        string direction = "";
        int directionIndex = Random.Range(0, possibleDirections.Count - 1);
        direction = possibleDirections[directionIndex];
        return direction;
    }

    void GhostScatterDirection()
    {
        if (transform.position.x == scatterNodes[scatterNodesIndex].transform.position.x && transform.position.y == scatterNodes[scatterNodesIndex].transform.position.y)
        {
            scatterNodesIndex++;

            if (scatterNodesIndex == scatterNodes.Length - 1)
            {
                scatterNodesIndex = 0;
            }
        }

        string direction = ClosestDirection(scatterNodes[scatterNodesIndex].transform.position);

        movementController.SetDirection(direction);
    }
    void RedGhostDirection()
    {
        string direction = ClosestDirection(gameManager.pacStudent.transform.position);
        movementController.SetDirection(direction);
    }

    void PinkGhostDirection()
    {
        string pacStudentDirection = gameManager.pacStudent.GetComponent<MovementController>().lastMovingDirection;
        float distanceInNodes = 0.34f;

        Vector2 target = gameManager.pacStudent.transform.position;
        if (pacStudentDirection == "left")
        {
            target.x -= distanceInNodes * 2;
        }
        else if (pacStudentDirection == "right")
        {
            target.x += distanceInNodes * 2;
        }
        else if (pacStudentDirection == "up")
        {
            target.y += distanceInNodes * 2;
        }
        else if (pacStudentDirection == "down")
        {
            target.y -= distanceInNodes * 2;
        }
        string direction = ClosestDirection(target);
        movementController.SetDirection(direction);
    }


    void BlueGhostDirection()
    {
        string pacStudentDirection = gameManager.pacStudent.GetComponent<MovementController>().lastMovingDirection;
        float distanceInNodes = 0.34f;

        Vector2 target = gameManager.pacStudent.transform.position;
        if (pacStudentDirection == "left")
        {
            target.x -= distanceInNodes * 2;
        }
        else if (pacStudentDirection == "right")
        {
            target.x += distanceInNodes * 2;
        }
        else if (pacStudentDirection == "up")
        {
            target.y += distanceInNodes * 2;
        }
        else if (pacStudentDirection == "down")
        {
            target.y -= distanceInNodes * 2;
        }

        GameObject redGhost = gameManager.redGhost;
        float aDistance = target.x - redGhost.transform.position.x;
        float bDistance = target.y - redGhost.transform.position.y;

        Vector2 blueTar = new Vector2(target.x + aDistance, target.y + bDistance);
        string direction = ClosestDirection(blueTar);
        movementController.SetDirection(direction);
    }

    void OrangeGhostDirection()
    {
        float distance = Vector2.Distance(gameManager.pacStudent.transform.position, transform.position);
        float distanceInNodes = 0.34f;

        if (distance < 0)
        {
            distance *= -1;
        }

        if (distance <= distanceInNodes * 8)
        {
            RedGhostDirection();
        }
        else
        {
            GhostScatterDirection();
        }
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









