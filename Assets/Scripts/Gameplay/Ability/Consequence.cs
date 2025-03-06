using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Consequence : SerializedScriptableObject
{ 
    public Consequence NextConsequence;

    public virtual async UniTask ExecuteConsequence(GameObject obj)
    {
    }

    protected async UniTask ExecuteNextConsequence(GameObject obj)
    {
        if (NextConsequence == null)
            return;
        
        await NextConsequence.ExecuteConsequence(obj);
    }
}