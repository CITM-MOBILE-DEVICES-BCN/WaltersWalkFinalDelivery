using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalterWalk
{
    public class ScoreMoneyManager : MonoBehaviour
    {
        public static ScoreMoneyManager instance;
        public int score;
        public int money;

        private int moneyCheckpoint;

        public bool isAddingPoints = false;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddPoints(int points)
        {
            score += points;
        }
        
        public void AddMoney(int bills)
        {
            money += bills;
        }
    }
}
