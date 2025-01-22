using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WalterWalk;

namespace PhoneMinigames
{
    public class SubwaySurferPlayer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            {
                DopamineBar.instance.DopaMineLevelValueModification(-10);
            }
        }
    }
}
