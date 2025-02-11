using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject cubePrefab; // ť�� ������
    public int rows = 5;  // �� ����
    public int cols = 5;  // �� ����
    public float spacing = 1f;  // ť�� �� ����

    private Node[,] grid; // ��� �迭
    public Node[,] Grid => grid;

    void Awake()
    {
        GenerateGrid();
    }



    /// <summary>
    /// Grid ��ǥ ��
    /// 02 12 22 32 42
    /// 01(5) 11(6...������ for��) 21 31 41
    /// 00(0) 10(1) 20(2) 30(3) 40(4)
    /// </summary>
    void GenerateGrid()
    {
        grid = new Node[rows, cols]; 

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Debug.Log(col + " " + row);
                Vector3 position = new Vector3(col * spacing, 0, row * spacing); // ť�� ��ġ
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);

                // ť���� �߽� ��ǥ ��� (�⺻������ ���� ��ġ�� �߽��̹Ƿ� �״�� ���)
                Vector3 centerPosition = cube.transform.position;

                // ��� ���� �� ����
                grid[col, row] = new Node(new Vector2Int(col, row), centerPosition, cube);
            }
        }
    }
}

// ��� Ŭ���� ����
public class Node
{
    public Vector2Int GridPosition { get; private set; }  // �׸��� ��ǥ
    public Vector3 WorldPosition { get; private set; }  // ť���� �߽� ���� ��ǥ
    public GameObject CubeObject { get; private set; }  // �ش� ��ġ�� ť�� ������Ʈ

    public Node(Vector2Int gridPosition, Vector3 worldPosition, GameObject cubeObject)
    {
        GridPosition = gridPosition;
        WorldPosition = worldPosition;
        CubeObject = cubeObject;
    }
}
