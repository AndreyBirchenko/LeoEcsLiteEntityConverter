using System;

using Leopotam.EcsLite;

namespace AB_Utility.FromSceneToEntityConverter
{
    public static class Extensions
    {
        public static IEcsSystems ConvertScene(this IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var containers = UnityEngine.Object.FindObjectsOfType<ComponentsContainer>();

            for (int i = 0; i < containers.Length; i++)
            {
                var container = containers[i];
                var destroyAfterConversion = container.DestroyAfterConversion;
                var entity = world.NewEntity();
                var packedEntity = world.PackEntityWithWorld(entity);

                for (int j = 0; j < container.Converters.Length; j++)
                {
                    var converter = container.Converters[j];
                    converter.Convert(packedEntity);
                    
                    if (destroyAfterConversion)
                    {
                        UnityEngine.Object.Destroy(converter);
                    }
                }

                if (destroyAfterConversion)
                {
                    UnityEngine.Object.Destroy(container);
                }
            }

            return systems;
        }
    }
}