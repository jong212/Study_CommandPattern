using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Player player;
    public CommandManager commandManager; 
    public GridGenerator gridGenerator;
    public GameObject playerPrefab;

    public Button Up;
    public Button Down;
    public Button Left;        
    public Button Right;

    public Button Undo;
    public Button Replay;
    public Button Jump;
    private void Start()
    {
        Node FirstNode = gridGenerator.Grid[0, 0];
        Vector3 spawnPosition = new Vector3(FirstNode.WorldPosition.x, 1.5f, FirstNode.WorldPosition.z);
        GameObject playerObj = Instantiate(playerPrefab.gameObject, spawnPosition, Quaternion.identity);
        playerObj.TryGetComponent<Player>(out Player player);
        this.player = player;
        this.player.currentPlayerNode = FirstNode;
        this.player.gridX = gridGenerator.rows;
        this.player.gridY = gridGenerator.cols;
        this.player.gridxy = gridGenerator.Grid;



        Up.onClick.AddListener(() => ExecuteMove(new Vector2Int(0, 1)));    // (1, 0) -> 위쪽
        Down.onClick.AddListener(() => ExecuteMove(new Vector2Int(0, -1)));  // (-1, 0) -> 아래쪽
        Left.onClick.AddListener(() => ExecuteMove(new Vector2Int(-1, 0))); // (0, -1) -> 왼쪽
        Right.onClick.AddListener(() => ExecuteMove(new Vector2Int(1, 0))); // (0, 1) -> 오른쪽

        Undo.onClick.AddListener(() => commandManager.Undo());
        Replay.onClick.AddListener(() =>
        {
            playerObj.transform.position = spawnPosition;
            player.currentPlayerNode = FirstNode;
            commandManager.RePlay();
        });

        Jump.onClick.AddListener(() => ExecuteCommand(new JumpCommand(player)));
    }



    public void ExecuteMove(Vector2Int dir)
    {
        ICommand moveCommand = new MoveCommand(player, dir);
        commandManager.ExecuteCommand(moveCommand);
        commandManager.RecordCommand(moveCommand);
    }

    public void ExecuteCommand(ICommand command)
    {
        command.Execute(); // 실행만 하고 기록은 하지 않음
    }

}
