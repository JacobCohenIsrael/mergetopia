using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mergetopia
{
    public class Mergeable : MonoBehaviour
    {
        [SerializeField]
        private int mergeId;
    
        [SerializeField]
        private MergeablesConfig mergeablesConfig;

        [SerializeField] private Rigidbody2D rb2D;
        
        [SerializeField] private Collider2D col2D;
    
        [SerializeField] private AudioClip[] popAudioClips;

        private bool canMerge = true;
        
        public int MergeId => mergeId;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!canMerge) { return; }

            TryMerge(other);
        }

        private void TryMerge(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent<Mergeable>(out var mergeable)) { return; }
            
            if (mergeable.MergeId == MergeId && GetInstanceID() < mergeable.GetInstanceID())
            {
                Merge(other, mergeable);
            }
        }

        private void Merge(Collision2D other, Mergeable mergeable)
        {
            var mergeableConfig = mergeablesConfig.GetMergeableConfigList().FirstOrDefault(m => m.MergeableId == mergeId + 1);
            if (mergeableConfig != null)
            {
                var newMergeable = Instantiate(mergeableConfig.MergeablePrefab, mergeable.transform.position, mergeable.transform.rotation);
                newMergeable.DisableMergeForDuration().Forget();
            }
            Destroy(gameObject);
            Destroy(other.gameObject);
            var randomClip = popAudioClips[Random.Range(0, popAudioClips.Length)];
            AudioSource.PlayClipAtPoint(randomClip, transform.position);
        }

        public void TurnGravityOff()
        {
            rb2D.gravityScale = 0;
        }

        public void TurnGravityOn()
        {
            rb2D.gravityScale = 1;
        }

        private async UniTask DisableMergeForDuration()
        {
            canMerge = false;
            await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
            canMerge = true;
            col2D.enabled = false;
            await UniTask.NextFrame();
            col2D.enabled = true;
        }
    }
}
