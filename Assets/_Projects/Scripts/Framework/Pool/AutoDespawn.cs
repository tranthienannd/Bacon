using UnityEngine;

public class AutoDespawn : PoolElement
{
    [SerializeField] private float timeLife = 0.2f;
    private void OnEnable()
    {
        DespawnSelf(timeLife);
    }
}
