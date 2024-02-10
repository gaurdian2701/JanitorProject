using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBasedPlatform : MonoBehaviour
{
    [SerializeField] private List<Transform> nodes;
    [SerializeField] private float speed;
    private int nodeSize;
    private int currentNode;
    private void Awake()
    {
        nodeSize = nodes.Count;
        currentNode = 0;
    }

    private void Start()
    {
        transform.position = nodes[0].position;
    }
    void Update()
    {
        if(currentNode == nodeSize)
            currentNode = 0;

        transform.position = Vector2.MoveTowards(transform.position, nodes[currentNode].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, nodes[currentNode].position) < 0.1f && currentNode < nodes.Count)
            currentNode++;
    }
}
