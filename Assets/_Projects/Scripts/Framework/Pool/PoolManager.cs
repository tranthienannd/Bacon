using UnityEngine;
using PathologicalGames;

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField] private PoolAsset poolAsset;
    [SerializeField] private SpawnPool poolDestroy = null;
    
    // Dont despawn when game-end
    [SerializeField] private SpawnPool poolDontDestroy = null;
    
    protected override void Initiate()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    public Transform SpawnDontDestroy(Transform prefab)
    {
        if (prefab == null)
        {
            Debug.LogError("[error]Cant spawn, Missing prefab");
            return null;
        }
        Transform clone = poolDontDestroy.Spawn(prefab);
        return AssignPool(clone, poolDontDestroy);
    }

    public Transform SpawnObject(PoolType poolType)
    {
        return SpawnObject(poolAsset.GetPoolElement(poolType).transform);
    }

    public Transform SpawnObject(PoolType poolType, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        return SpawnObject(poolAsset.GetPoolElement(poolType).transform, position, rotation, parent);
    }

    public PoolElement SpawnPoolElement(PoolType poolType)
    {
        return SpawnObject(poolAsset.GetPoolElement(poolType).transform).GetComponent<PoolElement>();
    }

    public PoolElement SpawnPoolElement(PoolType poolType, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        return SpawnObject(poolAsset.GetPoolElement(poolType).transform, position, rotation, parent).GetComponent<PoolElement>();
    }

    public Transform SpawnObject(Transform prefab)
    {
        if (prefab == null)
        {
            Debug.LogError("[error]Cant spawn, Missing prefab");
            return null;
        }
        Transform clone = poolDestroy.Spawn(prefab);
        return AssignPool(clone, poolDestroy);
    }

    public Transform SpawnObject(Transform prefab, Vector3 position, Quaternion rotation)
    {
        if (prefab == null)
        {
            Debug.LogError("[error]Cant spawn, Missing prefab");
            return null;
        }
        Transform clone = poolDestroy.Spawn(prefab, position, rotation);
        return AssignPool(clone, poolDestroy);
    }

    public Transform SpawnObject(Transform prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (prefab == null)
        {
            Debug.LogError("[error]Cant spawn, Missing prefab");
            return null;
        }
        Transform clone = poolDestroy.Spawn(prefab, position, rotation, parent);
        return AssignPool(clone, poolDestroy);
    }

    private Transform AssignPool(Transform clone, SpawnPool pool)
    {
        clone.GetComponent<PoolElement>()?.AssignSelf(pool);
        return clone;
    }

    public void DeSpawnAll()
    {
        this.poolDestroy?.DespawnAll();
    }

    public void DespawnObject(Transform clone)
    {
        if (poolDestroy.IsSpawned(clone))
        {
            poolDestroy.Despawn(clone);
            clone.SetParent(poolDestroy.transform);
        }
        else
        {
            if (clone != null)
                clone.gameObject.SetActive(false);
        }
    }
}
