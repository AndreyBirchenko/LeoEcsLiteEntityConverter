using UnityEngine;

namespace AB_Utility.FromSceneToEntityConverter
{
    public class ComponentsContainer : MonoBehaviour
    {
        [SerializeField] private BaseConverter[] _converters;
        [SerializeField] private bool _destroyAfterConversion;
        public BaseConverter[] Converters => _converters;
        public bool DestroyAfterConversion => _destroyAfterConversion;

        private void OnValidate()
        {
            _converters = GetComponents<BaseConverter>();
        }
    }
}