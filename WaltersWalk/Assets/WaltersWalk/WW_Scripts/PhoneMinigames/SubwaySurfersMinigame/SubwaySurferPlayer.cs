using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WalterWalk;

namespace PhoneMinigames
{
    public class SubwaySurferPlayer : MonoBehaviour
    {

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("collision");
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("COllision with objactle");
                DopamineBar.instance.DopaMineLevelValueModification(-30);
            }
        }
    }
}
