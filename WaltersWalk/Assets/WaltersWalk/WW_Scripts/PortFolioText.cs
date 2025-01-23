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
        GameDataManager gameDataManager;
        void Start()
        {
            gameDataManager = new GameDataManager();
            scoreText.text = "Score:" + ScoreMoneyManager.instance.money.ToString();
            moneyText.text = "Money:" + ScoreMoneyManager.instance.score.ToString();

            gameDataManager.AddCurrency(ScoreMoneyManager.instance.money);
        }

    }
}
