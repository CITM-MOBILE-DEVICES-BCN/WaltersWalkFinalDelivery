using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace WalterWalk
{
    public class PlayerCamera : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {

        public Camera camera;
        public float swipeThreshold = 50f; 
        private Vector2 startDragPosition;

        float maxRotation;
        float minRotation;

        private void Awake()
        {
            camera = Camera.main;
            maxRotation = camera.transform.eulerAngles.y + 60;
            minRotation = camera.transform.eulerAngles.y - 60;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {

            startDragPosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 endDragPosition = eventData.position;


            Vector2 swipeDelta = endDragPosition - startDragPosition;

            // Check if the swipe distance is significant
            if (Mathf.Abs(swipeDelta.x) > swipeThreshold && Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                if (swipeDelta.x > 0)
                {
                    float rotation = ClampRotation(camera.transform.eulerAngles.y + (swipeDelta.x * .1f));
                    camera.transform.DORotate(new Vector3(camera.transform.eulerAngles.x, rotation , camera.transform.eulerAngles.z), 0.1f);

                    Debug.Log("Right Swipe Detected");
                }
                else
                {
                    float rotation = ClampRotation(camera.transform.eulerAngles.y + (swipeDelta.x * .1f));
                    camera.transform.DORotate(new Vector3(camera.transform.eulerAngles.x, rotation, camera.transform.eulerAngles.z), 0.1f);
                    Debug.Log("Left Swipe Detected");
                }
            }
        }

        private float ClampRotation(float rotation)
        {
            rotation = Mathf.Clamp(rotation, minRotation, maxRotation);
            return rotation;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //Vector2 endDragPosition = eventData.position;


            //Vector2 swipeDelta = endDragPosition - startDragPosition;

            //// Check if the swipe distance is significant
            //if (Mathf.Abs(swipeDelta.x) > swipeThreshold && Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            //{
            //    if (swipeDelta.x > 0)
            //    {

            //        camera.transform.DORotate(new Vector3(camera.transform.eulerAngles.x, camera.transform.eulerAngles.y + swipeDelta.x, camera.transform.eulerAngles.z), );

            //        Debug.Log("Right Swipe Detected");
            //    }
            //    else
            //    {
            //        Debug.Log("Left Swipe Detected");
            //    }
            //}
        }
    }
}
