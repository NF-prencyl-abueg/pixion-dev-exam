using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "New Ability", menuName = "ScriptableObjects/Ability/New Ability")]
[Serializable]
public class Ability : SerializedScriptableObject, IActivateable, ICooldown
{
    public string ID;
    public AbilityExtendableEnum AbilityEnum;
    public Stat Cooldown;
    public AbilityBehaviour Behaviour;
    public AbilityParameterExtendableEnum CenterTransformParameter;
    
    private bool _isOnCooldown = false;
    private float _remainingTime = 0f;
    public async UniTask OnTriggerAbility(GameObject obj, AbilityParameterHandler abilityParameters)
    {
        if (_isOnCooldown)
            return;

        if (abilityParameters.IsAnAbilityExecuting)
        {
            abilityParameters.AbilityIsStillExecuting();
            return;
        }
        
        _isOnCooldown = true;
        RunCooldownTimer(Cooldown.Value).Forget();

        abilityParameters.SetParameter(CenterTransformParameter, obj);
        
        abilityParameters.StartAbility(AbilityEnum);
        await Behaviour.ExecuteBehaviour(abilityParameters);
        abilityParameters.FinishAbility();
    }
    
    public async UniTask RunCooldownTimer(float duration)
    {
        _remainingTime = duration;
        while (_remainingTime > 0f)
        {
            await UniTask.Yield(PlayerLoopTiming.Update);
            _remainingTime -= Time.deltaTime;
        }
        _isOnCooldown = false;
    }

    public void ResetCooldown()
    {
        _isOnCooldown = false;
    }

    public float GetNormalizedRemainingTime()
    {
        return _remainingTime / Cooldown.Value;
    }
    
}