using System;
using System.Collections.Generic;

using Leopotam.EcsLite;

using UnityEngine;

namespace AB_Utility.FromSceneToEntityConverter
{
    public static class Extensions
    {
        private static HashSet<EcsWorld> _dirtyWorlds = new HashSet<EcsWorld>();
        public static IEcsSystems ConvertScene(this IEcsSystems systems)
        {
            var world = systems.GetWorld();
            if (_dirtyWorlds.Contains(world))
            {
               throw new Exception("You cannot convert systems with the same world twice <a href=\"https://safe-clove-478.notion.site/You-cannot-convert-systems-with-the-same-world-twice-6f4c638c18e74b0b91c65cd0c7f9dc7b\">more info</a>" );
            }

            _dirtyWorlds.Add(world);
            var containers = UnityEngine.Object.FindObjectsOfType<ComponentsContainer>(true);

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