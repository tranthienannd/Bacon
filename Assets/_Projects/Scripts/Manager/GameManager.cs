using DG.Tweening;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public LevelController levelController;
    public void OnInit()
    {
        Input.multiTouchEnabled = false;
        levelController.OnInit();
    }
    
    private void LoadLevel(int level)
    {
        levelController.OnLevelLoad(level);
    }

    public void StartLevel()
    {
        LoadLevel(DataManager.Instance.GetData<DataLevel>().CurrentLevelId);
    }

    public void LoadCurrentLevel()
    {
        levelController.OnCurrentLevelLoad();
    }
    
    public void BackToHome()
    {
        UIManager.Instance.HideScreen<ScreenInGame>();
        UIManager.Instance.ShowScreen<ScreenHome>();
    }
    
    public void OnWin()
    {
        DOVirtual.DelayedCall(0.5f, () =>
        {
            DataManager.Instance.GetData<DataLevel>().PassLevel();
            UIManager.Instance.ShowPopup<PopupShowPicture>().SetPicture();
        });
    }
   
    public void OnLoss()
    {
        levelController.OnLose();
        UIManager.Instance.ShowPopup<PopupLose>();
    }
}
