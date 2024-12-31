using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WalterWalk;


namespace PhoneMinigames
{
    public class MemeSelector : MonoBehaviour
    {
        public GameObject[] memesPrefab;
        
        public GameObject mask;

        WeightedRandomSelector memeWeightCalculator;
        MemeTransition memeTransition;

        
        GameObject newMeme;

        void Start()
        {
            memeWeightCalculator = new WeightedRandomSelector(memesPrefab.Length);
            memeTransition = new MemeTransition();
            GetRandomMeme();
        }

        public void GetRandomMeme()
        {
            int index = memeWeightCalculator.GetNextWeightedRandom();
            newMeme = Instantiate(memesPrefab[index],mask.transform);
            newMeme.transform.localPosition = new Vector3(-100,0,0);
            newMeme.GetComponent<MemeLogic>().memeSelector = this;
            newMeme.transform.DOMoveX(0, 0.5f);
        }
    }
}
