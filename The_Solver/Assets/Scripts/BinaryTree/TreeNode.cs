using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TreeNode 
{
    int value, layer;
    TreeNode leftNode, rightNode, parentNode;
    BinaryTreeScript binaryTree;
    public NodePrefab nodeObject;

    public TreeNode( int value, int layer, BinaryTreeScript binaryTree)
    {
        this.value = value;
        this.layer = layer;
        this.binaryTree = binaryTree;
    }

    public TreeNode(int value, int layer, TreeNode parentNode, BinaryTreeScript binaryTree)
    {
        this.value = value;
        this.layer = layer;
        this.parentNode = parentNode;
        this.binaryTree = binaryTree;
    }

    public void AddNode( int value, int currentLayer)
    {
        if (value > this.value)
            SetRight(value, currentLayer);
        else if (value < this.value)
            SetLeft(value, currentLayer);
        else
            return;
    }

    public void SetLeft( int value, int currentLayer)
    {
        if (leftNode == null)
        {
            TreeNode node = SetNode(value, currentLayer);
            node.nodeObject.gameObject.transform.localPosition = Vector3.zero + new Vector3(-node.nodeObject.shiftValues.x, -node.nodeObject.shiftValues.y, 0);
            this.leftNode = node;
        }
        else
        {
            currentLayer++;
            leftNode.AddNode(value, currentLayer);
        }

    }

    public void SetRight(int value, int currentLayer)
    {
        if (rightNode == null)
        {
            TreeNode node = SetNode(value, currentLayer);
            node.nodeObject.gameObject.transform.localPosition = Vector3.zero + new Vector3(node.nodeObject.shiftValues.x, -node.nodeObject.shiftValues.y, 0);
            this.rightNode = node;
            
        }
        else
        {
            currentLayer++;
            rightNode.AddNode(value, currentLayer);
        }
  
    }

    TreeNode SetNode(int value, int currentLayer)
    {
        TreeNode node = new TreeNode(value, currentLayer, this, binaryTree);
        node.nodeObject = binaryTree.CreateNodeObject();
        node.nodeObject.SetValues(value, currentLayer);
        node.nodeObject.gameObject.transform.parent = this.nodeObject.gameObject.transform;
        return node;
    }

    public void PrintNode()
    {
        Debug.Log("Value: " + this.value + ";Layer: " + this.layer);
        if (leftNode != null)
            leftNode.PrintNode();
        if (rightNode != null)
            rightNode.PrintNode();
    }

}
