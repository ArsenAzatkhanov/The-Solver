using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodePrefab : MonoBehaviour
{
    [SerializeField] TextMeshPro valueText, layerText;
    public TreeNode thisNode;
    public Vector2 shiftValues;

    public void SetValues(int value, int layer, TreeNode node)
    {
        layerText.text = "Layer: " + layer;
        valueText.text = "Value: " + value;
        thisNode = node;
    }


    public NodePrefab CheckAnotherCollision()
    {
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

    public static NodePrefab FindCommonNode(TreeNode firstNode, TreeNode secondNode)
    {
        TreeNode firstParent = firstNode.GetParent(), 
            secondParent = secondNode.GetParent();
        while(true)
        {
            Debug.Log("Comparing " + firstParent.GetIndex() + " with " + secondParent.GetIndex());
            if(firstParent == secondParent)
                return firstParent.nodeObject;
            
            if (firstParent.GetParent() == null || secondParent.GetParent() == null)
                return firstParent.nodeObject;

            firstParent = firstParent.GetParent();
            secondParent = secondParent.GetParent();
        }
    }

}
