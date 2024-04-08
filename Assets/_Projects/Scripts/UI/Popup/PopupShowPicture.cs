using UnityEngine;
using UnityEngine.UI;

public class PopupShowPicture : PopupBase
{
    [SerializeField] private Image picture;

    public void OnBtnNextClick()
    {
        OnHide();
        UIManager.Instance.ShowPopup<PopupNextLevel>();
        GameManager.Instance.StartLevel();
    }
    
    public void SetPicture()
    {
        Texture2D texture = ScreenCapture.Instance.CaptureScreenshotAsTexture();
        picture.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}
