using UnityEngine;


public class SpawnHolder : MonoBehaviour
{
   public void RemoveSpawnedObjects()
    {
        DestroyEffect[] destroyEffects = GetComponentsInChildren<DestroyEffect>();

        destroyEffects.ForEach(t => t.Effect());
    }

}
