using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalterWalk
{
    public class BuildingPlacer 
    {
        private Vector3 box;
        private Vector3 position;

        private Vector3 size_increment;

        bool isGrowing = true;

        public BuildingPlacer(Vector3 position)
        {
            this.position = position;
            size_increment = new Vector3(1, 1, 1);
        }
        public Vector3 CreateSize()
        {
            Collider[] colliders = Physics.OverlapBox(position, box);

            while(colliders.Length == 0)
            {
                box += size_increment;
                colliders = Physics.OverlapBox(position, box);
            }

            box -= size_increment;
            return box;
        }

    }
}

