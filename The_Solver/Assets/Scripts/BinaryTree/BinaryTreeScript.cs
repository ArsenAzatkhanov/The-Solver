using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTreeScript : MonoBehaviour
{
    TreeNode rootNode;
    public int[] array;
    public GameObject nodeTemplatePrefab;

    private void Start()
    {
        print("Start");

        for (int i = 0; i < array.Length; i++)
        {
            AddNode(array[i]);
        }

        PrintTree();

    }

    public void AddNode( int value)
    {
        int layer = 0;
        if (rootNode == null)
        {
            rootNode = new TreeNode(value, layer, this);
            rootNode.nodeObject = CreateNodeObject();
            rootNode.nodeObject.SetValues(value, layer);
        }

        else
        {
            layer++;
            rootNode.AddNode(value, layer);
        }
    }

    void PrintTree()
    {
        if(rootNode != null)
        {
            rootNode.PrintNode();
        }
    }

    public NodePrefab CreateNodeObject()
    {
        GameObject nodeObj = Instantiate(nodeTemplatePrefab, transform.position, Quaternion.identity);
        return nodeObj.GetComponent<NodePrefab>();
    }
}
