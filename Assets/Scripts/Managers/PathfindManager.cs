using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class PathfindManager : Singelton<PathfindManager>
    {
        #region VARIABLES

        private bool fullControl;

        [Space]
        [Header("Colors")]
        public Color32 NodeEvaluatedColor;
        public Color32 NodeUnderInvestigationColor;
        public Color32 RetracePathColor;
        public Color32 FinalPathColor;

        public Stack<Node> Path = new Stack<Node>();
        public Heap<Node> OpenSet;
        public HashSet<Node> ClosedSet = new HashSet<Node>();

        private const int DIAGONAL_MOVEMENT_COST = 14;
        private const int ADJACENT_MOVEMET_COST = 10;

        [Range(0f, 1f)]
        public float AnimationDuration = 0.1f;
        private readonly WaitUntil waitUntil = new WaitUntil(() => InputManager.Instance.IsMouseLeftPressed);

        private bool isPaused;
        private PATHFINDING_TYPE pathfindingType;

        #endregion VARIABLES

        #region PROPERTIES

        public bool IsSearchingPath
        {
            get;
            private set;
        }

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS    

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        public void SwitchFullControl()
        {
            fullControl = !fullControl;
        }

        public void ChangePathfindingType(PATHFINDING_TYPE type)
        {
            if(pathfindingType == type)
            {
                return;
            }

            pathfindingType = type;
        }

        public void StartSearchAlgorithm()
        {
            if(IsSearchingPath || GridManager.Instance.GridIsCreated == false)
                return;

            ResetValues();

            IsSearchingPath = true;

            switch(pathfindingType)
            {
                //case PATHFINDING_TYPE.BREADTH_FIRST_SEARCH:          

                //    break;

                //case PATHFINDING_TYPE.DEPTH_FIRST_SEARCH:

                //    break;

                case PATHFINDING_TYPE.DIJKSTRA:

                    StartCoroutine(IDijkstra_Search());

                    break;

                case PATHFINDING_TYPE.A_STAR:

                    StartCoroutine(IA_StarSearch());

                    break;

                default:


                    break;
            }
        }

        public void Pause()
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
        }

        public void ResetValues()
        {
            Path.Clear();
            OpenSet = null;
            ClosedSet.Clear();
        }

        private IEnumerator IDijkstra_Search()
        {
            var startNode = GridManager.Instance.StartNode;
            var targetNode = GridManager.Instance.TargetNode;
            Node currentNode = null;
            var neighbourNodes = new List<Node>();

            OpenSet = new Heap<Node>(GridManager.Instance.GridSize_Max);
            ClosedSet = new HashSet<Node>();

            OpenSet.Add(startNode);

            while(OpenSet.Count > 0)
            {
                currentNode = OpenSet.RemoveFirst();

                ClosedSet.Add(currentNode);

                if(currentNode != startNode && currentNode != targetNode)
                {
                    currentNode.NodeVisual.AnimateColor(NodeEvaluatedColor);

                    if(fullControl)
                    {
                        yield return waitUntil;
                    }

                    yield return new WaitForSeconds(AnimationDuration);
                }

                if(currentNode == targetNode)
                {
                    yield return StartCoroutine(IRetracePath(startNode, targetNode));

                    yield return StartCoroutine(IShowPath());

                    IsSearchingPath = false;

                    yield break;
                }

                neighbourNodes = GridManager.Instance.GetNeighbours(currentNode);

                foreach(var neighbourNode in neighbourNodes)
                {
                    if(neighbourNode.Type == NODE_TYPE.OBSTACLE
                        ||
                        ClosedSet.Contains(neighbourNode))
                    {
                        continue;
                    }

                    var newMovementCostToNeighbourNode = currentNode.G_Cost +
                        GetDistanceBetweenNodes(currentNode, neighbourNode);

                    if(newMovementCostToNeighbourNode < neighbourNode.G_Cost
                        ||
                        OpenSet.Contains(neighbourNode) == false)
                    {
                        neighbourNode.G_Cost = newMovementCostToNeighbourNode;
                        neighbourNode.ParentNode = currentNode;

                        if(OpenSet.Contains(neighbourNode) == false)
                        {
                            OpenSet.Add(neighbourNode);

                            if(neighbourNode != targetNode)
                            {
                                neighbourNode.NodeVisual.AnimateColor(NodeUnderInvestigationColor);

                                if(fullControl)
                                {
                                    yield return waitUntil;
                                }

                                yield return new WaitForSeconds(AnimationDuration);

                            }
                        }
                    }               
                }           
            }   
        }

        private IEnumerator IA_StarSearch()
        {
            var startNode = GridManager.Instance.StartNode;
            var targetNode = GridManager.Instance.TargetNode;
            Node currentNode = null;
            var neighbourNodes = new List<Node>();

            OpenSet = new Heap<Node>(GridManager.Instance.GridSize_Max);
            ClosedSet = new HashSet<Node>();

            OpenSet.Add(startNode);

            while(OpenSet.Count > 0)
            {
                currentNode = OpenSet.RemoveFirst();

                ClosedSet.Add(currentNode);

                if(currentNode != startNode && currentNode != targetNode)
                {
                    currentNode.NodeVisual.AnimateColor(NodeEvaluatedColor);

                    if(fullControl)
                    {
                        yield return waitUntil;
                    }

                    yield return new WaitForSeconds(AnimationDuration);
                }

                if(currentNode == targetNode)
                {
                    yield return StartCoroutine(IRetracePath(startNode, targetNode));

                    yield return StartCoroutine(IShowPath());

                    IsSearchingPath = false;

                    yield break;
                }

                neighbourNodes = GridManager.Instance.GetNeighbours(currentNode);

                foreach(var neighbourNode in neighbourNodes)
                {
                    if(neighbourNode.Type == NODE_TYPE.OBSTACLE
                        ||
                        ClosedSet.Contains(neighbourNode))
                    {
                        continue;
                    }

                    var newMovementCostToNeighbourNode = currentNode.G_Cost + 
                        GetDistanceBetweenNodes(currentNode, neighbourNode);

                    if(newMovementCostToNeighbourNode < neighbourNode.G_Cost 
                        || 
                        OpenSet.Contains(neighbourNode) == false)
                    {
                        neighbourNode.G_Cost = newMovementCostToNeighbourNode;
                        neighbourNode.H_Cost = GetDistanceBetweenNodes(neighbourNode, targetNode);
                        neighbourNode.ParentNode = currentNode;     

                        if(OpenSet.Contains(neighbourNode) == false)
                        {
                            OpenSet.Add(neighbourNode);

                            if(neighbourNode != targetNode)
                            {
                                neighbourNode.NodeVisual.AnimateColor(NodeUnderInvestigationColor);

                                if(fullControl)
                                {
                                    yield return waitUntil;
                                }

                                yield return new WaitForSeconds(AnimationDuration);
                            }
                        }                  
                    }                  
                }
            }
        }

        private IEnumerator IRetracePath(Node startNode, Node targetNode)
        {
            var currentNode = targetNode;

            while(currentNode != startNode)
            {
                Path.Push(currentNode);

                if(currentNode != startNode && currentNode != targetNode)
                {
                    currentNode.NodeVisual.AnimateColor(RetracePathColor);

                    if(fullControl)
                    {
                        yield return waitUntil;
                    }
                   
                    yield return new WaitForSeconds(AnimationDuration);
                }

                currentNode = currentNode.ParentNode;
            }
        }

        private IEnumerator IShowPath()
        {
            Node currentNode = null;

            while(Path.Count > 1)
            {
                currentNode = Path.Pop();

                currentNode.NodeVisual.AnimateColor(FinalPathColor);

                if(fullControl)
                {
                    yield return waitUntil;
                }

                yield return new WaitForSeconds(AnimationDuration);
            }
        }

        private int GetDistanceBetweenNodes(Node node_A, Node node_B)
        {
            var distance_X = Mathf.Abs(node_A.GridPosition.x - node_B.GridPosition.x);
            var distance_Y = Mathf.Abs(node_A.GridPosition.y - node_B.GridPosition.y);

            if(distance_X > distance_Y)
            {
                return DIAGONAL_MOVEMENT_COST * distance_Y + ADJACENT_MOVEMET_COST * (distance_X - distance_Y);
            }

            return DIAGONAL_MOVEMENT_COST * distance_X + ADJACENT_MOVEMET_COST * (distance_Y - distance_X);

        }

        #endregion CUSTOM_FUNCTIONS
    }
}
