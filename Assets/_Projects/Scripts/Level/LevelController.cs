using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Level level;
    public LevelAsset currentMapAsset;
    public HandController handController;
    public Transform spawnHandPos;

    public bool isReady;
    public void OnInit()
    {
        
    }

    public void OnLevelLoad(int levelId)
    {
        DestroyCurrentLevel();

        level = PoolManager.Instance.SpawnObject(currentMapAsset.GetLevel(levelId).transform, Vector3.zero, Quaternion.identity, transform).GetComponent<Level>();
        if (level != null)
        {
            level.OnInit();
        }

    }

    public void DestroyCurrentLevel()
    {
        if (level == null) return;
        level.Clear();
        DespawnHand();
        level = null;
        handController = null;
        isReady = false;
    }

    public void OnLevelStart()
    {
        OnCurrentLevelLoad();
    }

    private void MoveBaconCallback()
    {
        isReady = true;
    }

    public void OnLose()
    {
        isReady = false;
        DespawnHand();
    }

    public void OnCurrentLevelLoad()
    {
        handController = InstantiateHand();
        handController.OnInit();
        handController.MoveToTargetPos(3f, 0.5f, 0.2f, MoveBaconCallback);
    }
    
    private HandController InstantiateHand()
    {
        return PoolManager.Instance
            .SpawnObject(currentMapAsset.handPrefab.transform, spawnHandPos.position, Quaternion.identity, transform)
            .GetComponent<HandController>();
    }
    
    private void DespawnHand()
    {
        handController.Despawn();
    }

    #region DEBUG
    #endregion
}
