using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker
{
    private static Stack<ICommand> undoStack = new Stack<ICommand>();
    private static Stack<ICommand> redoStack = new Stack<ICommand>();

    public static void ExecuteCommand(ICommand command)
    {
        command.Execute();//ICommand받아와서 실행함
        undoStack.Push(command);

        redoStack.Clear();
    }

    public static void Undocommand()//뒤로 
    {
        if (undoStack.Count > 0)
        {
            ICommand command = undoStack.Pop();
            redoStack.Push(command);
            command.Undo();
        }
    }

    public static void Redocommand()//앞으로 
    {
        if (redoStack.Count > 0)
        {
            ICommand command = redoStack.Pop();
            undoStack.Push(command);
            command.Execute();
        }
    }

}
