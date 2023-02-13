using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTreeScript : MonoBehaviour
{
    public enum LeafSide
    {
        Left,
        Right,
        Root
    }

    TreeNode rootNode;
    [SerializeField] bool autoGen;
    [SerializeField] int numberAmount, minNum, maxNum;
    [SerializeField] List<NodePrefab> nodesWithCollision = new List<NodePrefab>();
    public int[] array;
    public GameObject nodeTemplatePrefab;
    int t = 0;

    public int index;



    private void Start()
    {
        if (!autoGen) return;
        array = new int[numberAmount];
        for (int i = 0; i < array.Length; i++)
        {
            System.Random random = new System.Random();
            array[i] = random.Next(minNum, maxNum);
        }


    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.O))
        {
            if(t < array.Length)
            {
                AddNodeInRoot(array[t]);
                t++;
            }

        }
    }



    public TreeNode GetRoot()
    {
        return rootNode;
    }

    public void AddElemInCol(NodePrefab nodePrefab)
    {
        nodesWithCollision.Add(nodePrefab);
    }

    public void AddNodeInRoot( int value)
    {
        int layer = 0;
        if (rootNode == null)
        {
            rootNode = new TreeNode(value, layer, this, index, LeafSide.Root);
            index++;
            rootNode.nodeObject = CreateNodeObject();
            rootNode.nodeObject.name = "Node V:" + value + " L:" + layer;
            rootNode.nodeObject.SetValues(value, layer, rootNode);
        }
        else
        {
            layer++;
            rootNode.AddNode(value, layer);
        }

        StartCoroutine(FixCollisions());
    }

    void PrintTree()
    {
        if(rootNode != null)
        {
            rootNode.PrintNode();
        }
    }

    void CheckCollisionsInTree()
    {
        nodesWithCollision = new List<NodePrefab>();
        if (rootNode != null)
            rootNode.CheckNodeCollision();
    }

    public IEnumerator FixCollisions()
    {
        do
        {
            CheckCollisionsInTree();
            if (nodesWithCollision.Count == 0) break;
            for (int i = 0; i < nodesWithCollision.Count; i++)
                TreeNode.ShiftCollision(nodesWithCollision[i].connectedTreeNode);
            yield return null;
        } while (nodesWithCollision.Count > 0);

    }

    public NodePrefab CreateNodeObject()
    {
        GameObject nodeObj = Instantiate(nodeTemplatePrefab, transform.position, Quaternion.identity);
        return nodeObj.GetComponent<NodePrefab>();
    }
}
