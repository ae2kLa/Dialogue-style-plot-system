using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace plot
{
    [Serializable]
    [CreateAssetMenu(fileName = "PlotCommandConfig", menuName = "PlotCommandConfig")]
    public class CommandConfig : ScriptableObject
    {
        public enum CommandType
        {
            HEADER = 0,
            Background,
            Delay,
            Dialogue,
            Decision,
            Predicate
        }

        public List<CommandBase> commandList = new List<CommandBase>();
        public string fileName;
        private string txtSavePath = "Assets/Scripts/CommandCreator/TXT/"; 

        [CustomEditor(typeof(CommandConfig))]
        public class CommandConfigEditor : Editor
        {
            public CommandConfig commandConfig;
            private int selectedIndex;
            private ReorderableList reorderableList;
            
            private void OnEnable()
            {
                commandConfig = (CommandConfig)target;
                selectedIndex = 0;
                reorderableList = new ReorderableList(commandConfig.commandList, typeof(CommandBase), true, true, true, true);
                commandConfig.fileName = commandConfig.name;

                reorderableList.elementHeightCallback = (int index) =>
                {
                    ScriptableObject obj = commandConfig.commandList[index];
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
                    ScriptableObject obj = commandConfig.commandList[index];
                    SerializedObject serializedObject = new SerializedObject(obj);
                    SerializedProperty property = serializedObject.GetIterator();
                    property.NextVisible(true);

                    EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), obj.GetType().Name);
                    rect.y += EditorGUIUtility.singleLineHeight;
                    while (property.NextVisible(true))
                    {
                        rect.height = EditorGUI.GetPropertyHeight(property);
                        int indent = EditorGUI.indentLevel;
                        EditorGUI.indentLevel++;
                        EditorGUI.PropertyField(rect, property, true);
                        EditorGUI.indentLevel = indent;
                        rect.y += rect.height;
                    }
                    serializedObject.ApplyModifiedProperties();
                    GenerateTXTCommands(commandConfig.fileName);
                };

                reorderableList.onAddCallback = (ReorderableList l) =>
                {
                    selectedIndex = EditorGUILayout.Popup("Command Type", selectedIndex, Enum.GetNames(typeof(CommandType)));
                    string className = Enum.GetName(typeof(CommandType), (CommandType)selectedIndex);
                    Type commandType = Type.GetType("plot." + className);
                    ScriptableObject obj = ScriptableObject.CreateInstance(commandType);
                    Debug.Log(obj);
                    commandConfig.commandList.Add(obj as CommandBase);
                };

                //在列表中元素的顺序、数量发生变化时被调用
                reorderableList.onChangedCallback = (ReorderableList l) =>
                {
                    GenerateTXTCommands(commandConfig.fileName);
                };
            }

            public override void OnInspectorGUI()
            {
                serializedObject.Update();
                Rect rect = EditorGUILayout.GetControlRect(false, reorderableList.GetHeight());
                reorderableList.DoList(rect);

                //使用Popup控件来选择要添加的类型
                selectedIndex = EditorGUILayout.Popup("Command Type", selectedIndex, Enum.GetNames(typeof(CommandType)));
                CommandType dialogueType = (CommandType)selectedIndex;
                string className = Enum.GetName(typeof(CommandType), dialogueType);
                Type commandType = Type.GetType("plot." + className);

                serializedObject.ApplyModifiedProperties();

                GUILayout.Box("SaveOptions", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(20) });

                EditorGUILayout.LabelField("txt generate & load path: " + commandConfig.txtSavePath + commandConfig.fileName);
                //commandConfig.fileName = EditorGUILayout.TextField("fileName: ", commandConfig.fileName);

                EditorGUILayout.LabelField("Auto save is always running.");
                //if (GUILayout.Button("Save commands in txt"))
                //{
                //    GenerateTXTCommands(commandConfig.fileName);
                //}

                if (GUILayout.Button("Load txt File"))
                {
                    LoadTXTCommands(commandConfig.fileName);
                }
            }

            private void GenerateTXTCommands(string fileName)
            {
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    Debug.LogWarning("File mame is NULL or empty. Please input valid path!");
                    return;
                }
                
                string path = commandConfig.txtSavePath + fileName + ".txt";
                string content = "";
                foreach (CommandBase command in commandConfig.commandList)
                {
                    content += command.GetType().Name;
                    content += "\n";
                    //content += JsonUtility.ToJson(command); 
                    //content += "\n";
                    string json = JsonConvert.SerializeObject(command);
                    content += json;
                    content += "\n";
                }
                File.WriteAllText(path, content);

                Debug.Log("Generate TXT Commands!");
            }

            private void LoadTXTCommands(string fileName)
            {
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    Debug.LogWarning("File name is NULL or empty. Please input valid path!");
                    return;
                }

                string filePath = commandConfig.txtSavePath + fileName + ".txt";
                if (!File.Exists(filePath))
                {
                    Debug.LogWarning("File not exists! Please input valid path!");
                    return;
                }

                commandConfig.commandList.Clear();
                string[] lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i += 2)
                {
                    string className = lines[i];
                    Type commandType = Type.GetType("plot." + className);
                    if (commandType == null)
                    {
                        Debug.LogWarning("Invalid class name: " + className);
                        continue;
                    }

                    //CommandBase command = (CommandBase)JsonUtility.FromJson(lines[i+1], commandType);
                    CommandBase command = (CommandBase)JsonConvert.DeserializeObject(lines[i + 1] , commandType);
                    if (command == null)
                    {
                        Debug.LogWarning("Failed to create instance of " + className);
                        continue;
                    }
                    commandConfig.commandList.Add(command);
                }
                Debug.Log("Load TXT Commands!");

            }
        }


    }
}


