using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Codice.Client.Common.EventTracking.TrackFeatureUseEvent.Features.DesktopGUI.Filters;

namespace WalterWalk
{
    public class PlayerWarner : MonoBehaviour
    {

        List<GameObject> worrysomeCarsLeft = new List<GameObject>();
        List<GameObject> worrysomeCarsRight = new List<GameObject>();

        public Image leftWarning;
        public Image rightWarning;
        Color invisible;

        // Start is called before the first frame update
        void Start()
        {
            invisible = new Color(1, 1, 1, 0);
            PlayerManager.instance.warner = this;
        }

        // Update is called once per frame
        void Update()
        {
            if (worrysomeCarsRight.Count > 0) CalculateCarProximity(rightWarning, worrysomeCarsRight);
            else { rightWarning.color = invisible; }
            if (worrysomeCarsLeft.Count > 0) CalculateCarProximity(leftWarning, worrysomeCarsLeft);
            else { leftWarning.color = invisible; }

        }

        void CalculateCarProximity(Image img, List<GameObject> worrysomeCars)
        {
            float warningLvl = 0f;

            float minimum = 1000f;
            for (int i = 0; i < worrysomeCars.Count; ++i)
            {
                var distance = Vector3.Distance(transform.position, worrysomeCars[i].transform.position);
                if (distance < minimum)
                {
                    minimum = distance;
                }
            }

            warningLvl = 5f / (minimum);
            print("warining level " + warningLvl);
            img.color = new Color(img.color.r, img.color.g, img.color.b, warningLvl);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "car")
            {
	            if(PlayerManager.instance.playerOrientation == Orientation.Vertical)
	            {

	            	if (other.transform.position.x > transform.position.x)
	            	{
	            		
	            		worrysomeCarsLeft.Add(other.gameObject);
	            	}
	            	else
	            	{
	            		worrysomeCarsRight.Add(other.gameObject);
	            	}
	            }
                else
                {
                    if (other.transform.position.z > transform.position.z)
                    {

                        worrysomeCarsLeft.Add(other.gameObject);
                    }
                    else
                    {
                        worrysomeCarsRight.Add(other.gameObject);
                    }
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "car")
            {

            }
        }

        private void OnTriggerExit(Collider other)
        {

            if (other.tag == "car")
            {
                if (worrysomeCarsLeft.Contains(other.gameObject))
                {
                    worrysomeCarsLeft.Remove(other.gameObject);
                }
                if (worrysomeCarsRight.Contains(other.gameObject))
                {
                    worrysomeCarsRight.Remove(other.gameObject);
                }
            }
        }

        public void RemoveWarning(GameObject carObj)
        {
            if (worrysomeCarsLeft.Contains(carObj.gameObject))
            {
                worrysomeCarsLeft.Remove(carObj.gameObject);
            }
            if (worrysomeCarsRight.Contains(carObj.gameObject))
            {
                worrysomeCarsRight.Remove(carObj.gameObject);
            }
        }
    }
}
