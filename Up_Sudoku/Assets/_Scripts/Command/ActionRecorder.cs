using System.Collections.Generic;
using GameSystem;
using XomracUtilities.Patterns;

namespace Command
{
    public class ActionRecorder : Singleton<ActionRecorder>
    {

        #region Fields

        private Stack<BaseAction> actions = new();

        #endregion
        
        #region LifeCycle

        private void OnEnable()
        {
            GameManager.NewGameStarted += OnNewGameStarted;
        }
        
        private void OnDisable()
        {
            GameManager.NewGameStarted -= OnNewGameStarted;
        }

        #endregion

        #region Callbacks

        private void OnNewGameStarted(bool newGrid)
        {
            actions = new Stack<BaseAction>();
        }

        #endregion

        #region Methods
        
        public void Record(BaseAction action)
        {
            actions.Push(action);
            action.Execute();
        }

        public void Undo()
        {
            if (actions.TryPop(out BaseAction action))
            {
                action.Undo();
            }
        }
        
        #endregion
    }
}