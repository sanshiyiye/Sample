using System;
using UnityEditor;
using UnityEngine;

/**
* @classdesc DeviceInfoEditor
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/
// [ExecuteInEditMode]
// [CustomEditor(typeof(DeviceInfo))]
public class DeviceInfoEditor 
{
    // SerializedObject deviceData;

    // private Vector2 _scrollPos = Vector2.zero;
    //
    //
    // private Color backgroundColor;
    // private Color gridColor;



    // [MenuItem("Window/DeviceInfoEditor")]
    // static void OpenEditor()
    // {
    //     // DeviceInfoEditor editor = EditorWindow.GetWindow<DeviceInfoEditor>();
    //     // editor.Init();
    // }
    //
    // void Init()
    // {
    //     
    //     backgroundColor = new Color(0.4f, 0.4f, 0.4f);
    //     gridColor = new Color(0.1f, 0.1f, 0.1f);
    // }
    //
    // private void OnEnable()
    // {
    //     deviceData = new SerializedObject(Resources.Load<DeviceInfo>("DeviceInfo"));
    //     
    // }
    //
    // private void OnGUI()
    // {
    //     _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos,GUILayout.Width(position.width-10), GUILayout.Height(position.height - 50));
    //     
    //     EditorGUILayout.Space ();
    //     GUILayout.BeginVertical ("DeviceInfo", "window",GUILayout.Height(50));
    //
    //
	   //
    //
    //     EditorGUILayout.Space ();
    //
    //     // EditorGUILayout.PropertyField ("globalData");
    //     // EditorGUILayout.PropertyField (globalData.FindProperty ("globle2"));
    //     // EditorGUILayout.PropertyField (globalData.FindProperty ("globleType"));
    //     GUILayout.EndVertical ();
    //     
    //     EditorGUILayout.EndScrollView();
    //
    // }
    // private DeviceInfo deviceInfo;
    //
    // private SerializedProperty lowLevelDevices;
    //
    // private SerializedProperty midLevelDevices;
    //
    // private SerializedProperty highLevelDevices;
    

    // private void OnEnable()
    // {
    //     lowLevelDevices = serializedObject.FindProperty("lowLevelDevices");
    //     midLevelDevices = serializedObject.FindProperty("midLevelDevices");
    //     highLevelDevices = serializedObject.FindProperty("highLevelDevices");
    // }

    // public override void OnInspectorGUI() {
    //     deviceInfo = (DeviceInfo)target;
    //
    //
    //     //设置界面垂直布局
    //     GUILayout.BeginVertical();
    //
    //     //空一行
    //     GUILayout.Space(0);
    //     EditorGUILayout.LabelField("Device Info");
    //     // deviceInfo.ChatContent = EditorGUILayout.
    //     EditorGUILayout.PropertyField(lowLevelDevices, true);
    //
    //     EditorGUILayout.PropertyField(midLevelDevices, true);
    //     
    //     EditorGUILayout.PropertyField(highLevelDevices, true);
    //     if(GUILayout.Button("Save", GUILayout.Width(200))){
    //         Debug.Log("创建Map");
    //     }
    //     
    //
    //     //垂直布局end
    //     GUILayout.EndVertical();
    //     GUILayout.Label("This is a Label in a Custom Editor");
    // }
    
}
