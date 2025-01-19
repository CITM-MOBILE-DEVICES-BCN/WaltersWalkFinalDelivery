using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace PhoneMinigames
{
    public class SubwaySurferObstacleLogic : MonoBehaviour
    {
        public float speed = 10;
        void Start()
        {
            transform.DOMoveZ(-10, speed).SetEase(Ease.Linear).OnComplete(() => Destroy(gameObject));
        }

    }
}
