using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace PhoneMinigames
{
    public class InfinityScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
    {
        #region Private Members

        [SerializeField]
        private ScrollContent scrollContent;

        [SerializeField]
        private float outOfBoundsThreshold;

        private ScrollRect scrollRect;
        private Vector2 lastDragPosition;
        private bool positiveDrag;

        #endregion

        #region Public Members

        public ScrollScoreManager scrollScoreManager;

        #endregion

        private void Start()
        {
            scrollScoreManager = new ScrollScoreManager();
            scrollRect = GetComponent<ScrollRect>();
            scrollRect.vertical = scrollContent.Vertical;
            scrollRect.horizontal = scrollContent.Horizontal;
            scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
            scrollRect.velocity = Vector2.zero;

        }

      
        public void OnBeginDrag(PointerEventData eventData)
        {
            lastDragPosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (scrollContent.Vertical)
            {
                positiveDrag = eventData.position.y > lastDragPosition.y;
            }
            else if (scrollContent.Horizontal)
            {
                positiveDrag = eventData.position.x > lastDragPosition.x;
            }

            lastDragPosition = eventData.position;
        }

        public void OnScroll(PointerEventData eventData)
        {
            if (scrollContent.Vertical)
            {
                positiveDrag = eventData.scrollDelta.y > 0;
            }
            else
            {
                positiveDrag = eventData.scrollDelta.y < 0;
            }
        }

        public void OnViewScroll()
        {
            if (scrollContent.Vertical)
            {
                HandleVerticalScroll();
            }
            else
            {
                HandleHorizontalScroll();
            }
        }

        private void HandleVerticalScroll()
        {
            int currItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
            var currItem = scrollRect.content.GetChild(currItemIndex);

            if (!ReachedThreshold(currItem))
            {
                return;
            }

            int endItemIndex = positiveDrag ? 0 : scrollRect.content.childCount - 1;
            Transform endItem = scrollRect.content.GetChild(endItemIndex);
            Vector2 newPos = endItem.localPosition;

            if (positiveDrag)
            {
                newPos.y = endItem.localPosition.y - scrollContent.ChildHeight + scrollContent.ItemSpacing;
                scrollScoreManager.OnScore(1);
            }
            else
            {
                newPos.y = endItem.localPosition.y + scrollContent.ChildHeight - scrollContent.ItemSpacing;
                scrollScoreManager.OnScore(-1);
            }

            currItem.localPosition = newPos;
            currItem.SetSiblingIndex(endItemIndex);
        }

        private void HandleHorizontalScroll()
        {
            int currItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
            var currItem = scrollRect.content.GetChild(currItemIndex);
            if (!ReachedThreshold(currItem))
            {
                return;
            }

            int endItemIndex = positiveDrag ? 0 : scrollRect.content.childCount - 1;
            Transform endItem = scrollRect.content.GetChild(endItemIndex);
            Vector2 newPos = endItem.localPosition;

            if (positiveDrag)
            {
                newPos.x = endItem.localPosition.x - scrollContent.ChildWidth * 1.5f + scrollContent.ItemSpacing;
            }
            else
            {
                newPos.x = endItem.localPosition.x + scrollContent.ChildWidth * 1.5f - scrollContent.ItemSpacing;
            }

            currItem.localPosition = newPos;
            currItem.SetSiblingIndex(endItemIndex);
        }

        private bool ReachedThreshold(Transform item)
        {
            Vector3 itemWorldPos = item.position;
            Vector3 canvasLocalPos = transform.InverseTransformPoint(itemWorldPos);

            if (scrollContent.Vertical)
            {
                float posYThreshold = transform.localPosition.y + scrollContent.Height * 0.5f + outOfBoundsThreshold;
                float negYThreshold = transform.localPosition.y - scrollContent.Height * 0.5f - outOfBoundsThreshold;

                return positiveDrag ? canvasLocalPos.y - scrollContent.ChildHeight * 0.5f > posYThreshold :
                    canvasLocalPos.y + scrollContent.ChildHeight * 0.5f < negYThreshold;
            }
            else
            {
                float posXThreshold = transform.localPosition.x + scrollContent.Width * 0.5f + outOfBoundsThreshold;
                float negXThreshold = transform.localPosition.x - scrollContent.Width * 0.5f - outOfBoundsThreshold;
                return positiveDrag ? canvasLocalPos.x - scrollContent.ChildWidth * 0.5f > posXThreshold :
                    canvasLocalPos.x + scrollContent.ChildWidth * 0.5f < negXThreshold;
            }
        }
    }
}