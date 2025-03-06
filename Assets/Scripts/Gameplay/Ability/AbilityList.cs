using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "New Ability List", menuName = "ScriptableObjects/Ability/Ability List")]
public class AbilityList : SerializedScriptableObject
{
    [SerializeField][OdinSerialize] public Dictionary<AbilityExtendableEnum, Ability> AbilityDictionary = new Dictionary<AbilityExtendableEnum, Ability>();
}
