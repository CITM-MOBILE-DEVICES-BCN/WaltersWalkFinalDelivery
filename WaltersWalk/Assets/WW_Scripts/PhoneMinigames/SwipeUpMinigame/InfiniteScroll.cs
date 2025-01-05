using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PhoneMinigames
{
    public class InfiniteScroll : MonoBehaviour
    {
        public ScrollRect scrollRect;
        public RectTransform viewPortTransform;
        public RectTransform contentTransform;
        public VerticalLayoutGroup verticalLayoutGroup;

        public RectTransform[] items;

        Vector2 oldVelocity;
        bool isUpdated;
        void Start()
        {
            isUpdated = false;
            oldVelocity = Vector2.zero;
            int itemsToAdd = Mathf.CeilToInt(viewPortTransform.rect.height / (items[0].rect.height + verticalLayoutGroup.spacing));

            for (int i = 0; i < itemsToAdd; i++) 
            {
                RectTransform newItem = Instantiate(items[Random.Range(0, items.Length)], contentTransform);
                newItem.SetAsLastSibling();
            }

            for (int i = 0; i < itemsToAdd; i++)
            {
                int num = items.Length; 
                RectTransform newItem = Instantiate(items[Random.Range(0, items.Length)], contentTransform);
                newItem.SetAsFirstSibling();
            }

            contentTransform.localPosition = new Vector3(contentTransform.localPosition.x, 0 - (items[0].rect.height + verticalLayoutGroup.spacing) * itemsToAdd, contentTransform.localPosition.z);
        }

        // Update is called once per frame
        void Update()
        {
            if (isUpdated)
            {
                scrollRect.velocity = oldVelocity;
                isUpdated = false;
            }

            if (contentTransform.localPosition.y > 0)
            {
                Canvas.ForceUpdateCanvases();
                oldVelocity = scrollRect.velocity;
                contentTransform.localPosition += new Vector3(0,items.Length * (items[0].rect.height + verticalLayoutGroup.spacing), 0);
                isUpdated = true;
            }

            if (contentTransform.localPosition.y < 0 - (items.Length * (items[0].rect.height+verticalLayoutGroup.spacing))) 
            {
                Canvas.ForceUpdateCanvases();
                oldVelocity = scrollRect.velocity;
                contentTransform.localPosition += new Vector3(0, items.Length * (items[0].rect.height + verticalLayoutGroup.spacing), 0);
                isUpdated = true;
            }
        }
    }
}

