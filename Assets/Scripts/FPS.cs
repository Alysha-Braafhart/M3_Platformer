using UnityEngine;

public class FPS : MonoBehaviour
{
    GUIStyle style;
    void Start()
    {
        style = new GUIStyle();
        style.fontSize = 30;
        style.normal.textColor = Color.white;
    }
    void OnGUI()
    {
        float fps = 1.0f / Time.deltaTime;
        GUI.Label(new Rect(10, 10, 200, 50), "FPS: " + Mathf.Round(fps), style);
    }
}

