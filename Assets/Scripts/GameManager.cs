using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _width = 4;
    private int _heigh = 4;

    [SerializeField]
    private Node _nodePrefab;

    [SerializeField]
    private Block _blockPrefab;

    private void Start()
    {
        GenerateGrid();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { }
    }

    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _heigh; y++)
            {
                var node = Instantiate(_nodePrefab, new Vector2(x, y), Quaternion.identity);
            }
        }
    }

    void SpawnBlock(Node node,int num)
    {
        var block = Instantiate(_blockPrefab, node.transform.position, Quaternion.identity);
        block.Init();
    }
}
