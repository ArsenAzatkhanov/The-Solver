using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TreeNode 
{
    int value, layer, index;
    TreeNode leftNode, rightNode, parentNode;
    BinaryTreeScript binaryTree;
    public NodePrefab nodeObject;
    public BinaryTreeScript.LeafSide leafSide;

    public TreeNode( int value, int layer, BinaryTreeScript binaryTree, int index, BinaryTreeScript.LeafSide leafSide)
    {
        this.value = value;
        this.layer = layer;
        this.binaryTree = binaryTree;
        this.index = index;
        this.leafSide = leafSide;
    }

    public TreeNode(int value, int layer, TreeNode parentNode, BinaryTreeScript binaryTree, int index, BinaryTreeScript.LeafSide leafSide)
    {
        this.value = value;
        this.layer = layer;
        this.parentNode = parentNode;
        this.binaryTree = binaryTree;
        this.index = index;
        this.leafSide = leafSide;
    }

    public TreeNode GetParent()
    {
        return parentNode;
    }

    public int GetIndex()
    {
        return index;
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
            TreeNode node = SetNode(value, currentLayer, BinaryTreeScript.LeafSide.Left);
            node.nodeObject.gameObject.transform.localPosition = Vector3.zero + new Vector3(-node.nodeObject.shiftValues.x, -node.nodeObject.shiftValues.y, 0);

            ShiftCollision(node);

            SetLineRenderer(node, this);
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
            TreeNode node = SetNode(value, currentLayer, BinaryTreeScript.LeafSide.Right);
            node.nodeObject.gameObject.transform.localPosition = Vector3.zero + new Vector3(node.nodeObject.shiftValues.x, -node.nodeObject.shiftValues.y, 0);

            ShiftCollision(node);


            SetLineRenderer(node, this);
            this.rightNode = node;
        }
        else
        {
            currentLayer++;
            rightNode.AddNode(value, currentLayer);
        }
  
    }


    public TreeNode GetLeft()
    {
        return leftNode;
    }



    public TreeNode GetRight()
    {
        return rightNode;
    }



    TreeNode SetNode(int value, int currentLayer, BinaryTreeScript.LeafSide leafSide)
    {
        TreeNode node = new TreeNode(value, currentLayer, this, binaryTree, binaryTree.index, leafSide);
        binaryTree.index++;
        node.nodeObject = binaryTree.CreateNodeObject();
        node.nodeObject.SetValues(value, currentLayer, node);
        node.nodeObject.gameObject.transform.parent = this.nodeObject.gameObject.transform;
        node.nodeObject.gameObject.name = "Node V:" + value + " L:" + currentLayer;



        return node;
    }

    public void ShiftCollision(TreeNode node)
    {
        NodePrefab collidedNode = node.nodeObject.FindAnotherCollision();

        if (collidedNode == null) return;

        NodePrefab commonNode = NodePrefab.FindCommonNode(node, collidedNode.connectedTreeNode);

        NodePrefab nodeToShift;

        if (node.leafSide == BinaryTreeScript.LeafSide.Left)
        {
            nodeToShift = commonNode.connectedTreeNode.GetRight().nodeObject;
            nodeToShift.transform.localPosition += new Vector3(nodeToShift.shiftValues.x * 2, 0, 0);
            Debug.Log("Moved " + nodeToShift.gameObject.name + " to right");
        }

        else if (node.leafSide == BinaryTreeScript.LeafSide.Right)
        {
            nodeToShift = commonNode.connectedTreeNode.GetLeft().nodeObject;
            nodeToShift.transform.localPosition += new Vector3(-nodeToShift.shiftValues.x * 2, 0, 0);
            Debug.Log("Moved " + nodeToShift.gameObject.name + " to left");
        }

        UpdateLineRenderer(commonNode.connectedTreeNode);


    }

    static void SetLineRenderer(TreeNode nodeFrom, TreeNode nodeTo)
    {
        LineRenderer lineRenderer = nodeFrom.nodeObject.gameObject.GetComponent<LineRenderer>() ? nodeFrom.nodeObject.gameObject.GetComponent<LineRenderer>() : nodeFrom.nodeObject.gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, nodeFrom.nodeObject.transform.position);
        lineRenderer.SetPosition(1, nodeTo.nodeObject.transform.position);
    }

    public static void UpdateLineRenderer(TreeNode node)
    {
        if(node.GetLeft() != null)
        {
            SetLineRenderer(node.GetLeft(), node);
            UpdateLineRenderer(node.GetLeft());
        }
        if (node.GetRight() != null)
        {
            SetLineRenderer(node.GetRight(), node);
            UpdateLineRenderer(node.GetRight());
        }
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
