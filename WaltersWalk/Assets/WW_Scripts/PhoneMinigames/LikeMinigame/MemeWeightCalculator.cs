using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhoneMinigames
{
    public class MemeWeightCalculator : MonoBehaviour
    {
        public GameObject[] gameObjects;
        private List<GameObject> previouslySelectedObjects = new List<GameObject>();
        private GameObject lastSelectedObject = null; 

        private Dictionary<GameObject, float> objectWeights = new Dictionary<GameObject, float>();

        void Start()
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                objectWeights[gameObjects[i]] = 1f;
            }
        }

        public GameObject SelectGameObject()
        {
           
            AdjustObjectWeights();

            GameObject selectedObject = GetRandomObject();
          
            previouslySelectedObjects.Add(selectedObject);

            lastSelectedObject = selectedObject;

            return selectedObject;
        }

        private void AdjustObjectWeights()
        {
            // Decrease the weight for the objects that have been selected more frequently
            List<GameObject> keys = new List<GameObject>(objectWeights.Keys); // Convert dictionary keys to list for indexed access
            for (int i = 0; i < keys.Count; i++)
            {
                GameObject obj = keys[i];
                if (previouslySelectedObjects.Contains(obj))
                {
                    objectWeights[obj] = Mathf.Max(0.5f, objectWeights[obj] - 0.1f); // Decrease weight for selected objects
                }
                else
                {
                    objectWeights[obj] = Mathf.Min(2f, objectWeights[obj] + 0.05f); // Increase weight for unselected objects
                }
            }
        }

        private GameObject GetRandomObject()
        {
            // List of objects based on their adjusted weights
            List<GameObject> weightedObjects = new List<GameObject>();

            List<GameObject> keys = new List<GameObject>(objectWeights.Keys); // Convert dictionary keys to list
            for (int i = 0; i < keys.Count; i++)
            {
                GameObject obj = keys[i];
                int weight = Mathf.CeilToInt(objectWeights[obj]); // Convert weight to an integer for counting times
                for (int j = 0; j < weight; j++)
                {
                    weightedObjects.Add(obj);
                }
            }

            // Make sure we don't pick the last selected GameObject
            GameObject selectedObject = null;
            while (selectedObject == lastSelectedObject)
            {
                selectedObject = weightedObjects[Random.Range(0, weightedObjects.Count)];
            }

            return selectedObject;
        }
    }
}
