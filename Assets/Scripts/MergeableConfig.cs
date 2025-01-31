using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mergetopia/MergeableConfig", fileName = "MergeableConfig")]
public class MergeableConfig : ScriptableObject
{
    public List<MergeableBallConfig> mergeableBallConfigList;
}

[Serializable]
public class MergeableBallConfig
{
    public int MergeableId;
    public Mergeable MergeablePrefab;
}
