using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodePrefab : MonoBehaviour
{
    [SerializeField] TextMeshPro valueText, layerText;
    public TreeNode connectedTreeNode;
    public Vector2 shiftValues;

    public void SetValues(int value, int layer, TreeNode node)
    {
        layerText.text = "Layer: " + layer;
        valueText.text = "Value: " + value;
        connectedTreeNode = node;
    }


    public NodePrefab FindAnotherCollision()
    {
        if (connectedTreeNode.leafSide == BinaryTreeScript.LeafSide.Root) return null;

        Collider2D[] collisions = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);
        NodePrefab compareNode;
        for (int i = 0; i < collisions.Length; i++)
        {
            compareNode = collisions[i].gameObject.GetComponent<NodePrefab>();
            if (compareNode != this)
                return compareNode;
        }
        return null;
    }

    public bool CheckAnotherCollision()
    {
        Collider2D[] collisions = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);
        NodePrefab compareNode;
        for (int i = 0; i < collisions.Length; i++)
        {
            compareNode = collisions[i].gameObject.GetComponent<NodePrefab>();
            if (compareNode != this)
                return true;
        }
        return false;
    }



    public static NodePrefab FindCommonNode(TreeNode firstNode, TreeNode secondNode)
    {
        TreeNode firstParent = firstNode.GetParent(), 
            secondParent = secondNode.GetParent();
        while(true)
        {
            if(firstParent == secondParent)
                return firstParent.nodeObject;
            
            if (firstParent.GetParent() == null || secondParent.GetParent() == null)
                return firstParent.nodeObject;

            firstParent = firstParent.GetParent();
            secondParent = secondParent.GetParent();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

}
