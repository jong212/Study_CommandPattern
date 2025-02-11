using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject cubePrefab; // 큐브 프리팹
    public int rows = 5;  // 행 개수
    public int cols = 5;  // 열 개수
    public float spacing = 1f;  // 큐브 간 간격

    private Node[,] grid; // 노드 배열
    public Node[,] Grid => grid;

    void Awake()
    {
        GenerateGrid();
    }



    /// <summary>
    /// Grid 좌표 ▼
    /// 02 12 22 32 42
    /// 01(5) 11(6...순서로 for돔) 21 31 41
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
                Vector3 position = new Vector3(col * spacing, 0, row * spacing); // 큐브 위치
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);

                // 큐브의 중심 좌표 계산 (기본적으로 생성 위치가 중심이므로 그대로 사용)
                Vector3 centerPosition = cube.transform.position;

                // 노드 생성 및 저장
                grid[col, row] = new Node(new Vector2Int(col, row), centerPosition, cube);
            }
        }
    }
}

// 노드 클래스 정의
public class Node
{
    public Vector2Int GridPosition { get; private set; }  // 그리드 좌표
    public Vector3 WorldPosition { get; private set; }  // 큐브의 중심 월드 좌표
    public GameObject CubeObject { get; private set; }  // 해당 위치의 큐브 오브젝트

    public Node(Vector2Int gridPosition, Vector3 worldPosition, GameObject cubeObject)
    {
        GridPosition = gridPosition;
        WorldPosition = worldPosition;
        CubeObject = cubeObject;
    }
}
