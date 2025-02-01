using System.Linq;
using UnityEngine;

namespace Mergetopia
{
    public class Mergeable : MonoBehaviour
    {
        [SerializeField]
        private int mergeId;
    
        [SerializeField]
        private MergeablesConfig mergeablesConfig;

        [SerializeField] private Rigidbody2D rb2D;
    
        [SerializeField] private AudioClip[] popAudioClips;

        public int MergeId => mergeId;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Mergeable>(out var mergeable))
            {
                if (mergeable.MergeId == MergeId && GetInstanceID() < mergeable.GetInstanceID())
                {
                    var mergeableBallConfig = mergeablesConfig.GetMergeableConfigList().FirstOrDefault(m => m.MergeableId == mergeId + 1);
                    if (mergeableBallConfig != null)
                    {
                        Instantiate(mergeableBallConfig.MergeablePrefab, mergeable.transform.position, mergeable.transform.rotation);
                    }
                    Destroy(gameObject);
                    Destroy(other.gameObject);
                    var randomClip = popAudioClips[Random.Range(0, popAudioClips.Length)];
                    AudioSource.PlayClipAtPoint(randomClip, transform.position);
                }
            }
        }

        public void TurnGravityOff()
        {
            rb2D.gravityScale = 0;
        }

        public void TurnGravityOn()
        {
            rb2D.gravityScale = 1;
        }
    }
}
