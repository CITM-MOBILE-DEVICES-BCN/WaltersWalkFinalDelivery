using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace PhoneMinigames
{
    public class ScrollScoreManager
    {
       public int score = 0;
        
       public void OnScore(int points)
       {
            score += points;
       }
    }
}
