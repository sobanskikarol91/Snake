using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using  System.Linq;

public class SpawnHolder : MonoBehaviour
{
   public void RemoveSpawnedObjects()
    {
        DestroyEffect[] destroyEffects = GetComponentsInChildren<DestroyEffect>();

        destroyEffects.ForEach(t => t.Effect());
    }

}
