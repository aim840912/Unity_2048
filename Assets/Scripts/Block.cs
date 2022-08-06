using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Block : MonoBehaviour
{
    public Node node;
    Block MergingBlock;

    int BlockNum
    {
        get { return Random.value > 0.8f ? 4 : 2; }
    }
    public TextMeshProUGUI blockNumText;

    public void Init()
    {
        blockNumText.text = BlockNum.ToString();
    }

    public bool CanMerge(int value)
    {
        return this.BlockNum == value;
    }

    public void MergeBlock(Block blockToMergeWith)
    {
        MergingBlock = blockToMergeWith;
    }
}
