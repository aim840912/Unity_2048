using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public Node node;
    public Vector2 pos => transform.position;
    int _blackNum;

    int BlockNum
    {
        get { return Random.value > 0.8f ? 4 : 2; }
    }
    public TextMeshPro blockNumText;

    public void Init()
    {
        _blackNum = BlockNum;
        blockNumText.text = _blackNum.ToString();
    }

    public void SetBlock(Node node)
    {
        this.node = node;
        node.OccupiedBlock = this;
    }

    public void MergeBlock(Block blockToMergeWith)
    {
        node.OccupiedBlock = null;
    }

    public bool CanMerge(int value)
    {
        return this._blackNum == value;
    }
}
