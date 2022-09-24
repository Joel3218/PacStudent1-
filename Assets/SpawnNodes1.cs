using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNodes1 : MonoBehaviour
{

    int num = 25;

    public float currentOffset;
    public float offset = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "Node")
        {
            currentOffset = offset;
            for (int i = 0; i < num; i++)
            {
                //cloning the node
                GameObject clone = Instantiate(gameObject, new Vector3(transform.position.x + currentOffset, transform.position.y, 0), Quaternion.identity);
                currentOffset = offset;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
