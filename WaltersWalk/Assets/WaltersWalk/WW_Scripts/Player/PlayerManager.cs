using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalterWalk
{
    public class PlayerManager : MonoBehaviour
    {
	    public static PlayerManager instance;
        
	    public Orientation playerOrientation;
        public GameObject player;
        public PlayerWarner warner;
        private void Awake()
        {
            if (instance) { Destroy(this); }

            instance = this;
        }

    }
}
