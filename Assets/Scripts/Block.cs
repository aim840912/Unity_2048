using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public int blockNum;
    public Node node;
    public Vector2 pos => transform.position;
    public Block MergingBlock;

    public TextMeshPro blockNumText;

    public void Init(int value)
    {
        blockNum = value;
        blockNumText.text = blockNum.ToString();
    }

    public void SetBlock(Node node)
    {
        if (this.node != null)
            this.node.OccupiedBlock = null;
        this.node = node;
        this.node.OccupiedBlock = this;
        Debug.Log(this.pos);
    }
}
