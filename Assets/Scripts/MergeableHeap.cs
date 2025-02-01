using UnityEngine;

namespace Mergetopia
{
    public class MergeableHeap : MonoBehaviour
    {
        [SerializeField]
        private MergeablesConfig mergeablesConfig;

        [SerializeField] private int maxMergeableIndex = 4;
        
        private MergeableConfig nextMergeableConfig;
        private void Start()
        {
            RandomizeMergeable();
        }

        public MergeableConfig GetNextMergeable()
        {
            var config = nextMergeableConfig;
            RandomizeMergeable();
            return config;
        }

        public MergeableConfig PeakNextMergeable()
        {
            return nextMergeableConfig;
        }
        
        private void RandomizeMergeable()
        {
            var randomInt = Random.Range(0, maxMergeableIndex);
            nextMergeableConfig = mergeablesConfig.GetMergeableConfigList()[randomInt];
        }
    }
}
