using UnityEngine;

public class ScreenInGame : ScreenBase
{
   public override void OnShow()
   {
      base.OnShow();
      GameManager.Instance.StartLevel();
      UIManager.Instance.ShowPopup<PopupNextLevel>();
   }
}
