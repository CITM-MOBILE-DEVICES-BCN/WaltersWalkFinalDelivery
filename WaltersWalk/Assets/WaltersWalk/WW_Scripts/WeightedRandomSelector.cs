using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace WalterWalk
{
    public class WeightedRandomSelector
    {
        private int arraySize;
        private List<int> previousResults;
        private int lastResult = -1;
        private float[] weights; // Pre-calculated weights array

        public WeightedRandomSelector(int size)
        {
            if (size <= 0)
            {
                throw new System.ArgumentException("Array size must be positive.");
            }

            arraySize = size;
            previousResults = new List<int>();
            weights = new float[size]; // Initialize weights array
            for (int i = 0; i < size; i++)
            {
                weights[i] = 1f; // Initialize all weights to 1
            }
        }

        public int GetNextWeightedRandom()
        {
            if (arraySize == 1) return 0;

            // Reset weights to 1 before recalculating
            for (int i = 0; i < arraySize; i++)
            {
                weights[i] = 1f;
            }

            // Apply penalties for previous results
            for (int i = 0; i < previousResults.Count; i++)
            {
                weights[previousResults[i]] *= Mathf.Pow(0.5f, previousResults.Count - i);
            }

            //Remove the last result to avoid repetition
            if (lastResult != -1)
            {
                weights[lastResult] = 0;
            }

            // Calculate total weight and handle edge case of all weights being zero
            float totalWeight = 0f;
            for (int i = 0; i < arraySize; i++)
            {
                totalWeight += weights[i];
            }

            if (totalWeight <= 0f)
            {
                previousResults.Clear();
                for (int i = 0; i < arraySize; i++)
                {
                    weights[i] = 1f;
                }
                totalWeight = arraySize; // Total weight is now just the array size
            }

            // Select random index based on cumulative probabilities
            float randomValue = Random.value * totalWeight; 
            float cumulativeWeight = 0f;

            for (int i = 0; i < arraySize; i++)
            {
                cumulativeWeight += weights[i];
                if (randomValue < cumulativeWeight)
                {
                    previousResults.Add(i);
                    lastResult = i;
                    return i;
                }
            }

            // This should never happen, but is included for safety.
            Debug.LogError("Error in weighted random selection. Returning 0 as fallback.");
            previousResults.Add(0);
            lastResult = 0;
            return 0;
        }

        public void ClearPreviousResults()
        {
            previousResults.Clear();
            lastResult = -1;
        }
    }
}
