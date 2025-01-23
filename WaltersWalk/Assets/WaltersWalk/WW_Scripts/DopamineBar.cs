using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using UnityEngine.UI;

namespace WalterWalk
{
    public class DopamineBar : MonoBehaviour
    {
        public float dopamineValue = 100;
        public float dopamineDecraseRate = 1;
        public Slider dopamineSlider;
        public PlayerDeath playerDeath;
        private Animator animator;
        private bool hasScratched = false;


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
            ScoreMoneyManager.instance.AddPoints((int)value);
        }

        void Update()
        {
            dopamineValue -= Time.deltaTime * dopamineDecraseRate;
            dopamineSlider.value = dopamineValue;

            if (dopamineValue >= 100)
            {
                dopamineValue = 100;
                ScoreMoneyManager.instance.AddMoney(1);
            }

            if (dopamineValue <= 0) 
            {
                if (PlayerPowerUps.instance.IsPowerUpAvailable("LSD"))
                {
                    dopamineValue = 100;
                    PlayerPowerUps.instance.SetPowerUpState("LSD", false);

                }
                else
                {
                    playerDeath.DeathByDopamine();

                }
            }
            if (dopamineValue <= 30 && !animator.GetBool("IsPhoneActive") && !hasScratched)
            {
                AudioManager.PlaySound(SoundType.SCRATCHING);
                animator.SetTrigger("Scratching");
                hasScratched = true;

            }
        }
    }
}
