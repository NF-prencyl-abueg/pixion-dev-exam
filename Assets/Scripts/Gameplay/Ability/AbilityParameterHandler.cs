using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability Parameter Handler", menuName = "ScriptableObjects/Utility/Ability Parameter")]
public class AbilityParameterHandler : SerializedScriptableObject
{
    public bool IsAnAbilityExecuting = false;

    public Dictionary<AbilityParameterExtendableEnum, object> AbilityParameters = new Dictionary<AbilityParameterExtendableEnum, object>();

    
    public void Initialize()
    {
        IsAnAbilityExecuting = false;
        AbilityParameters = new Dictionary<AbilityParameterExtendableEnum, object>();
    }

    public void SetParameter(AbilityParameterExtendableEnum abilityParameterKey, object value)
    {
        AbilityParameters[abilityParameterKey] = value;
    }

    public T GetParameter<T>(AbilityParameterExtendableEnum abilityParameterKey)
    {
        return (T)AbilityParameters[abilityParameterKey];
    }

    public void RemoveParameter(AbilityParameterExtendableEnum abilityParameterKey)
    {
        AbilityParameters.Remove(abilityParameterKey);
    }
}