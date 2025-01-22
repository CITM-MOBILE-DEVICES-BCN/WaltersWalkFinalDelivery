using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using WalterWalk;

namespace PhoneMinigames
{
    public class MemeLogic : MonoBehaviour
    {
        public Button likeButton;
        public Button dislikeButton;
        public MemeSelector memeSelector;

        public Image memeImage;

        public bool isGoodMeme;
        void Start()
        {
            likeButton.onClick.AddListener(() => MemeEvaulation(true));
            dislikeButton.onClick.AddListener(() => MemeEvaulation(false));
        }

        void MemeEvaulation(bool hasLiked)
        {
            if (isGoodMeme == hasLiked)
            {
                //logic for adding score, dopamine, etc
                Debug.Log("Corect");
                memeSelector.GetRandomMeme();
                memeImage.color = Color.green;
                DopamineBar.instance.DopaMineLevelValueModification(10);
                gameObject.transform.DOLocalMove(new Vector3(100, 0, 0), 0.5f).OnComplete(() => Destroy(gameObject)); 
            }
            else
            {
                Debug.Log("Incorrect");
                memeSelector.GetRandomMeme();
                memeImage.color = Color.red;
                DopamineBar.instance.DopaMineLevelValueModification(-10);
                gameObject.transform.DOLocalMove(new Vector3(100, 0, 0), 0.5f).OnComplete(() => Destroy(gameObject));
            }
        }
       
    }
}
