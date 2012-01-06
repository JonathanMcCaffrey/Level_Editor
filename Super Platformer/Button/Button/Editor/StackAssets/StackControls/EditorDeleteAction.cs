using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor
{
    public struct EditorDeleteAction : IEditorStackAction
    {
        ActionFlag m_ActionFlag = ActionFlag.Delete;
        int m_OwnerId = 0;

        public void Action()
        {

        }

    }
}
