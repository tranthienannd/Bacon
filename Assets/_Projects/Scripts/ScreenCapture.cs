using UnityEngine;

public class ScreenCapture : MonoSingleton<ScreenCapture>
{
    public Camera cameraToCapture;
    public int captureWidth = 800; 
    public int captureHeight = 1000; 

    public Texture2D CaptureScreenshotAsTexture()
    {
        RenderTexture renderTexture = new RenderTexture(captureWidth, captureHeight, 24);
        
        cameraToCapture.targetTexture = renderTexture;
        cameraToCapture.Render();

        Texture2D screenshot = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        screenshot.Apply();

        cameraToCapture.targetTexture = null;
        RenderTexture.active = null;

        Destroy(renderTexture);

        return screenshot;
    }
}
