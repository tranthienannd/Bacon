using UnityEngine;

public class PopupLose : PopupBase
{
    public void OnBtnReplayClick()
    {
        OnHide();
        GameManager.Instance.LoadCurrentLevel();
    }

    public void OnBtnHomeClick()
    {
        OnHide();
        UIManager.Instance.HideScreen<ScreenInGame>();
        UIManager.Instance.ShowScreen<ScreenHome>();
        GameManager.Instance.LoadCurrentLevel();
    }
}
