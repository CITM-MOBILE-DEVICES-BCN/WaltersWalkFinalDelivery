using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WalterWalk
{
    public class PortFolioText : MonoBehaviour
    {
        public TextMeshProUGUI moneyText;
        public TextMeshProUGUI scoreText;
        void Start()
        {
            scoreText.text = "Score:" + ScoreMoneyManager.instance.money.ToString();
            moneyText.text = "Money:" + ScoreMoneyManager.instance.score.ToString();
        }

    }
}
