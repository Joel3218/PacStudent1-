using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public GameObject Node;
    public float speed = 4f;

    public string direction = "";
    public string lastMovingDirection = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NodeController currentNodeController = Node.GetComponent<NodeController>();

        transform.position = Vector2.MoveTowards(transform.position, Node.transform.position, speed * Time.deltaTime);

        if (transform.position.x == Node.transform.position.x && transform.position.y == Node.transform.position.y)
        {
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

    public void SetDirection(string newDirection)
    {
        direction = newDirection;
    }

}
