using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhoneMinigames
{
    public class PhoneCallAudio : MonoBehaviour
    {
        public AudioSource audioSource; 
        public AudioClip acceptCallClip; 
        public AudioClip declineCallClip;

        private void Start()
        {
            
        }

        public void OnAcceptCall()
        {
            PlayAcceptCallAudio();
        }

        public void OnDeclineCall()
        {
            PlayDeclineCallAudio();
        }

        private void PlayAcceptCallAudio()
        {


            audioSource.loop = false;
            audioSource.clip = acceptCallClip;
            audioSource.Play();

        }

        private void PlayDeclineCallAudio()
        {
            audioSource.loop = false;
            audioSource.clip = declineCallClip;
            audioSource.Play();

        }
    }
}