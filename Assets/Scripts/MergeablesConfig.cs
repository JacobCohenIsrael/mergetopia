using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Mergetopia
{
    [CreateAssetMenu(menuName = "Mergetopia/MergeablesConfig", fileName = "MergeablesConfig")]
    public class MergeablesConfig : ScriptableObject
    {
        [SerializeField]
        private List<MergeableConfig> mergeableConfigList;

        public ReadOnlyCollection<MergeableConfig> GetMergeableConfigList()
        {
            return mergeableConfigList.AsReadOnly();
        }
    }

    [Serializable]
    public class MergeableConfig
    {
        public int MergeableId;
        public Mergeable MergeablePrefab;
    }
}