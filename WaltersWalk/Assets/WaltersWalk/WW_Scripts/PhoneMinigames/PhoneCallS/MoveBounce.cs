using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhoneMinigames
{
    public class MoveBounce : MonoBehaviour
    {
        public float speed = 50f;
        public float minX, maxX, minY, maxY;

        private RectTransform rectTransform;
        private Vector2 direction;

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            direction = GetRandomDirection(); // Initial random direction
        }

        void Update()
        {
            // Move the object
            rectTransform.anchoredPosition += direction * speed * Time.deltaTime;

            // Check boundaries and bounce with a new random direction
            if (rectTransform.anchoredPosition.x < minX || rectTransform.anchoredPosition.x > maxX)
            {
                direction = GetRandomDirection(); // New direction on x bounce
                direction.x = -direction.x; // Ensure it bounces back
                rectTransform.anchoredPosition = new Vector2(
                    Mathf.Clamp(rectTransform.anchoredPosition.x, minX, maxX),
                    rectTransform.anchoredPosition.y
                );
            }
            if (rectTransform.anchoredPosition.y < minY || rectTransform.anchoredPosition.y > maxY)
            {
                direction = GetRandomDirection(); // New direction on y bounce
                direction.y = -direction.y; // Ensure it bounces back
                rectTransform.anchoredPosition = new Vector2(
                    rectTransform.anchoredPosition.x,
                    Mathf.Clamp(rectTransform.anchoredPosition.y, minY, maxY)
                );
            }
        }

        // Helper function to get a random direction
        private Vector2 GetRandomDirection()
        {
            return Random.insideUnitCircle.normalized;
        }
    }
}
