using plot_command_executor;
using System;
using UnityEditor;
using UnityEngine;

namespace plot_command
{
    [Serializable]
    public class Predicate : CommandBase
    {

        [field: SerializeField]
        public int value = -1;

        public override void Execute()
        {
            if (this.value == -1) return;

            //不断出队，直至队头为 Predicate value == -1 的下一个命令
            while (CommandSender.Instance.GetCommandsCount() != 0)
            {
                CommandBase command = CommandSender.Instance.DequeueCommand();
                Predicate p = command as Predicate;
                if (p != null && p.value == -1) break;
            }
        }

        public override void OnUpdate()
        {

        }

        public override bool IsFinished()
        {
            return true;
        }

        [CustomPropertyDrawer(typeof(Predicate))]
        public class PredicateDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                EditorGUI.BeginProperty(position, label, property);

                // Draw background color
                Rect backgroundRect = position;
                backgroundRect.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.DrawRect(backgroundRect, Color.yellow);

                // Draw label and value field
                Rect labelRect = position;
                labelRect.width = EditorGUIUtility.labelWidth;
                EditorGUI.LabelField(labelRect, label);
                Rect valueRect = position;
                valueRect.xMin = labelRect.xMax;
                EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("value"), GUIContent.none);
                EditorGUI.EndProperty();
            }
        }

    }
}