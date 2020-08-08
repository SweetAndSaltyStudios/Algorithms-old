using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class Node : IHeapItem<Node>
    {
        #region VARIABLES

        #endregion VARIABLES

        #region PROPERTIES     

        public NODE_TYPE Type
        {
            get;
            set;
        }
        public int HeapIndex
        {
            get;
            set;
        }
        public int G_Cost
        {
            get;
            set;
        }
        public int H_Cost
        {
            get;
            set;
        }
        public int F_Cost
        {
            get
            {
                return G_Cost + H_Cost;
            }
        }
        public Node ParentNode
        {
            get;
            set;
        }
        public NodeVisual NodeVisual
        {
            get;
            private set;
        }
        public Vector2Int GridPosition
        {
            get;
            private set;
        }

        #endregion PROPERTIES

        #region CONSTRUCTORS

        public Node(Vector2Int gridPosition, NodeVisual nodeVisual)
        {
            GridPosition = gridPosition;

            NodeVisual = nodeVisual;
        }

        #endregion CONSTRUCTORS

        #region CUSTOM_FUNCTIONS  

        public int CompareTo(Node nodeToCompare)
        {
            var result = F_Cost.CompareTo(nodeToCompare.F_Cost);

            if(result == 0)
            {
                result = H_Cost.CompareTo(nodeToCompare.H_Cost);
            }

            return -1 * result;
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
