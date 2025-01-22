using Codice.Client.BaseCommands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace WalterWalk
{

    public enum Direction
    {
        LEFT,
        RIGHT
    }

    public class Car : MonoBehaviour
    {

        public Direction dir;
        public Vector3 direction;
        private float timeAlive = 6f;
        public float speed = 0f;
        public SoundType claxonType;

        private BoxCollider collider;
	    private GameObject carSpawned = null;
	    public bool goesDown = false;

        public bool hasPlayedClaxon = false;

        private AudioSource audioSource;


        private void Awake()
        {
            collider = GetComponent<BoxCollider>();
            audioSource = gameObject.AddComponent<AudioSource>(); 
            audioSource.spatialBlend = 1.0f;
        }
        public void Start()
        {
           

            GameObject[] cars = Resources.LoadAll<GameObject>("Cars");
            collider.enabled = true;
            if (carSpawned == null)
            {
                carSpawned = Instantiate(cars[UnityEngine.Random.Range(0, cars.Length - 1)], this.transform);
                carSpawned.transform.localPosition = new Vector3(0, -1, 0);
                collider.size = new Vector3(collider.bounds.size.z, collider.bounds.size.y, collider.bounds.size.x);
            }
	        if (!goesDown){
		        if (dir == Direction.LEFT)
		        {
			        direction = new Vector3(1, 0, 0);
			        this.transform.eulerAngles = new Vector3(0,90,0);
		        }
		        else
		        {
			        direction = new Vector3(-1, 0, 0);
                    this.transform.eulerAngles = new Vector3(0,-90,0);
		        }
	        }
	        else
	        {
	        	if (dir == Direction.LEFT)
		        {
			        direction = new Vector3(0, 0, -1);
                    this.transform.eulerAngles = new Vector3(0, 180,0);
		        }
		        else
		        {
			        direction = new Vector3(0, -0, 1);
                    this.transform.eulerAngles = new Vector3(0, 0,0);
		        }
	        }
	        
	        
	        float prevSpeed = speed;
	        
	        speed = ( UnityEngine.Random.Range(speed- 3f, speed+3f) + UnityEngine.Random.Range(speed - 3f, speed + 3f) )* .5f;
	        
        }

        // Update is called once per frame
        void Update()
        {
           

            if(dir == Direction.LEFT)
            {
                if (transform.position.x > PlayerManager.instance.player.transform.position.x)
                {
                    collider.enabled = false;
                    
                   PlayerManager.instance.warner.RemoveWarning(this.gameObject);
                }
            }
            else
            {
                if (transform.position.x < PlayerManager.instance.player.transform.position.x)
                {
                    collider.enabled = false;
                   PlayerManager.instance.warner.RemoveWarning(this.gameObject);
                }
            }
           // return;
            transform.position += direction * Time.deltaTime * speed;
        }

        private void OnEnable()
        {
            Invoke("Deactivate", timeAlive);
            Start();
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            PlayerManager.instance.warner.RemoveWarning(this.gameObject);
        }


        public void PlayClaxon()
        {
            AudioClip clip = AudioManager.instance.GetAudioClip(claxonType);
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
                UnityEngine.Debug.Log("Sonando claxon: " + claxonType.ToString());
            }
        }
    }
}
