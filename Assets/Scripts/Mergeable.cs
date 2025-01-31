using System;
using UnityEngine;

public class Mergeable : MonoBehaviour
{
    [SerializeField]
    private int mergeId;
    
    [SerializeField]
    private MergeableConfig mergeConfig;

    [SerializeField] private Rigidbody2D rb2D;

    public int MergeId => mergeId;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Mergeable>(out var mergeable))
        {
            if (mergeable.mergeId == mergeId && GetInstanceID() < mergeable.GetInstanceID())
            {
                var mergeableBallConfig = mergeConfig.mergeableBallConfigList.Find(m => m.MergeableId == mergeId + 1);
                if (mergeableBallConfig != null)
                {
                    Instantiate(mergeableBallConfig.MergeablePrefab, mergeable.transform.position, mergeable.transform.rotation);
                }
                Destroy(gameObject);
                Destroy(other.gameObject);
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
