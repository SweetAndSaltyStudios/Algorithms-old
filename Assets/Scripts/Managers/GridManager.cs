using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public enum SELECTION_TYPE
    {

    }

    public class GridManager : Singelton<GridManager>
    {
        #region VARIABLES

        [Range(0, 6)]
        public float X;
        [Range(0, 6)]
        public float Y;

        [Space]
        [Header("PREFABS")]
        public NodeVisual NodeVisualPrefab = null;

        [Space]
        [Header("KEY NODES")]
        public Node TargetNode = null;
        public Node StartNode = null;
        public Node SelectedNode = null;
        public Node PreviousNode = null;
        public Node Node_HoverCurrent = null;
        public Node Node_HoverPrevious = null;

        private bool moveStartNode;
        private bool moveTargetNode;

        [Space]
        [Header("COLORS")]
        public Color32 DefaultNodeColor;
        public Color32 ObstacleNodeColor;
        public Color32 StartNodeColor; 
        public Color32 TargetNodeColor;
        public Color32 HoveredNodeColor;

        [Space]
        [Header("GRID LAYOUT VARIABLES")]
        [Range(4, 100)] public int GridWorldSize_X = 4;
        [Range(4, 100)] public int GridWorldSize_Y = 4;

        private Vector2Int gridSize;
        private readonly float nodeRadius = 1f;

        #endregion VARIABLES

        #region PROPERTIES

        public Node[,] Grid
        {
            get;
            private set;
        }

        public float NodeDiameter
        {
            get;
            private set;
        }

        public int GridSize_Max
        {
            get
            {
                return gridSize.x * gridSize.y;
            }
        }

        public bool GridIsCreated
        {
            get
            {
                return Grid != null || StartNode != null || TargetNode != null;
            }
        }

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            NodeDiameter = nodeRadius * 2;
            gridSize = new Vector2Int(
                Mathf.RoundToInt(GridWorldSize_X / NodeDiameter),
                Mathf.RoundToInt(GridWorldSize_Y / NodeDiameter)
                );
        }

        private void Update()
        {
            if(Grid == null || InputManager.Instance.IsOverUI || PathfindManager.Instance.IsSearchingPath)
            {
                return;
            }

            // HandleHoverOverNode();

            if(InputManager.Instance.IsMouseLeftDown)
            {
                SelectedNode = GetNodeFromWorldPoint(InputManager.Instance.MouseWorldPosition);

                if(SelectedNode == null)
                {
                    return;
                }

                switch(SelectedNode.Type)
                {               
                    case NODE_TYPE.START:

                        moveStartNode = true;

                        break;
                    case NODE_TYPE.TARGET:

                        moveTargetNode = true;

                        break;

                    default:

                        CreateOrDestroyObstacle();

                        break;
                }

                PreviousNode = SelectedNode;
            }

            if(InputManager.Instance.IsMouseLeftPressed)
            {
                SelectedNode = GetNodeFromWorldPoint(InputManager.Instance.MouseWorldPosition);

                if(SelectedNode == null)
                {
                    return;
                }

                if(SelectedNode == PreviousNode)
                {
                    return;
                }

                DragKeyNode();

                PreviousNode = SelectedNode;

            }

            if(InputManager.Instance.IsMouseLeftUp)
            {
                ResetValues();
            }
        }

        private void ResetValues()
        {
            moveStartNode = false;
            moveTargetNode = false;
            SelectedNode = null;
            Node_HoverPrevious = null;
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        private void DragKeyNode()
        {
            if(moveStartNode)
            {
                PreviousNode.Type = NODE_TYPE.DEFAULT;
                PreviousNode.NodeVisual.AnimateColor(DefaultNodeColor, 0.2f);

                StartNode = SelectedNode;
                SelectedNode.Type = NODE_TYPE.START;
                SelectedNode.NodeVisual.AnimateColor(StartNodeColor, 0.1f);
            }
            else if(moveTargetNode)
            {
                PreviousNode.Type = NODE_TYPE.DEFAULT;
                PreviousNode.NodeVisual.AnimateColor(DefaultNodeColor, 0.2f);

                TargetNode = SelectedNode;
                TargetNode.Type = NODE_TYPE.TARGET;
                TargetNode.NodeVisual.AnimateColor(TargetNodeColor, 0.1f);
            }
            else
            {
                if(SelectedNode.Type == NODE_TYPE.START || SelectedNode.Type == NODE_TYPE.TARGET)
                {
                    return;
                }

                CreateOrDestroyObstacle();
            }
        }

        private void CreateOrDestroyObstacle()
        {
            if(SelectedNode.Type == NODE_TYPE.OBSTACLE)
            {
                SelectedNode.Type = NODE_TYPE.DEFAULT;
                SelectedNode.NodeVisual.AnimateColor(DefaultNodeColor, 0.1f);
                SelectedNode.NodeVisual.AnimateScale(Vector2.one, 0.1f);            
            }
            else
            {
                SelectedNode.Type = NODE_TYPE.OBSTACLE;
                SelectedNode.NodeVisual.AnimateColor(ObstacleNodeColor, 0.1f);
                SelectedNode.NodeVisual.AnimateScale(Vector2.one, 0.1f);
            }
        }

        private void HandleHoverOverNode()
        {
            if(SelectedNode != null || PreviousNode != null)
            {
                return;
            }

            Node_HoverCurrent = GetNodeFromWorldPoint(InputManager.Instance.MouseWorldPosition);

            if(Node_HoverCurrent == null)
            {
                return;
            }

            if(Node_HoverCurrent == Node_HoverPrevious)
            {
                return;
            }

            if(Node_HoverCurrent.Type != NODE_TYPE.DEFAULT)
            {
                if(Node_HoverPrevious != null)
                {
                    Node_HoverPrevious.NodeVisual.AnimateColor(DefaultNodeColor, 0.2f);
                }

                return;
            }

            if(Node_HoverPrevious != null)
            {
                Node_HoverPrevious.NodeVisual.AnimateColor(DefaultNodeColor, 0.2f);
            }

            Node_HoverCurrent.NodeVisual.AnimateColor(HoveredNodeColor, 0.1f);

            Node_HoverPrevious = Node_HoverCurrent;
        }

        public void CreateGrid()
        {
            if(Grid == null)
            {
                StartCoroutine(ICreateGrid());
            }        
        }

        public void ClearGrid()
        {
            if(Grid == null)
            {
                return;
            }

            StartCoroutine(IClearGrid());          
        }

        private Node GetNodeFromWorldPoint(Vector2 worldPoint)
        {
            var percent_X = (worldPoint.x + GridWorldSize_X * X) / GridWorldSize_X;
            var percent_Y = (worldPoint.y + GridWorldSize_Y * Y) / GridWorldSize_Y;

            percent_X = Mathf.Clamp01(percent_X);
            percent_Y = Mathf.Clamp01(percent_Y);

            var x = Mathf.RoundToInt((gridSize.x - 1) * percent_X);
            var y = Mathf.RoundToInt((gridSize.y - 1) * percent_Y);

            return Grid[x, y];
        }

        public List<Node> GetNeighbours(Node node)
        {
            var result = new List<Node>();

            for(int x = -1; x <= 1; x++)
                for(int y = -1; y <= 1; y++)
                {
                    if(x == 0 && y == 0)
                        continue;

                    var check_X = node.GridPosition.x + x;
                    var check_Y = node.GridPosition.y + y;

                    if(check_X >= 0 && check_X < gridSize.x && check_Y >= 0 && check_Y < gridSize.y)
                    {
                        result.Add(Grid[check_X, check_Y]);
                    }
                }

            return result;
        }

        private IEnumerator ICreateGrid(float spawnDealy = 0.01f)
        {
            Grid = new Node[gridSize.x, gridSize.y];

            var gridPosition = Vector2Int.zero;

            var worldBottomLeft =
                (Vector2)transform.position -
                Vector2.right * GridWorldSize_X * 0.5f -
                Vector2.up * GridWorldSize_Y * 0.5f;

            var worldPosition = Vector2.zero;

            var waitForSpawnDelay = new WaitForSeconds(spawnDealy);

            var spawnHeight = InputManager.Instance.GetWorldPointFromScreenPoint(new Vector2(0, Screen.height)).y;

            for(int x = 0; x < gridSize.x; x++)
                for(int y = 0; y < gridSize.y; y++)
                {
                    worldPosition =
                        worldBottomLeft +
                        Vector2.right * (x * NodeDiameter + nodeRadius)
                        +
                        Vector2.up * (y * NodeDiameter + nodeRadius);

                    gridPosition = new Vector2Int(x, y);

                    var newNodeVisual = Instantiate(
                        NodeVisualPrefab,
                        worldPosition + (Vector2.up * (spawnHeight - worldPosition.y + 1)),
                        Quaternion.identity,
                        transform);

                    newNodeVisual.gameObject.name = $"Node {worldPosition}";

                    newNodeVisual.AnimatePosition(
                        newNodeVisual.transform.position,
                        worldPosition,
                        LeanTweenType.easeOutBounce,
                        1f);

                    Grid[x, y] = new Node(gridPosition, newNodeVisual);

                    if(gridPosition == Vector2Int.zero)
                    {
                        StartNode = Grid[0, 0];
                        StartNode.NodeVisual.AnimateColor(Color.green);
                        StartNode.Type = NODE_TYPE.START;
                    }
                    else if(gridPosition == new Vector2Int(gridSize.x - 1, gridSize.y - 1))
                    {
                        TargetNode = Grid[gridSize.x - 1, gridSize.y - 1];
                        TargetNode.NodeVisual.AnimateColor(Color.red);
                        TargetNode.Type = NODE_TYPE.TARGET;
                    }

                    yield return null;
                }
        }

        private IEnumerator IClearGrid(float spawnDealy = 0.01f)
        {
            NodeVisual nodeVisual = null;

            var dropHeight = InputManager.Instance.GetWorldPointFromScreenPoint(new Vector2(0, 0)).y - 1;

            for(int x = 0; x < gridSize.x; x++)
                for(int y = 0; y < gridSize.y; y++)
                {
                    nodeVisual = Grid[x, y].NodeVisual;

                    nodeVisual.AnimatePosition(
                        nodeVisual.transform.position,
                        new Vector2(nodeVisual.transform.position.x, dropHeight),
                        LeanTweenType.easeInExpo,
                        0.2f);

                    Destroy(nodeVisual.gameObject, 1f);

                    yield return null;
                }

            Grid = null;
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
