//using UnityEngine;
//using UnityEditor;
//using System.Collections.Generic;
//using UnityEditorInternal;
//using System.Reflection;
//using System;
//using UnityEngine.Serialization;
//using Unity.VisualScripting;
//using UnityEditor.VersionControl;

//namespace plot
//{
//    public class CommandCreator : MonoBehaviour
//    {
//        public enum CommandType
//        {
//            HEADER = 0,
//            Background,
//            Delay,
//            Dialogue,
//            //Decision,
//            //Predicate,
//        }

//        public CommandConfig commandConfig;

//        [CustomEditor(typeof(CommandCreator))]
//        public class MyScriptEditor : Editor
//        {
//            public CommandCreator commandCreator;
//            private int selectedIndex;
//            private ReorderableList reorderableList;
//            private string fileName;
//            private string scriptableObjectSavePath = "Assets/Scripts/CommandCreator/SO/";
//            private string txtSavePath = "Assets/Scripts/CommandCreator/TXT";

//            private void OnEnable()
//            {
//                commandCreator = (CommandCreator)target;
//                commandCreator.commandConfig = ScriptableObject.CreateInstance<CommandConfig>();
//                selectedIndex = 0;
//                reorderableList = new ReorderableList(commandCreator.commandConfig.commandList, typeof(ScriptableObject), true, true, true, true);

//                reorderableList.elementHeightCallback = (int index) => 
//                {
//                    ScriptableObject obj = commandCreator.commandConfig.commandList[index];
//                    SerializedObject serializedObject = new SerializedObject(obj);
//                    SerializedProperty property = serializedObject.GetIterator();
//                    property.NextVisible(true);
//                    float totalHeight = EditorGUIUtility.singleLineHeight;
//                    while (property.NextVisible(true))
//                    {
//                        totalHeight += EditorGUI.GetPropertyHeight(property);
//                    }
//                    return totalHeight;
//                };

//                reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
//                {
//                    ScriptableObject obj = commandCreator.commandConfig.commandList[index];
//                    SerializedObject serializedObject = new SerializedObject(obj);
//                    SerializedProperty property = serializedObject.GetIterator();
//                    property.NextVisible(true);

//                    EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), obj.GetType().Name);
//                    rect.y += EditorGUIUtility.singleLineHeight;
//                    while (property.NextVisible(true))
//                    {
//                        rect.height = EditorGUI.GetPropertyHeight(property);
//                        int indent = EditorGUI.indentLevel;
//                        EditorGUI.indentLevel++;
//                        EditorGUI.PropertyField(rect, property, true);
//                        EditorGUI.indentLevel = indent;
//                        rect.y += rect.height;
//                    }
//                    serializedObject.ApplyModifiedProperties();
//                };

//                reorderableList.drawHeaderCallback = (Rect rect) =>
//                {
//                    EditorGUI.LabelField(rect, "Commands List");
//                };

//                reorderableList.onAddCallback = (ReorderableList l) =>
//                {
//                    selectedIndex = EditorGUILayout.Popup("Command Type", selectedIndex, Enum.GetNames(typeof(CommandType)));
//                    string className = Enum.GetName(typeof(CommandType), (CommandType)selectedIndex);
//                    Type commandType = Type.GetType("plot." + className);
//                    commandCreator.commandConfig.commandList.Add(ScriptableObject.CreateInstance(commandType));
//                };
//            }

//            public override void OnInspectorGUI()
//            {
//                serializedObject.Update();
//                //reorderableList.DoLayoutList();
//                Rect rect = EditorGUILayout.GetControlRect(false, reorderableList.GetHeight());
//                reorderableList.DoList(rect);

//                //使用Popup控件来选择要添加的类型
//                selectedIndex = EditorGUILayout.Popup("Command Type", selectedIndex, Enum.GetNames(typeof(CommandType)));
//                CommandType dialogueType = (CommandType)selectedIndex;
//                string className = Enum.GetName(typeof(CommandType), dialogueType);
//                Type commandType = Type.GetType("plot." + className);

//                serializedObject.ApplyModifiedProperties();

//                GUILayout.Box("SaveOptions", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(20) });

//                fileName = EditorGUILayout.TextField("fileName: ", fileName);

//                EditorGUILayout.LabelField("scriptableObjectSavePath: " + scriptableObjectSavePath);
//                if (GUILayout.Button("Generate ScriptableObject Commands!"))
//                {
//                    GenerateSOCommands(fileName);
//                }
//                EditorGUILayout.LabelField("txtSavePath: " + txtSavePath);
//                if (GUILayout.Button("Generate txt Commands!"))
//                {
//                    GenerateTXTCommands(fileName);
//                }
//            }

//            private void GenerateSOCommands(string fileName)
//            {
//                if (string.IsNullOrWhiteSpace(fileName))
//                {
//                    Debug.LogWarning("File name is NULL or empty. Please input valid path!");
//                    return;
//                }

//                AssetDatabase.CreateAsset(commandCreator.commandConfig, scriptableObjectSavePath + fileName + ".asset");
//                AssetDatabase.SaveAssets();
//                Debug.Log("Generate SO Commands!");
//            }

//            private void GenerateTXTCommands(string fileName)
//            {
//                if (string.IsNullOrWhiteSpace(fileName))
//                {
//                    Debug.LogWarning("File mame is NULL or empty. Please input valid path!");
//                    return;
//                }

//                AssetDatabase.CreateAsset(commandCreator.commandConfig, txtSavePath + fileName + ".asset");
//                AssetDatabase.SaveAssets();
//                Debug.Log("Generate TXT Commands!");
//            }
//        }
//    }

//}
