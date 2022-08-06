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

    public List<Node> _nodeList;
    public List<Block> _blockList;

    private void Start()
    {
        GenerateGrid();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var freeNodes = _nodeList
                .Where(n => n.OccupiedBlock == null)
                .OrderBy(b => Random.value)
                .ToList();
            foreach (var node in freeNodes.Take(1))
            {
                SpawnBlock(node);
            }

            if (freeNodes.Count() <= 1)
            {
                Debug.Log("U lose");
            }
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

    void SpawnBlock(Node node)
    {
        var block = Instantiate(_blockPrefab, node.transform.position, Quaternion.identity);
        block.Init();
        block.SetBlock(node);
    }
}
