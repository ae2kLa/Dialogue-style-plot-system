using plot_command;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

#if UNITY_EDITOR

namespace plot_command_creator
{
    [CreateAssetMenu(fileName = "PlotCommandConfig", menuName = "PlotCommandConfig")]
    public class CommandConfig : ScriptableObject
    {
        [SerializeReference]
        public List<CommandBase> commandList = new List<CommandBase>();



        [CustomEditor(typeof(CommandConfig))]
        public class CommandConfigEditor : Editor
        {
            public CommandConfig commandConfig;
            private int selectedIndex;
            private static List<Type> commandTypes = new List<Type>();

            [DidReloadScripts]
            private static void OnRecompile()
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var types = assemblies.SelectMany(assembly => assembly.GetTypes());
                var filterTypes = types.Where(type => type.IsSubclassOf(typeof(CommandBase)) && !type.ContainsGenericParameters && type.IsClass);
                commandTypes = filterTypes.ToList();
            }

            private void OnEnable()
            {
                commandConfig = (CommandConfig)target;
                selectedIndex = 0;
            }

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                serializedObject.Update();

                string[] names = new string[commandTypes.Count];
                for (int i = 0; i < commandTypes.Count; i++)
                    names[i] = commandTypes[i].Name;
                selectedIndex = EditorGUILayout.Popup("Command Type", selectedIndex, names);
                Type commandType = Type.GetType("plot_command." + names[selectedIndex]);

                if (GUILayout.Button("Add"))
                {
                    var obj = Activator.CreateInstance(commandType) as CommandBase;
                    if (obj == null) return;
                    commandConfig.commandList.Add(obj);
                }

                serializedObject.ApplyModifiedProperties();
            }

        }


    }
}

#endif
