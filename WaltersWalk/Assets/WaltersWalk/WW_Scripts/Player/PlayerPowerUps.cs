using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using PhoneMinigames;

namespace WalterWalk
{
    public class PlayerPowerUps : MonoBehaviour
	{
		
        private Animator animator;
        private GameDataManager gameDataManager;
        private DopamineBar dopamineBar;

        public static PlayerPowerUps instance;
		public PhoneMinigames phoneCallSpawner;

        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

            animator = GetComponent<Animator>();
            gameDataManager = new GameDataManager();
            dopamineBar = DopamineBar.instance;

            StartCoroutine(UsePowerUpsInOrder());


        }

        // Update is called once per frame
        void Update()
        {


        }

        public void UseCigarette()
        {
            if (IsPowerUpAvailable("Cigarette"))
            {
                AudioManager.PlaySound(SoundType.SMOKING);
                animator.SetTrigger("SmokeCig");
                dopamineBar.dopamineDecraseRate = 0.75f;
                SetPowerUpState("Cigarette", false);
                UnityEngine.Debug.Log("Cigarette used");

            }
            else
            {
                UnityEngine.Debug.Log("You don't have a cigarette");
            }
        }

        public void UseLSD()
        {
            if (IsPowerUpAvailable("LSD"))
            {
                AudioManager.PlaySound(SoundType.PILLS);
                animator.SetTrigger("Pills");
                UnityEngine.Debug.Log("LSD used");
            }
            else
            {
                UnityEngine.Debug.Log("You don't have LSD");
            }
        }

        public void UseBubbleGum()
        {
            if (IsPowerUpAvailable("BubbleGum"))
            {
                SetPowerUpState("BubbleGum", false);
                UnityEngine.Debug.Log("BubbleGum used");
            }
            else
            {
                UnityEngine.Debug.Log("You don't have a bubblegum");
            }
        }

        public void UseSportShoes()
        {
            if (IsPowerUpAvailable("Sport Shoes"))
            {
                SetPowerUpState("Sport Shoes", false);
                UnityEngine.Debug.Log("SportShoes used");

            }
            else
            {
                UnityEngine.Debug.Log("You don't have Sport Shoes");
            }
        }

        public void UseAirPlaneMode()
        {
            if (IsPowerUpAvailable("Air Plane Mode"))
            {
                phoneCallSpawner.isAirPlaneModeActive = true;  
                SetPowerUpState("Air Plane Mode", false);
                UnityEngine.Debug.Log("AirPlaneMode used");
            }
            else
            {
                UnityEngine.Debug.Log("You don't have air plane mode on");
            }
        }

        public bool IsPowerUpAvailable(string itemName)
        {
            var item = gameDataManager.playerData.itemsOwned.Find(i => i.itemName == itemName);
            return item != null && item.isOwned;
        }

        public void SetPowerUpState(string itemName, bool state)
        {
            var item = gameDataManager.playerData.itemsOwned.Find(i => i.itemName == itemName);
            if (item != null)
            {
                item.isOwned = state;
                gameDataManager.SavePlayerData();
            }
        }
        private IEnumerator UsePowerUpsInOrder()
        {
            UseSportShoes();
            UseAirPlaneMode();
            UseBubbleGum();
            yield return new WaitForSeconds(3);
            UseCigarette();
            yield return new WaitForSeconds(3);
            UseLSD();
        }
    }
}
