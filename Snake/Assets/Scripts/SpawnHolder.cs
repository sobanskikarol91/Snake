using UnityEngine;


public class SpawnHolder : MonoBehaviour
{
    // Remove all spawned objects on scene,
    public void RemoveSpawnedObjects()
    {
        DestroyEffect[] destroyEffects = GetComponentsInChildren<DestroyEffect>();

        destroyEffects.ForEach(t => t.Effect());
    }
}
