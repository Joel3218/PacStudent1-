using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNodes : MonoBehaviour
{

    int num = 28;

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
                GameObject clone = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y + currentOffset, 0), Quaternion.identity);
                currentOffset += offset;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
