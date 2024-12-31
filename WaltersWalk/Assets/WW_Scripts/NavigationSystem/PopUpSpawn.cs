using System.Collections;
using UnityEngine;
using DG.Tweening; 

namespace WalterWalk
{
    public class PopUpSpawn : MonoBehaviour
    {
        [SerializeField] float scaleUpTime = 0.2f;
        [SerializeField] float scaleDownTime = 0.15f;
        [SerializeField] public float dissapearTime = 0.1f;
        [SerializeField] Vector3 initialScale = Vector3.zero;
        [SerializeField] Vector3 maxScale = new Vector3(1.2f, 1.2f, 1.2f);
        [SerializeField] Vector3 finalScale = new Vector3(1f, 1f, 1f);

        public bool isDissapeared = false;

        private void Start()
        {
            SpawnAnim();
        }

        public void SpawnAnim()
        {
            transform.localScale = initialScale; 
            Sequence spawnSequence = DOTween.Sequence();

            spawnSequence
                .Append(transform.DOScale(maxScale, scaleUpTime).SetEase(Ease.OutQuad))
                .Append(transform.DOScale(finalScale, scaleDownTime).SetEase(Ease.InQuad));
        }

        public void DespawnPopUp()
        {
            DespawnAnim();
        }

        private void DespawnAnim()
        {
            transform.DOScale(Vector3.zero, dissapearTime)
                .SetEase(Ease.InQuad)
                .OnComplete(() => isDissapeared = true);
        }
    }
}
