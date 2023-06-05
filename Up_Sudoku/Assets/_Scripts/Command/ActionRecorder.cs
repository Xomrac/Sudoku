using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XomracUtilities.Patterns;

public class ActionRecorder : Singleton<ActionRecorder>
{

    public Stack<BaseAction> actions = new();
    public void Record(BaseAction action)
    {
        actions.Push(action);
        action.Execute();
    }

    public void Undo()
    {
        if (actions.Count>0)
        {
            var action = actions.Pop();
            action.Undo();
        }
    }
}
