using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditorInternal;
using System.Reflection;
using System;

namespace plot
{
    public class CommandCreator : MonoBehaviour
    {
        public enum CommandType
        {
            HEADER = 0,
            Background,
            Delay,
            Dialogue,
            //Decision,
            //Predicate,
        }

        public List<ScriptableObject> commandList = new List<ScriptableObject>();

        [CustomEditor(typeof(CommandCreator))]
        public class MyScriptEditor : Editor
        {
            public CommandCreator commandCreator;
            private int selectedIndex;
            private ReorderableList reorderableList;

            private void OnEnable()
            {
                commandCreator = (CommandCreator)target;
                selectedIndex = 0;
                reorderableList = new ReorderableList(commandCreator.commandList, typeof(ScriptableObject), true, true, true, true);

                reorderableList.elementHeightCallback = (int index) => 
                {
                    ScriptableObject obj = commandCreator.commandList[index];
                    SerializedObject serializedObject = new SerializedObject(obj);
                    SerializedProperty property = serializedObject.GetIterator();
                    property.NextVisible(true);
                    float totalHeight = EditorGUIUtility.singleLineHeight;
                    while (property.NextVisible(true))
                    {
                        totalHeight += EditorGUI.GetPropertyHeight(property);
                    }
                    return totalHeight;
                };

                reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    ScriptableObject obj = commandCreator.commandList[index];
                    SerializedObject serializedObject = new SerializedObject(obj);
                    SerializedProperty property = serializedObject.GetIterator();
                    property.NextVisible(true);

                    EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), obj.GetType().Name);
                    rect.y += EditorGUIUtility.singleLineHeight;
                    while (property.NextVisible(true))
                    {
                        rect.height = EditorGUI.GetPropertyHeight(property);
                        EditorGUI.PropertyField(rect, property, true);
                        rect.y += rect.height;
                    }
                    serializedObject.ApplyModifiedProperties();
                };

                reorderableList.drawHeaderCallback = (Rect rect) =>
                {
                    EditorGUI.LabelField(rect, "Command List");
                };

                reorderableList.onAddCallback = (ReorderableList l) =>
                {
                    selectedIndex = EditorGUILayout.Popup("Command Type", selectedIndex, Enum.GetNames(typeof(CommandType)));
                    string className = Enum.GetName(typeof(CommandType), (CommandType)selectedIndex);
                    Type commandType = Type.GetType("plot." + className);
                    commandCreator.commandList.Add(ScriptableObject.CreateInstance(commandType));
                };
            }

            public override void OnInspectorGUI()
            {
                serializedObject.Update();
                //reorderableList.DoLayoutList();
                Rect rect = EditorGUILayout.GetControlRect(false, reorderableList.GetHeight());
                reorderableList.DoList(rect);

                GUILayout.Box("FunctionButtons", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(20) });

                // 使用Popup控件来选择要添加的类型
                selectedIndex = EditorGUILayout.Popup("Command Type", selectedIndex, Enum.GetNames(typeof(CommandType)));
                CommandType dialogueType = (CommandType)selectedIndex;
                string className = Enum.GetName(typeof(CommandType), dialogueType);
                Type commandType = Type.GetType("plot." + className);

                if (GUILayout.Button("Add Command"))
                {
                    commandCreator.commandList.Add(ScriptableObject.CreateInstance(commandType));
                }

                serializedObject.ApplyModifiedProperties();

                if (GUILayout.Button("Generate Commands!"))
                {
                    GenerateCommands();
                }
            }

            private void GenerateCommands()
            {
                string generatedString = "Generate Commands!";
                Debug.Log(generatedString);
            }
        }

        //[CustomEditor(typeof(CommandCreator))]
        //public class MyScriptEditor : Editor
        //{
        //    public CommandCreator commandCreator;
        //    private int selectedIndex;

        //    private void OnEnable()
        //    {
        //        commandCreator = (CommandCreator)target;
        //        selectedIndex = 0;
        //    }

        //    public override void OnInspectorGUI()
        //    {
        //        serializedObject.Update();

        //        // 显示List中的元素
        //        for (int i = 0; i < commandCreator.commandList.Count; i++)
        //        {
        //            EditorGUILayout.ObjectField(commandCreator.commandList[i], typeof(ScriptableObject), true);

        //            ScriptableObject obj = commandCreator.commandList[i];
        //            SerializedObject serializedObject = new SerializedObject(obj);
        //            SerializedProperty property = serializedObject.GetIterator();
        //            property.NextVisible(true);

        //            // 遍历所有的属性
        //            while (property.NextVisible(true))
        //            {
        //                EditorGUILayout.PropertyField(property, true);
        //            }

        //            serializedObject.ApplyModifiedProperties();
        //        }

        //        //EditorGUILayout.Separator();
        //        GUILayout.Box("FunctionButtons", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(20) });

        //        // 使用Popup控件来选择要添加的类型
        //        selectedIndex = EditorGUILayout.Popup("Command Type", selectedIndex, Enum.GetNames(typeof(CommandType)));
        //        CommandType dialogueType = (CommandType)selectedIndex;
        //        string className = Enum.GetName(typeof(CommandType), dialogueType);
        //        Type commandType = Type.GetType("plot." + className);

        //        if (GUILayout.Button("Add Command"))
        //        {
        //            commandCreator.commandList.Add(ScriptableObject.CreateInstance(commandType));
        //        }

        //        serializedObject.ApplyModifiedProperties();

        //        if (GUILayout.Button("Generate Commands!"))
        //        {
        //            GenerateCommands();
        //        }
        //    }

        //    private void GenerateCommands()
        //    {
        //        string generatedString = "Generate Commands!";
        //        Debug.Log(generatedString);
        //    }
        //}
    }

}
