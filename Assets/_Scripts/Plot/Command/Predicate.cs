using plot_command_creator;
using plot_command_executor;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace plot_command
{
    public enum ValueRange
    {
        decision0 = 0,
        decision1,
        decision2,
        decision3,
        decision4,
        end
    };

    [Serializable]
    public class Predicate : CommandBase
    {

        [field: SerializeField]
        public ValueRange value = ValueRange.decision0;

        public override void Execute()
        {
            if (this.value == ValueRange.end) return;

            //不断出队，直至队头为 Predicate value == -1 的下一个命令
            while (CommandSender.Instance.GetCommandsCount() != 0)
            {
                CommandBase command = CommandSender.Instance.DequeueCommand();
                Predicate p = command as Predicate;
                if (p != null && p.value == ValueRange.end) break;
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
                backgroundRect.height = EditorGUIUtility.singleLineHeight * 2;
                EditorGUI.DrawRect(backgroundRect, new Color(255 / 255f, 235 / 255f, 181/255f));

                // Draw label
                Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
                EditorGUI.LabelField(labelRect, label, EditorStyles.boldLabel);

                // Draw value field
                Rect valueRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("value"), false);

                EditorGUI.EndProperty();
            }

            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                return base.GetPropertyHeight(property, label) + EditorGUIUtility.singleLineHeight;
            }
        }


    }
}