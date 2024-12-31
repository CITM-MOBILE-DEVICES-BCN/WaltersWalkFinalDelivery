using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalterWalk
{
    public class DestroyableMapTile : MonoBehaviour
    {
        void Start()
        {
            GameManager.Instance.mapCreator?.AddDestroyableTile(this);
            //transform.parent = transform.parent.parent.parent;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "building")
            {
                Destroy(other.gameObject);
            }
        }

    }
}
