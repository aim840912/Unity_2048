using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private int _width = 4;
    private int _heigh = 4;

    [SerializeField]
    private Node _nodePrefab;

    [SerializeField]
    private Block _blockPrefab;

    private List<Node> _nodeList;
    private List<Block> _blockList;

    private void Start()
    {
        GenerateGrid();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.LeftArrow))
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveBlock(Vector2.right);
        }
    }

    void GenerateGrid()
    {
        _nodeList = new List<Node>();
        _blockList = new List<Block>();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _heigh; y++)
            {
                var node = Instantiate(_nodePrefab, new Vector2(x, y), Quaternion.identity);
                _nodeList.Add(node);
            }
        }
    }

    void SpawnsNewBlocks()
    {
        var freeNodes = _nodeList
            .Where(n => n.OccupiedBlock == null)
            .OrderBy(b => Random.value)
            .ToList();
        foreach (var node in freeNodes.Take(1))
        {
            SpawnBlockPos(node);
        }
        Debug.Log(freeNodes.Count());
        if (freeNodes.Count() <= 1)
        {
            Debug.Log("U lose");
        }
    }

    void SpawnBlockPos(Node node)
    {
        var block = Instantiate(_blockPrefab, node.transform.position, Quaternion.identity);
        block.Init();
        block.SetBlock(node);
        _blockList.Add(block);
    }

    void MoveBlock(Vector2 dir)
    {
        var orderBlocks = _blockList
            .OrderBy(b => b.transform.position.x)
            .ThenBy(b => b.transform.position.y)
            .ToList();

        foreach (var orderBlock in orderBlocks)
        {
            var nextBlock = orderBlock.node;

            do
            {
                orderBlock.SetBlock(nextBlock);

                var possibleNode = GetNodeAtPosition(nextBlock.pos + dir);
                if (possibleNode.OccupiedBlock != null) { }
            } while (nextBlock != null);
        }

        SpawnsNewBlocks();
    }

    Node GetNodeAtPosition(Vector2 pos)
    {
        return _nodeList.FirstOrDefault(n => n.pos == pos);// FirstOrDefault Linq語法
    }
}
