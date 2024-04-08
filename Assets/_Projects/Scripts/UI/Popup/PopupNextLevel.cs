using TMPro;
using UnityEngine;

public class PopupNextLevel : PopupBase
{
    [SerializeField] private TextMeshProUGUI levelName;

    public override void OnShow()
    {
        levelName.text = $"Food Hack #{DataManager.Instance.GetData<DataLevel>().CurrentLevelId}";
        base.OnShow();
    }

    public void OnBtnNextClick()
    {
        OnHide();
        GameManager.Instance.levelController.OnLevelStart();
    }
}
