using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ability", menuName = "ScriptableObjects/Ability/New Ability")]
[Serializable]
public class Ability : SerializedScriptableObject
{
    public string ID;
    public Stat Cooldown;
    public AbilityBehaviour Behaviour;
    
    
    private bool _isOnCooldown = false;

    public async UniTask OnTriggerAbility(GameObject user)
    {
        if (_isOnCooldown)
            return;
        
        //add logic for if another ability is executing;

        _isOnCooldown = true;
        
        RunCooldownTimer(Cooldown.Value).Forget();


        await Behaviour.ExecuteBehaviour(user);
    }
    
    public async UniTask RunCooldownTimer(float duration)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(duration));
        _isOnCooldown = false;
    }

    public void ResetCooldown()
    {
        _isOnCooldown = false;
    }
}
