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

    public bool Merging;
    int BlockNum
    {
        get { return Random.value > 0.8f ? 4 : 2; }
    }
    public TextMeshPro blockNumText;

    public void Init()
    {
        blockNum = BlockNum;
        blockNumText.text = blockNum.ToString();
    }

    public void SetBlock(Node node)
    {
        this.node = node;
        node.OccupiedBlock = this;
    }

    public void MergeBlock(Block blockToMergeWith)
    {
        MergingBlock = blockToMergeWith;
        node.OccupiedBlock = null;
        blockToMergeWith.Merging = true;
    }

    public bool CanMerge(int value)
    {
        return blockNum == value;
    }
}
