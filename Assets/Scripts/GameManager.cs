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
        if (Input.GetKeyDown(KeyCode.A))
            MoveBlock(Vector2.left);
        if (Input.GetKeyDown(KeyCode.D))
            MoveBlock(Vector2.right);
        if (Input.GetKeyDown(KeyCode.W))
            MoveBlock(Vector2.up);
        if (Input.GetKeyDown(KeyCode.S))
            MoveBlock(Vector2.down);
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
        var block = Instantiate(_blockPrefab, node.pos, Quaternion.identity);
        block.Init();
        block.SetBlock(node);
        _blockList.Add(block);
    }

    void MoveBlock(Vector2 dir)
    {
        var orderedBlocks = _blockList.OrderBy(b => b.pos.x).ThenBy(b => b.pos.y).ToList();
        if (dir == Vector2.right || dir == Vector2.up)
            orderedBlocks.Reverse();

        foreach (var block in orderedBlocks)
        {
            var currentNode = block.node;

            do
            {
                block.SetBlock(currentNode);

                var nextNode = GetNodeAtPosition(currentNode.pos + dir); // 超出就會是null

                if (nextNode != null)
                {
                    if (nextNode.OccupiedBlock != null)
                    {
                        // if (nextNode.OccupiedBlock.CanMerge(block.blockNum))
                        // {
                        //     block.MergeBlock(nextNode.OccupiedBlock);
                        // }
                    }
                    else if (nextNode.OccupiedBlock == null)
                    {
                        currentNode = nextNode;
                    }
                }
            } while (currentNode != block.node);

            block.transform.position = block.node.pos;
        }

        SpawnsNewBlocks();
    }

    Node GetNodeAtPosition(Vector2 pos)
    {
        return _nodeList.FirstOrDefault(n => n.pos == pos);
    }
}
