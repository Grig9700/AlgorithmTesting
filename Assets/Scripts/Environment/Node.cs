using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class Node : MonoBehaviour
{
    [HideInInspector]
    public float distanceToEnd; //k
    [HideInInspector]
    public float distanceToStart; //k

    public bool isStartNode;
    public bool isEndNode;
    [HideInInspector]
    public float range;

    //[HideInInspector]
    public bool explored;
    [HideInInspector]
    public bool drawn;
    //[HideInInspector]
    public float totalCostToReach;  //g
    [HideInInspector]
    public float heuristicCost;
    //[HideInInspector]
    public Node cameFrom;


    //DStarLite
    [HideInInspector]
    public float rhs;
    [HideInInspector]
    public float minimumCost;
    [HideInInspector]
    public float currentCost;
    //[HideInInspector]
    //public OPENState state = OPENState.New;
    [HideInInspector]
    public bool firstTime = true;

    //[HideInInspector]
    public List<Node> neighbors;

    [SerializeField]
    private CircleCollider2D coli;

    [HideInInspector]
    public int rangeAdjust = 2; //is 2 because the colliders overlappind decides if they're neighbours or not, as such range is only half the distance

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.white;
        Handles.DrawWireDisc(gameObject.transform.position, Vector3.forward, range * rangeAdjust);
    }

    private void OnValidate()
    {
        if (range < 0)
        {
            range = 0;
        }

        coli.radius = range;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        neighbors.Add(collision.GetComponent<Node>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        neighbors.Remove(collision.GetComponent<Node>());
    }

    private void Start()
    {
        neighbors = new List<Node>();
    }
}