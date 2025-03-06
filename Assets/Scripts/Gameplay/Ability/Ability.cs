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
        
        Utility.RunCooldownTimer(Cooldown.Value).Forget();


        await Behaviour.ExecuteBehaviour(user);
        //set ability execution to true;

    }
}
