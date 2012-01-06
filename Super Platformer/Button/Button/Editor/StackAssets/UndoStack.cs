using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor
{
    public static class UndoActionStack
    {
        #region Fields
        private static Stack<IEditorStackAction> m_ActionStack;
        #endregion

        #region Methods
        public static void Push(IEditorStackAction a_ActionStack)
        {
            m_ActionStack.Push(a_ActionStack);
        }

        public static void Pop()
        {
            IEditorStackAction tempContainer = m_ActionStack.Pop();
            tempContainer.Action();
        }

        public static void Clear()
        {
            m_ActionStack.Clear();
        }
        #endregion
    }
}
