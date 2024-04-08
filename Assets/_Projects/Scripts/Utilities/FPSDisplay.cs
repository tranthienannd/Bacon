﻿///This script has been taken from : http://wiki.unity3d.com/index.php?title=FramesPerSecond
///Author: Dave Hampson 

using UnityEngine;
 
public class FPSDisplay : MonoBehaviour
{
	private float _deltaTime = 0.0f;

	private void Update()
	{
		_deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
	}

	private void OnGUI()
	{
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 50;
        style.normal.textColor = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        float msec = _deltaTime * 1000.0f;
        float fps = 1.0f / _deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}