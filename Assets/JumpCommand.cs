using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : ICommand
{
    private Player player;

    public JumpCommand(Player player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.Jump(); // 점프 동작 실행
    }

    public void Undo()
    {
        // 점프는 되돌리기에서 제외되므로 비워둠
    }
}
