using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BasePreview : EditorWindow
{
    private static Vector2 size = new Vector2(37.5f,19.5f);
    private static bool drawBorder = true;

    [MenuItem("jamtools/Preview base")]
    static void newWindow()
    {
        CreateWindow<BasePreview>();
    }

    private void OnEnable()
    {
        SceneView.beforeSceneGui += OnSceneGUI;
    }
    
    private void OnDisable()
    {
        SceneView.beforeSceneGui -= OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView pScene)
    {
        if(drawBorder)
            Handles.DrawWireCube(Vector3.zero, new Vector3(size.x, size.y, 0));
    }

    private void OnGUI()
    {
        size = EditorGUILayout.Vector2Field("Border size", size);
        drawBorder = EditorGUILayout.Toggle("Draw border", drawBorder);
    }
}
