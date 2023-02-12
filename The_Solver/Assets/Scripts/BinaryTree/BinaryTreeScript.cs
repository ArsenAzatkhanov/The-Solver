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
    public int[] array;
    public GameObject nodeTemplatePrefab;
    int t = 0;

    public int index;

    private void Update()
    {
        if (t < array.Length && Input.GetKeyDown(KeyCode.O))
        {
            AddNodeInRoot(array[t]);
            t++;
        }
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
