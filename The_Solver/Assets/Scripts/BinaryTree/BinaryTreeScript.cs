using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTreeScript : MonoBehaviour
{
    TreeNode rootNode;
    public int[] array;

    private void Start()
    {
        for (int i = 0; i < array.Length; i++)
        {
            AddNode(array[i]);
        }
    }

    public void AddNode( int value )
    {
        if (rootNode == null)
            rootNode = new TreeNode(value);
    }
}
