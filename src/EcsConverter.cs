using System;

using Leopotam.EcsLite;

using UnityEngine;

using Object = UnityEngine.Object;

namespace AB_Utility.FromSceneToEntityConverter
{
    public static class EcsConverter
    {
        public static T InstantiateAndCreateEntity<T>
        (T original, Vector3 position, Quaternion rotation, Transform parent,
            EcsWorld world)
            where T : UnityEngine.Object
        {
            var obj = Object.Instantiate(original, position, rotation, parent);
            ConvertObject(obj as GameObject, world);
            return obj;
        }

        public static T InstantiateAndCreateEntity<T>
            (T original, Vector3 position, Quaternion rotation, EcsWorld world)
            where T : UnityEngine.Object
        {
            var obj = Object.Instantiate(original, position, rotation);
            ConvertObject(obj as GameObject, world);
            return obj;
        }

        public static T InstantiateAndCreateEntity<T>
            (T original, Transform parent, EcsWorld world, bool worldPositionStay)
            where T : UnityEngine.Object
        {
            var obj = Object.Instantiate(original, parent, worldPositionStay);
            ConvertObject(obj as GameObject, world);
            return obj;
        }

        public static T InstantiateAndCreateEntity<T>
            (T original, Transform parent, EcsWorld world)
            where T : UnityEngine.Object
        {
            var obj = Object.Instantiate(original, parent);
            ConvertObject(obj as GameObject, world);
            return obj;
        }

        public static T InstantiateAndCreateEntity<T>
            (T original, EcsWorld world)
            where T : UnityEngine.Object
        {
            var obj = Object.Instantiate(original);
            ConvertObject(obj as GameObject, world);
            return obj;
        }

        internal static void ConvertContainer(ComponentsContainer container, EcsWorld world)
        {
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

        private static void ConvertObject(GameObject obj, EcsWorld world)
        {
            var container = obj.GetComponent<ComponentsContainer>();
#if DEBUG
            if (!container)
            {
                throw new Exception("You can not instantiate and create entity without components container");
            }
#endif

            ConvertContainer(container, world);
        }
    }
}