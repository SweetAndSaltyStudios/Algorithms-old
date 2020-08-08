using System.Collections;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class SortingManager : Singelton<SortingManager>
    {
        #region VARIABLES

        private bool fullControl;

        [Space]
        [Header("Colors")]
        public Color32 Grey;
        public Color32 Black;
        public Color32 Red;
        public Color32 Green;

        public PylonVisual PylonVisualPrefab;

        private Pylon[] pylons;

        public int PylonMinValue = 1;
        public int PylonMaxValue = 100;

        private const int MAX_PYLON_COUNT = 56;

        private SORTING_TYPE sortingType;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Start()
        {
            // CreatePylons();    
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        public void SwitchFullControl()
        {
            fullControl = !fullControl;
        }

        public void ChangeSortingType(SORTING_TYPE type)
        {
            if(sortingType == type)
            {
                return;
            }

            sortingType = type;
        }

        public void CreatePylons()
        {
            if(pylons != null)
            {
                Debug.LogWarning("Pylons already created!");
                return;
            }

            StartCoroutine(ICreatePylons());
        }

        public void ClearPylons()
        {
            if(pylons == null)
            {
                return;
            }
            
            StartCoroutine(IClearPylons());        
        }

        public void StartSort()
        {
            if(pylons == null)
            {
                return;
            }

            SelectSort(sortingType);
        }

        private void SelectSort(SORTING_TYPE type)
        {
            switch(type)
            {
                case SORTING_TYPE.BUBBLE:
                    StartCoroutine(IBubbleSort());
                    break;
                case SORTING_TYPE.SELECTED:
                    StartCoroutine(ISelectedSort());
                    break;
                case SORTING_TYPE.INSERTION:
                    StartCoroutine(IInsertionSort());
                    break;
                //case SORTING_TYPE.MERGE:
                //    StartCoroutine(IMergeSort());
                //    break;
                //case SORTING_TYPE.QUICK:
                //    StartCoroutine(IQuickSort());
                //    break;
                //case SORTING_TYPE.RADIX:
                //    StartCoroutine(IRadixSort());
                //    break;
                //case SORTING_TYPE.COUNTING:
                //    StartCoroutine(ICountingSort());
                //    break;
                //case SORTING_TYPE.BUCKET:
                //    StartCoroutine(IBucketSort());
                //    break;
                //case SORTING_TYPE.SHELL:
                //    StartCoroutine(IShellSort());
                //    break;
                default:
                    break;
            }
        }

        private void PrintArray<T>(T[] array)
        {
            foreach(var item in array)
            {
                print(item);
            }
        }

        private IEnumerator ICreatePylons()
        {
            pylons = new Pylon[MAX_PYLON_COUNT];

            var startingPositition = InputManager.Instance.GetWorldPointFromScreenPoint(Vector2.zero);

            var randomValue = 0;
            var heightScale = 0f;

            for(int i = 0; i < pylons.Length; i++)
            {
                randomValue = Random.Range(PylonMinValue, PylonMaxValue);
                heightScale = randomValue;

                startingPositition.x += 1.25f;               

                var newPylonVisual = Instantiate(PylonVisualPrefab, startingPositition, Quaternion.identity, transform);

                LeanTween.scaleY(newPylonVisual.gameObject, heightScale * 0.5f, 0.5f);
                LeanTween.color(newPylonVisual.gameObject, Color.grey, 0.5f);

                pylons[i] = new Pylon(randomValue, newPylonVisual);

                newPylonVisual.gameObject.name = $"Pylon {pylons[i].Value}";
            }

            yield return null;
        }

        private IEnumerator IClearPylons()
        {
            var dropHeight = 0f;

            PylonVisual pylonVisual = null;

            for(int i = 0; i < pylons.Length; i++)
            {
                pylonVisual = pylons[i].PylonVisual;
                dropHeight = pylonVisual.transform.localScale.y;

                LeanTween.scaleY(pylonVisual.gameObject, -2 * pylonVisual.transform.localScale.y, 1f);
                LeanTween.color(pylonVisual.gameObject, Color.clear, 0.5f);

                Destroy(pylons[i].PylonVisual.gameObject, 1f);
            }

            pylons = null;

            yield return null;
        }

        private IEnumerator IBubbleSort()
        {          
            var i = 0;
            var j = 0;
            Pylon tempPylon = null;
            var tempPosition = Vector2.zero;

            var lastIndex = 1;

            var waitForAnimation = 0.1f;
            var waitUntil = new WaitUntil(() => InputManager.Instance.IsMouseLeftPressed);

            yield return waitUntil;

            for(i = 0; i < pylons.Length - 1; i++)
            {
                for(j = 0; j < pylons.Length - 1 - i; j++)
                {           
                    pylons[j].PylonVisual.AnimateColor(Black, waitForAnimation);

                    if(fullControl)
                    {
                        yield return waitUntil;
                    }

                    yield return new WaitForSeconds(waitForAnimation);

                    pylons[j + 1].PylonVisual.AnimateColor(Black, waitForAnimation);

                    if(fullControl)
                    {
                        yield return waitUntil;
                    }

                    yield return new WaitForSeconds(waitForAnimation);

                    if(pylons[j].Value > pylons[j + 1].Value)
                    {         
                        tempPylon = pylons[j];
                        pylons[j] = pylons[j + 1];
                        pylons[j + 1] = tempPylon;
#if UNITY_EDITOR
                        pylons[j].PylonVisual.transform.SetSiblingIndex(j);
                        pylons[j + 1].PylonVisual.transform.SetSiblingIndex(j + 1);
#endif
                        tempPosition = pylons[j].PylonVisual.transform.localPosition;

                        if(fullControl)
                        {
                            yield return waitUntil;
                        }

                        yield return new WaitForSeconds(waitForAnimation);

                        SwapVisualPositions(j, j + 1, tempPosition, waitForAnimation);

                        if(fullControl)
                        {
                            yield return waitUntil;
                        }

                        yield return new WaitForSeconds(waitForAnimation);

                        pylons[j].PylonVisual.AnimateColor(Grey, waitForAnimation);

                        if(fullControl)
                        {
                            yield return waitUntil;
                        }

                        yield return new WaitForSeconds(waitForAnimation);
                    }
                    else
                    {
                        pylons[j].PylonVisual.AnimateColor(Red, waitForAnimation);

                        if(fullControl)
                        {
                            yield return waitUntil;
                        }

                        yield return new WaitForSeconds(waitForAnimation);

                        pylons[j].PylonVisual.AnimateColor(Grey, waitForAnimation);

                        if(fullControl)
                        {
                            yield return waitUntil;
                        }

                        yield return new WaitForSeconds(waitForAnimation);
                    }

                    if(pylons[j + 1] == pylons[pylons.Length - lastIndex])
                    {
                        pylons[j + 1].PylonVisual.AnimateColor(Green, waitForAnimation);
                        lastIndex++;

                        if(fullControl)
                        {
                            yield return waitUntil;
                        }

                        yield return new WaitForSeconds(waitForAnimation);
                    }                 
                }
            }

            pylons[0].PylonVisual.AnimateColor(Green); 
        }

        private IEnumerator ISelectedSort()
        {
            var i = 0;
            var j = 0;
            var minValue = 0;
            var minIndex = 0;
            var tempPosition = Vector2.zero;
            Pylon tempPylon = null;

            // PylonVisual targetPylonVisual = null;
            PylonVisual minPylonVisual = null;

            var waitForAnimation = 0.1f;
            var waitUntil = new WaitUntil(() => InputManager.Instance.IsMouseLeftPressed);

            // targetPylonVisual = pylons[0].PylonVisual;
            minPylonVisual = pylons[0].PylonVisual;

            for(i = 0; i < pylons.Length; i++)
            {
                minValue = pylons[i].Value;
                minIndex = i;

                for(j = i; j < pylons.Length; j++)
                {
                    pylons[i].PylonVisual.AnimateColor(Black);

                    if(fullControl)
                    {
                        yield return waitUntil;
                    }

                    yield return new WaitForSeconds(waitForAnimation);

                    if(pylons[j].Value < minValue)
                    {
                        minValue = pylons[j].Value;
                        minIndex = j;

                        minPylonVisual = pylons[j].PylonVisual;
                    }                         
                }

                if(minValue < pylons[i].Value)
                {
                    tempPylon = pylons[i];
                    pylons[i] = pylons[minIndex];
                    pylons[minIndex] = tempPylon;

                    tempPosition = pylons[i].PylonVisual.transform.localPosition;

                    if(fullControl)
                    {
                        yield return waitUntil;
                    }

                    yield return new WaitForSeconds(waitForAnimation);

                    SwapVisualPositions(i, minIndex, tempPosition, waitForAnimation);

#if UNITY_EDITOR
                    pylons[i].PylonVisual.transform.SetSiblingIndex(i);
                    pylons[minIndex].PylonVisual.transform.SetSiblingIndex(minIndex);
#endif
                    if(fullControl)
                    {
                        yield return waitUntil;
                    }

                    yield return new WaitForSeconds(waitForAnimation);
                }

                LeanTween.color(pylons[i].PylonVisual.gameObject, Green, waitForAnimation);
            }
        }

        private void SwapVisualPositions(int index_A, int index_B, Vector2 tempPosition, float waitForAnimation)
        {
            LeanTween.moveLocalX(
                pylons[index_A].PylonVisual.gameObject,
                pylons[index_B].PylonVisual.transform.localPosition.x,
                waitForAnimation);

            LeanTween.moveLocalX(
                pylons[index_B].PylonVisual.gameObject,
                tempPosition.x,
                waitForAnimation);
        }

        private IEnumerator IInsertionSort()
        {
            var i = 0;
            var j = 0;
            var key = 0;
            Pylon tempPylon;

            var tempPosition = Vector2.zero;

            var animationDuration = 0.1f;
            var waitUntil = new WaitUntil(() => InputManager.Instance.IsMouseLeftPressed);

            for(i = 1; i < pylons.Length; i++)
            {
                key = pylons[i].Value;
                j = i - 1;
                
                while(j >= 0 && key < pylons[j].Value)
                {              
                    tempPylon = pylons[j];
                    pylons[j] = pylons[j + 1];
                    pylons[j + 1] = tempPylon;

                    tempPosition = pylons[j].PylonVisual.transform.localPosition;

                    if(fullControl)
                    {
                        yield return waitUntil;
                    }

                    yield return new WaitForSeconds(animationDuration);

                    SwapVisualPositions(j, j + 1, tempPosition, animationDuration);

#if UNITY_EDITOR
                    pylons[j].PylonVisual.transform.SetSiblingIndex(j);
                    pylons[j + 1].PylonVisual.transform.SetSiblingIndex(j + 1);
#endif

                    j--;

                    if(fullControl)
                    {
                        yield return waitUntil;
                    }

                    yield return new WaitForSeconds(animationDuration);
                }     
            }

            Debug.Log("SORTING DONE!");
        }

        private IEnumerator IMergeSort()
        {
            //var array = new int[] { 5, 2, 6, 7, 7, 8, 10, 9, 59, 100 };

            //var lowIndex = 0;
            //var highIndex = 0;
            //var midIndex = 0;

            //if(lowIndex == highIndex)
            //{
            //    yield break;
            //}
            //else
            //{
            //    midIndex = (lowIndex + highIndex) / 2;

            //    IMergeSort(array, lowIndex, midIndex);
            //    IMergeSort(array, lowIndex, midIndex);
            //    Merge(array, lowIndex, midIndex + 1, highIndex);
            //}

            yield return null;
        }

        private IEnumerator IQuickSort()
        {

            yield return null;
        }

        private IEnumerator IRadixSort()
        {

            yield return null;
        }

        private IEnumerator ICountingSort()
        {

            yield return null;

        }

        private IEnumerator IBucketSort()
        {

            yield return null;

        }

        private IEnumerator IShellSort()
        {

            yield return null;

        }

        #endregion CUSTOM_FUNCTIONS
    }
}
