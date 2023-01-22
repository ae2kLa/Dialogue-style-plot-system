using plot_utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


namespace plot_command_creator
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

        public List<CommandBase_SO> commandList = new List<CommandBase_SO>();
        public string fileName;
        private string txtSavePath = "Assets/Scripts/CommandCreator/Text/";

        [CustomEditor(typeof(CommandConfig))]
        public class CommandConfigEditor : Editor
        {
            public CommandConfig commandConfig;
            private int selectedIndex;
            private ReorderableList reorderableList;

            private void OnEnable()
            {
                commandConfig = (CommandConfig)target;
                commandConfig.fileName = commandConfig.name;
                selectedIndex = 0;
                reorderableList = new ReorderableList(commandConfig.commandList, typeof(CommandBase_SO), true, true, true, true);

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
                        EditorGUI.indentLevel++;
                        //EditorGUI.PropertyField(rect, property, true);//PropertyField不支持文本框输入中文

                        //或许使用字典/函数式更简洁
                        float width = 90f;
                        switch (property.propertyType)
                        {
                            case SerializedPropertyType.String:
                                EditorGUI.LabelField(new Rect(rect.x, rect.y, width, EditorGUIUtility.singleLineHeight), property.name);
                                EditorGUI.BeginChangeCheck();
                                string newValue = EditorGUI.TextField(new Rect(rect.x + width, rect.y, rect.width - width, EditorGUIUtility.singleLineHeight), property.stringValue);
                                if (EditorGUI.EndChangeCheck())
                                {
                                    property.stringValue = newValue;
                                }
                                break;
                            case SerializedPropertyType.Float:
                                EditorGUI.LabelField(new Rect(rect.x, rect.y, width, EditorGUIUtility.singleLineHeight), property.name);
                                EditorGUI.BeginChangeCheck();
                                float newFloatValue = EditorGUI.FloatField(new Rect(rect.x + width, rect.y, rect.width - width, EditorGUIUtility.singleLineHeight), property.floatValue);
                                if (EditorGUI.EndChangeCheck())
                                {
                                    property.floatValue = newFloatValue;
                                }
                                break;
                            case SerializedPropertyType.Integer:
                                EditorGUI.LabelField(new Rect(rect.x, rect.y, width, EditorGUIUtility.singleLineHeight), property.name);
                                EditorGUI.BeginChangeCheck();
                                int newIntValue = EditorGUI.IntField(new Rect(rect.x + width, rect.y, rect.width - width, EditorGUIUtility.singleLineHeight), property.intValue);
                                if (EditorGUI.EndChangeCheck())
                                {
                                    property.intValue = newIntValue;
                                }
                                break;
                            case SerializedPropertyType.Boolean:
                                EditorGUI.LabelField(new Rect(rect.x, rect.y, width, EditorGUIUtility.singleLineHeight), property.name);
                                EditorGUI.BeginChangeCheck();
                                bool newBoolValue = EditorGUI.Toggle(new Rect(rect.x + width, rect.y, rect.width - width, EditorGUIUtility.singleLineHeight), property.boolValue);
                                if (EditorGUI.EndChangeCheck())
                                {
                                    property.boolValue = newBoolValue;
                                }
                                break;
                            default: EditorGUI.PropertyField(rect, property, false);break;
                        }

                        EditorGUI.indentLevel--;
                        rect.y += rect.height;
                    }
                    serializedObject.ApplyModifiedProperties();
                    GenerateTXTCommands(commandConfig.fileName);
                };

                reorderableList.onAddCallback = (ReorderableList l) =>
                {
                    selectedIndex = EditorGUILayout.Popup("Command Type", selectedIndex, Enum.GetNames(typeof(CommandType)));
                    string className = Enum.GetName(typeof(CommandType), (CommandType)selectedIndex);
                    Type commandType = Type.GetType("plot_command_creator." + className);
                    ScriptableObject obj = ScriptableObject.CreateInstance(commandType);
                    commandConfig.commandList.Add(obj as CommandBase_SO);
                    
                };


                LoadTXTCommands(commandConfig.fileName);
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
                Type commandType = Type.GetType("plot_command_creator." + className);

                serializedObject.ApplyModifiedProperties();

                GUILayout.Box("SaveOptions", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(20) });

                EditorGUILayout.LabelField("txt generate & load path: " + commandConfig.txtSavePath + commandConfig.fileName);
                //commandConfig.fileName = EditorGUILayout.TextField("fileName: ", commandConfig.fileName);

                EditorGUILayout.LabelField("Auto save is always running.");

                //if (GUILayout.Button("GenerateTXTCommands"))
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

                #region "已弃用的写入方式。"
                /*原因：虽然能很方便的转换成Json格式，但用对应API生成对象时，由于引擎不推荐使用new创建SO对象，所以会报Warning；
                 * 自定义正则来解析Json数据或许行得通，不过我最开始就实现了TextParser，无需再针对Json格式的解析造轮子*/
                //foreach (CommandBase command in commandConfig.commandList)
                //{
                //    content += command.GetType().Name;
                //    content += "\n";
                //    string json = JsonConvert.SerializeObject(command);
                //    content += json;
                //    content += "\n";
                //}
                #endregion

                #region "把commandList中的所有对象按顺序写入文本文件"
                foreach (CommandBase_SO command in commandConfig.commandList)
                {
                    Type commandType = command.GetType();
                    if (commandType == null)
                    {
                        Debug.LogWarning("Invalid commandType.");
                        continue;
                    }
                    FieldInfo[] fields = commandType.GetFields();
                    string commandString = "[" + command.GetType().Name + "(";

                    #region "把该对象中所有的属性按我的约定格式写入文本"
                    foreach (var field in fields)
                    {
                        var value = field.GetValue(command);
                        Type fieldType = field.FieldType;
                        //筛去为空的非泛型
                        if (value == null && !fieldType.IsGenericType)
                        {
                            commandString += field.Name + "=" + "\"\"" + ",";
                            continue;
                        }

                        #region "处理泛型"
                        if (fieldType.IsGenericType)
                        {
                            //commandString += field.Name + "=" + "\"" + value + "\"" + ",";

                            //处理IList
                            if (typeof(IList).IsAssignableFrom(fieldType))
                            {
                                if (value == null) continue;

                                //string elementClassName = field.FieldType.GetGenericArguments()[0].Name;
                                int count = Convert.ToInt32(fieldType.GetProperty("Count").GetValue(value, null));
                                if (count == 0) continue;

                                var elementType = fieldType.GetProperty("Item").GetValue(value, new object[] { 0 }).GetType();
                                commandString += "elementType" + "=" + "\"" + elementType.Namespace + "." + elementType.Name + "\"" + ",";
                                for (int i = 0; i < count; i++)
                                {
                                    // 获取列表元素
                                    object item = fieldType.GetProperty("Item").GetValue(value, new object[] { i });
                                    commandString += "element" + (i + 1) + "=" + "\"" + item + "\"" + ",";
                                }

                            }
                            //TODO: 处理字典
                        }
                        #endregion
                        else if (value.GetType() == typeof(string))
                        {
                            commandString += field.Name + "=" + "\"" + value + "\"" + ",";
                        }
                        else
                        {
                            commandString += field.Name + "=" + value + ",";
                        }
                    }
                    #endregion
                    commandString = commandString.TrimEnd(',');
                    commandString += ")]";
                    //Debug.Log(commandString);
                    content += commandString + "\n";
                }
                #endregion

                System.IO.File.WriteAllText(path, content);
                //Debug.Log("Generate TXT Commands!");
            }

            private void LoadTXTCommands(string fileName)
            {
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    Debug.LogWarning("File name is NULL or empty, please input valid path!");
                    return;
                }

                string filePath = commandConfig.txtSavePath + fileName + ".txt";
                if (!File.Exists(filePath))
                {
                    Debug.LogWarning("File does not exists, please input valid path!");
                    return;
                }

                commandConfig.commandList.Clear();

                MyCommand[] mc = TextParser.ParserByLine(filePath);

                for (int i = 0; i < mc.Length; i++)
                {
                    Type commandType = Type.GetType("plot_command_creator." + mc[i].name);
                    CommandBase_SO command = ScriptableObject.CreateInstance(commandType) as CommandBase_SO;
                    TextParser.AssignCommandParams(command, commandType, mc[i].parameter);
                    commandConfig.commandList.Add(command);
                }

                Debug.Log("Load TXT Commands!");
            }
        }


    }
}


