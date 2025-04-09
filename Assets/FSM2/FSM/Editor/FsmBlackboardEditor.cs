using UnityEditor;

/// <summary>
///     Editor for <see cref="FsmBlackboard"/>.
/// </summary>
[CustomEditor(typeof(FsmBlackboard))]
public class FsmBlackboardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var blackboard = (FsmBlackboard)target;
        EditorGUILayout.LabelField("Blackboard Variables", EditorStyles.boldLabel);
        if (blackboard.Variables.Count > 0)
        {
            foreach (var keyValuePair in blackboard.Variables)
            {
                var key = keyValuePair.Key.ToString();
                var value = keyValuePair.Value.ToString();
                var type = keyValuePair.Value.GetType().Name;
                EditorGUILayout.TextField($"{key} ({type})", value);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Blackboard is empty", MessageType.Info);
        }
        EditorGUILayout.Separator();
    }

}