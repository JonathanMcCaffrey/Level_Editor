using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor
{
    class BinaryTree
    {
        #region Fields
        private BinaryTreeNode m_HeadNode;
        #endregion

        public void FindFirstEmptyNode()
        {

        }

        public void FindLastNode()
        {
            bool isFound = false;
            BinaryTreeNode tempSearchContainer = m_HeadNode;
            BinaryTreeNode tempFoundContainer = m_HeadNode;

            while (isFound == false)
            {
                if ((tempSearchContainer = tempSearchContainer.FindOldestChild()) != null)
                {
                    tempFoundContainer = tempSearchContainer;
                }
                else if (tempFoundContainer != null)
                {
                    isFound = true;
                }
                else
                {
                    throw new Exception("Error");
                }
            }
        }
    }
}
