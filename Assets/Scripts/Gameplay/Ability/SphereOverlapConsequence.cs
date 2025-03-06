using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sphere Overlap Consequence", menuName = "ScriptableObjects/Ability/Sphere Overlap Consequence")]
public class SphereOverlapConsequence : Consequence
{
    public Stat Radius;

    public override async UniTask ExecuteConsequence(GameObject obj)
    {
        Vector3 center = obj.transform.position;
        Collider[] colliders = Physics.OverlapSphere(center, Radius.Value);
        
        foreach (Collider collider in colliders)
        {
            if (!collider.CompareTag("Enemy"))
                return;
            
            ExecuteNextConsequence(collider.gameObject).Forget();
        }
    }
}