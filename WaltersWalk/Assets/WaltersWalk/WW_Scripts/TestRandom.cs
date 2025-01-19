using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalterWalk
{
    public class TestRandom : MonoBehaviour
    {
        public WeightedRandomSelector selector; 

        public int[] numbers = { 1, 2, 3, 4, 5 }; // Array of integers

        void Start()
        {
            selector = new WeightedRandomSelector(numbers.Length); // Initialize the RandomSelector
           
            // Select a random number
            int selectedIndex = selector.GetNextWeightedRandom();
            Debug.Log("Selected Number: " + numbers[selectedIndex]);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int selectedIndex = selector.GetNextWeightedRandom();
                Debug.Log("Selected Number: " + numbers[selectedIndex]);
            }
          
        }
    }
}
