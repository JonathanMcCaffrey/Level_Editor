using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor
{
    public class EditorMoveAction : IEditorStackAction
    {
        #region Fields
        int m_OwnerId = 0;
        #endregion

        #region Construction
        public EditorMoveAction(int a_OwnerId)
        {
            m_OwnerId = a_OwnerId;
        }
        #endregion

        #region Methods
        public void Action()
        {

        }
        #endregion
    }
}
