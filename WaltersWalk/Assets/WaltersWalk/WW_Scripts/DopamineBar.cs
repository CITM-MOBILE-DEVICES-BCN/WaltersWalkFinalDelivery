using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WalterWalk
{
    public class DopamineBar : MonoBehaviour
    {
        public float dopamineValue = 100;
        public float dopamineDecraseRate = 1;
        public Slider dopamineSlider;

        //create a singleton
        public static DopamineBar instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
        public void DopaMineLevelValueModification(float value)
        {
            dopamineValue += value;
        }

        void Update()
        {
            dopamineValue -= Time.deltaTime * dopamineDecraseRate;
            dopamineSlider.value = dopamineValue;
        }
    }
}
