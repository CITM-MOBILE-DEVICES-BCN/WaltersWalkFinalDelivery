using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalterWalk
{
    public class PlayerPowerUps : MonoBehaviour
    {
        private Animator animator;
        private GameDataManager gameDataManager;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();

            gameDataManager = new GameDataManager();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                UseCigarette();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                UseLSD();
            }
        }

        public void UseCigarette()
        {
            if (IsPowerUpAvailable("Cigarette"))
            {
                AudioManager.PlaySound(SoundType.SMOKING);
                SetPowerUpState("Cigarette", false);
                UnityEngine.Debug.Log("Cigarette used");
                animator.SetTrigger("SmokeCig");
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
                SetPowerUpState("LSD", false);
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
                SetPowerUpState("Air Plane Mode", false);
            }
            else
            {
                UnityEngine.Debug.Log("You don't have air plane mode on");
            }
        }

        private bool IsPowerUpAvailable(string itemName)
        {
            var item = gameDataManager.playerData.itemsOwned.Find(i => i.itemName == itemName);
            return item != null && item.isOwned;
        }

        private void SetPowerUpState(string itemName, bool state)
        {
            var item = gameDataManager.playerData.itemsOwned.Find(i => i.itemName == itemName);
            if (item != null)
            {
                item.isOwned = state;
                gameDataManager.SavePlayerData();
            }
        }
    }
}
