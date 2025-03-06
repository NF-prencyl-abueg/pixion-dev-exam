using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sphere Overlap Consequence", menuName = "ScriptableObjects/Ability/Sphere Overlap Consequence")]
public class SphereOverlapConsequence : Consequence
{
    public Stat Radius;
    public Stat FrontOffset;
    
    public bool IsVisualized = false;
    [ShowIf("IsVisualized")]
    public SphereOverlapConsequenceVisualizer VisualizerPrefab;
    
    public override async UniTask ExecuteConsequence(GameObject obj)
    {
        Vector3 center = obj.transform.position + obj.transform.forward * FrontOffset.Value;
        Quaternion boxRotation = Quaternion.LookRotation(obj.transform.forward);
        
        if(IsVisualized)
            SpawnVisualizer(center, Radius.Value, boxRotation);
        
        Collider[] colliders = Physics.OverlapSphere(center, Radius.Value);
        
        foreach (Collider collider in colliders)
        {
            if (!collider.CompareTag("Enemy"))
                continue;
            
            ExecuteNextConsequence(collider.gameObject).Forget();
        }
    }
    
    public void SpawnVisualizer( Vector3 center, float radius, Quaternion boxRotation)
    {
        SphereOverlapConsequenceVisualizer visualizer = Instantiate(VisualizerPrefab, center, boxRotation);
        visualizer.Initialize(center, radius, boxRotation);
    }
}