using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Blackboard for <see cref="FsmState"/>s.
/// </summary>
public class FsmBlackboard : MonoBehaviour
{
    /// <summary>
    ///     Information store inside the blackboard.
    /// </summary>
    public readonly Dictionary<string, object> Variables = new();

    /// <summary>
    ///     Remove value from blackboard.
    /// </summary>
    /// <param name="valueName">The name of the vriable stored within the blackboard to remove.</param>
    /// <returns>
    ///     True if removal is successful, false otherwise.
    /// </returns>
    public bool RemoveValue(string valueName)
    {
        bool whiteboardDoesContainValue = Variables.ContainsKey(valueName);
        if (whiteboardDoesContainValue)
        {
            Variables.Remove(valueName);
            return true;
        }
        else return false;
    }

    /// <summary>
    ///     Adds or updates value in blackboard.
    /// </summary>
    /// <typeparam name="T">The type of variable to add.</typeparam>
    /// <param name="valueName">The name of the variable within the blackboard to update.</param>
    /// <param name="value">The value to update.</param>
    public void SetValue<T>(string valueName, T value)
    {
        bool whiteboardDoesContainValue = Variables.ContainsKey(valueName);
        if (whiteboardDoesContainValue)
        {
            // SET
            Variables[valueName] = value;
        }
        else
        {
            // ADD
            Variables.Add(valueName, value);
        }
    }

    /// <summary>
    ///     Get a value stored in the blackboard.
    /// </summary>
    /// <typeparam name="T">The type of variable to add.</typeparam>
    /// <param name="valueName">The name of the variable within the blackboard to update.</param>
    /// <returns>
    ///     The value requested.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///     Thrown if blackboard does not contain <paramref name="valueName"/>.
    /// </exception>
    /// <exception cref="InvalidCastException">
    ///     Thrown if requested type <typeparamref name="T"/> does not match the value's type.
    /// </exception>
    public T GetValue<T>(string valueName)
    {
        // Reject request if not contained in blackboard
        bool containsValue = Variables.ContainsKey(valueName);
        if (!containsValue)
        {
            string msg = $"{nameof(FsmBlackboard)} of {name} does not contain value called \"{valueName}\".";
            throw new ArgumentException(msg);
        }

        // Ensure type is correct
        object value = Variables[valueName];
        Type valueType = value.GetType();
        Type requestType = typeof(T);
        if (valueType != requestType)
        {
            string msg =
                $"{name}'s {nameof(FsmBlackboard)} value \"{valueName}\" is type " +
                $"\"{valueType.Name}\" but the requested type is \"{requestType}\".";
            throw new InvalidCastException(msg);
        }

        return (T)value;
    }

    public void GetBool(string valueName) => GetValue<float>(valueName);
    public void GetInt(string valueName) => GetValue<int>(valueName);
    public void GetFloat(string valueName) => GetValue<float>(valueName);
    public void GetVector3(string valueName) => GetValue<Vector3>(valueName);
    public void GetQuaternion(string valueName) => GetValue<Quaternion>(valueName);
    public void GetTransform(string valueName) => GetValue<Transform>(valueName);
    public void GetGameObject(string valueName) => GetValue<GameObject>(valueName);
    public void GetString(string valueName) => GetValue<string>(valueName);

}
