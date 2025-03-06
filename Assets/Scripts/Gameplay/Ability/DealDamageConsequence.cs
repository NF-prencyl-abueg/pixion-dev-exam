using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deal Damage Consequence", menuName = "ScriptableObjects/Ability/Deal Damage Consequence")]
public class DealDamageConsequence : Consequence
{
    public Stat Damage;
    
    public override async UniTask ExecuteConsequence(GameObject obj)
    {
        if (!obj)
            return;

        if (!obj.TryGetComponent<Health>(out var targetHealth))
            return;
        
        targetHealth.ApplyDamage(Damage.Value);
    }
}