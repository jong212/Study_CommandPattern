using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public Stack<ICommand> commandHistory = new Stack<ICommand>();
    public Queue<ICommand> commandQueue = new Queue<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        commandHistory.Push(command);
    }

    public void RecordCommand(ICommand command)
    {
        commandQueue.Enqueue(command);
    }
    
    public void Undo()
    {
        if (commandHistory.Count > 0)
        {
            ICommand command = commandHistory.Pop();
            command.Undo();

            // ��� ���� ����ȭ: Stack -> List -> Queue ��ȯ
            List<ICommand> tempList = new List<ICommand>(commandQueue);
            tempList.Remove(command);  // Undo�� ��� ����
            commandQueue = new Queue<ICommand>(tempList); // �ٽ� Queue�� ��ȯ
        }
    }

    public void RePlay() => StartCoroutine(CoReplay());

    private IEnumerator CoReplay()
    {
        float waitTime = 0.7f;
        yield return new WaitForSeconds(waitTime);

        Queue<ICommand> tempQueue = new Queue<ICommand>(commandQueue); // ���� ����


        while (tempQueue.Count > 0)
        {
            ICommand command = tempQueue.Dequeue();
            command.Execute();
            yield return new WaitForSeconds(waitTime);
        }

    }



}
