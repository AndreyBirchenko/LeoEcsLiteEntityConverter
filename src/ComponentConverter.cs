using System;

using Leopotam.EcsLite;

using UnityEngine;

namespace AB_Utility.FromSceneToEntityConverter
{
    [DisallowMultipleComponent]
    public class ComponentConverter<T> : BaseConverter where T : struct
    {
        [SerializeField] protected T _component;

        public override void Convert(EcsPackedEntityWithWorld entityWithWorld)
        {
            if (entityWithWorld.Unpack(out var world, out var entity))
            {
                ref var component = ref world.GetPool<T>().Add(entity);
                component = _component;
            }
        }

        private void Reset()
        {
            var container = GetComponent<ComponentsContainer>();
            container.GetConverters();
        }
    }
}