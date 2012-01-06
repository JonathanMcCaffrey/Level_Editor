using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor
{
    class BinaryTreeNode
    {
        #region Fields
        private int m_Owner;

        BinaryTreeNode m_Parent;
        BinaryTreeNode m_LeftChild;
        BinaryTreeNode m_RightChild;

        BinaryTreeNode m_LeftNode;
        BinaryTreeNode m_RightNode;
        #endregion

        #region Properties
        public int Owner
        {
            get { return m_Owner; }
        }
        #endregion

        #region Constructor

        public BinaryTreeNode(int a_Owner, BinaryTreeNode a_Parent)
        {

        }

        #endregion

        #region Methods
        public BinaryTreeNode FindFirstEmptyNode()
        {
            return new BinaryTreeNode(0, m_Parent); 
        }

        public BinaryTreeNode FindOldestChild()
        {
            if (m_RightChild != null)
            {
                return m_RightChild;
            }
            else if (m_LeftChild != null)
            {
                return m_LeftChild;
            }
            else
            {
                return null;
            }
        }

        void Add(BinaryTreeNode a_Child)
        {
            if(m_LeftChild == null)
            {
                m_LeftChild = a_Child;
            }
            else if(m_RightChild == null)
            {
                m_LeftChild = a_Child;
            }
        }
        #endregion
    }
}
