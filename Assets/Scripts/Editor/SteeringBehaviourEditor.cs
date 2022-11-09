using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(SteeringBehaviour))]
public class SteeringBehaviourEditor : Editor
{
    private readonly GUIContent _addSeekLabel = new GUIContent("Add Seek");
    private readonly GUIContent _addFleeLabel = new GUIContent("Add Flee");
    private readonly GUIContent _addEvadeLabel = new GUIContent("Add Evade");
    private readonly GUIContent _addPursueLabel = new GUIContent("Add Pursue");



    private SteeringBehaviour _steeringBehaviour;

    public override void OnInspectorGUI()
    {
        _steeringBehaviour = (SteeringBehaviour) target;

        _steeringBehaviour.updateType =
            (PhysicsUpdateTypes) EditorGUILayout.EnumPopup("Physics Update Type: ", _steeringBehaviour.updateType);
        
        EditorGUILayout.Space();
        ShowModuleDetails(typeof(ISeeker), _addSeekLabel);
        EditorGUILayout.Space();
        ShowModuleDetails(typeof(IFlee), _addFleeLabel);
        EditorGUILayout.Space();
        ShowModuleDetails(typeof(IEvade), _addEvadeLabel);
        EditorGUILayout.Space();
        ShowModuleDetails(typeof(IPursue), _addPursueLabel);
        EditorGUILayout.Space();
    }
    
    void AddModule(object o)
    {
        Undo.AddComponent(_steeringBehaviour.gameObject, (Type)o);
    }
    
     void ShowModuleDetails(Type baseType, GUIContent buttonLabel)
     {
         if (EditorGUILayout.DropdownButton(buttonLabel, FocusType.Passive))
         { 
             GenericMenu menu = new GenericMenu(); 
             List<Type> validTypes = GetScriptTypes(baseType); 
             foreach (var t in validTypes) 
                 menu.AddItem(new GUIContent(t.ToString()), false, AddModule, t); 
             menu.ShowAsContext();
         }

            var modules = _steeringBehaviour.GetComponents(baseType);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Existing: ", EditorStyles.boldLabel, GUILayout.Width(60));
            EditorGUILayout.BeginVertical();
            
            if (modules.Length > 0)
            {
                for (int i = 0; i < modules.Length; ++i)
                {
                    // Check if module is on root object
                    bool isRoot = (modules[i].transform == _steeringBehaviour.transform);

                    // Label string
                    string labelString = isRoot ?
                        modules[i].GetType().Name :
                        string.Format("{0} ({1})", modules[i].GetType().Name, modules[i].gameObject.name);

                    // Get label rect
                    var rect = EditorGUILayout.GetControlRect();
                    bool canViewChild = !isRoot && modules[i].gameObject.scene.IsValid();
                    if (canViewChild)
                        rect.width -= 20;
                    
                    EditorGUI.LabelField(rect, labelString);

                    if (canViewChild)
                    {
                        rect.x += rect.width;
                        rect.width = 20;
                        if (GUI.Button(rect, EditorGUIUtility.FindTexture("d_ViewToolOrbit"), EditorStyles.label))
                            EditorGUIUtility.PingObject(modules[i].gameObject);
                    }
                }
            }
            else
                EditorGUILayout.LabelField("<none>");
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

     List<Type> GetScriptTypes(Type baseClass)
     {
         List<Type> result = new List<Type>();

         var guids = AssetDatabase.FindAssets("t:MonoScript");
         for (int i = 0; i < guids.Length; ++i)
         {
             var script = AssetDatabase.LoadAssetAtPath<MonoScript>(AssetDatabase.GUIDToAssetPath(guids[i]));
             var t = script.GetClass();
             if (t != null && baseClass.IsAssignableFrom(t) && script.GetClass().IsSubclassOf(typeof(MonoBehaviour)))
                 result.Add(t);
         }

         return result;
     }
}
