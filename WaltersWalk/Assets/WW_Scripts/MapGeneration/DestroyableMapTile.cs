using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Codice.Client.Common.EventTracking.TrackFeatureUseEvent.Features.DesktopGUI.Filters;

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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "building")
            {
                Destroy(collision.gameObject);
            }
        }

    }
}
