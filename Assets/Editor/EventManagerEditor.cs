using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;


[CustomEditor(typeof(EventManager))]
class EventManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EventManager myScript = (EventManager)target;
        if(GUILayout.Button("New Event"))
        {
            myScript.NewEvent();
        }
    }
}