using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rect Overlap Consequence", menuName = "ScriptableObjects/Ability/Rect Overlap Consequence")]
public class RectOverlapConsequence : Consequence
{
    public Stat Width;
    public Stat Depth;
    public Stat Height;
    
    public bool IsVisualized = false;
    [ShowIf("IsVisualized")]
    public RectOverlapConsequenceVisualizer VisualizerPrefab;
    
    
    public override async UniTask ExecuteConsequence(GameObject obj)
    {
        Vector3 center = obj.transform.position;
        Vector3 halfExtents = Utility.CalculateHalfExtents(Width.Value, Height.Value, Depth.Value);
        
        Collider[] colliders = Physics.OverlapBox(center, halfExtents);

        foreach (Collider collider in colliders)
        {
            if (!collider.CompareTag("Enemy"))
                return;
            
            ExecuteNextConsequence(collider.gameObject).Forget();
        }
    }
}