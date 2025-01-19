using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PhoneMinigames
{
    public class ScrollScoreUI : MonoBehaviour
    {
        public InfinityScroll infinityScroll;
        public TextMeshProUGUI scoreText;

        void Update()
        {
            scoreText.text = infinityScroll.scrollScoreManager.score.ToString();
        }
    }
}
