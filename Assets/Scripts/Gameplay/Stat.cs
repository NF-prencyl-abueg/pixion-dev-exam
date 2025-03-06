using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Stat", menuName = "ScriptableObjects/Stat")]
public class Stat : SerializedScriptableObject
{
    [FormerlySerializedAs("value")] public float Value;
}