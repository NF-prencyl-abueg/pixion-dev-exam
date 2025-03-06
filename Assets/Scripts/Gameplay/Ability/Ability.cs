using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ability", menuName = "ScriptableObjects/Ability/New Ability")]
[Serializable]
public class Ability : SerializedScriptableObject, IActivateable, ICooldown
{
    public string ID;
    public Stat Cooldown;
    public AbilityBehaviour Behaviour;
    
    private bool _isOnCooldown = false;
    private float _remainingTime = 0f;
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
}