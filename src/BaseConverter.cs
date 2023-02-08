using Leopotam.EcsLite;

using UnityEngine;

namespace AB_Utility.FromSceneToEntityConverter
{
    [RequireComponent(typeof(ComponentsContainer))]
    public abstract class BaseConverter : MonoBehaviour
    {
        public abstract void Convert(EcsPackedEntityWithWorld entityWithWorld);
    }
}