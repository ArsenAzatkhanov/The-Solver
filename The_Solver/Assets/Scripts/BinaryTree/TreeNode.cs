using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TreeNode : MonoBehaviour
{
    int value;
    TreeNode leftNode, rightNode, parentNode;

    public TreeNode( int value )
    {
        this.value = value;
    }

    public TreeNode(int value, TreeNode parentNode)
    {
        this.value = value;
        this.parentNode = parentNode;
    }

    public void AddNode( int value)
    {
        if (value > this.value)
            SetRight(value);
        else if (value < this.value)
            SetLeft(value);
        else
            return;
    }

    public void SetLeft( int value )
    {
        if (leftNode == null)
        {
            TreeNode node = new TreeNode(value, this);
            this.leftNode = node;
        }
        else
            leftNode.AddNode(value);
    }

    public void SetRight(int value)
    {
        if (rightNode == null)
        {
            TreeNode node = new TreeNode(value, this);
            this.rightNode = node;
        }
        else
            rightNode.AddNode(value);  
    }

}
