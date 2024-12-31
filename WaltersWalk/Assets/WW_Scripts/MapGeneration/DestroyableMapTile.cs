using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalterWalk
{
    public class DestroyableMapTile : MonoBehaviour
    {
        void Start()
        {
            WalkerCreator.Instance.AddDestroyableTile(this);
        }

    }
}
