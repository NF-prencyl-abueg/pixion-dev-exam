using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rect Overlap Consequence", menuName = "ScriptableObjects/Ability/Rect Overlap Consequence")]
public class RectOverlapConsequence : Consequence
{
    public Stat Width;
    public Stat Depth;
    public Stat Height;
    public Stat FrontOffset; 
        
    public bool IsVisualized = false;
    [ShowIf("IsVisualized")]
    public RectOverlapConsequenceVisualizer VisualizerPrefab;
    
    
    public override async UniTask ExecuteConsequence(GameObject obj)
    {
        Vector3 center = obj.transform.position + obj.transform.forward * FrontOffset.Value;
        Vector3 halfExtents = Utility.CalculateHalfExtents(Width.Value, Height.Value, Depth.Value);
        Quaternion boxRotation = Quaternion.LookRotation(obj.transform.forward);
        
        Collider[] colliders = Physics.OverlapBox(center, halfExtents, boxRotation);

        if(IsVisualized)
            SpawnVisualizer(center, halfExtents, boxRotation);
        
        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.name);
            if (!collider.CompareTag("Enemy"))
                continue;
            
            ExecuteNextConsequence(collider.gameObject).Forget();
        }
    }

    public void SpawnVisualizer( Vector3 center, Vector3 halfExtents, Quaternion boxRotation)
    {
        RectOverlapConsequenceVisualizer visualizer = Instantiate(VisualizerPrefab, center, boxRotation);
        visualizer.Initialize(center, halfExtents, boxRotation);
    }
}