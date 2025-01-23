using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace WalterWalk
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] Animator playerDeathAnimator;
        [SerializeField] GameObject blackScreen;
        public void DeathByCar()
        {
            blackScreen.SetActive(true);
            //play sound
            StartCoroutine(HospitalTransition());
        }  
        public void DeathByDopamine()
        {
            StartCoroutine(BlackScreen());
        }

        IEnumerator BlackScreen()
        {
            yield return new WaitForSeconds(2f);
            blackScreen.SetActive(true);
            StartCoroutine(HospitalTransition());
        }

        IEnumerator HospitalTransition()
        {
            yield return new WaitForSeconds(2);
            //load hospital scene
            SceneManager.LoadScene("Hospital");
        }

    }
}
