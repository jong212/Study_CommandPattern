using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Player player;
    private Vector2Int direction;
    private Vector2Int prevPosition ; // ���� ��ġ ����

    public MoveCommand(Player player, Vector2Int direction)
    {
        this.player = player;
        this.direction = direction;
    }
    public void Execute() 
    {
        prevPosition = player.currentPlayerNode.GridPosition; // ���� ��ġ ����

        player.Move(direction);
    }

    public void Undo()
    {
        Vector2Int reverseDirection = prevPosition - player.currentPlayerNode.GridPosition;
        player.Move(reverseDirection);
    }
}
