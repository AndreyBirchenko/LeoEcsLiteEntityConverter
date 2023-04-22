using System;
using System.IO;
using System.Reflection;

using AB_Utility.LeoEcsLiteEntityConverter.Generator.Runtime;

using UnityEditor;

using UnityEngine;

namespace AB_Utility.FromSceneToEntityConverter.Editor
{
    public class Generator : ScriptableObject
    {
        private const string _converterTemplate = "ConverterTemplate.txt";
        private static string _convertersPath = Application.dataPath + "/TestScripts/Converters";

        [InitializeOnLoadMethod]
        public static void Generate()
        {
            var components = TypeCache.GetTypesWithAttribute<GenerateConverterAttribute>();
            var alreadyGeneratedConverters = TypeCache.GetTypesWithAttribute<ComponentConverterAttribute>();

            foreach (var component in components)
            {
                if (CanGenerateConverter(component, alreadyGeneratedConverters))
                {
                    GenerateConverter(component);
                }
            }
        }

        private static bool CanGenerateConverter(Type componentType,
            TypeCache.TypeCollection alreadyGeneratedConverters)
        {
            foreach (var converter in alreadyGeneratedConverters)
            {
                var attributes = converter.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    if (attribute is ComponentConverterAttribute converterAttribute)
                    {
                        var converterType = converterAttribute.ComponentType;
                        if (converterType == componentType)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private static void GenerateConverter(Type componentType)
        {
            var converterCode = GetTemplateContent(_converterTemplate);
            converterCode = converterCode.Replace("#COMPONENT_NAMESPACE#", componentType.Namespace);
            converterCode = converterCode.Replace("#COMPONENT_TYPE#", componentType.Name);
            File.WriteAllText($"{GetSavePath()}/{componentType.Name}Converter.cs", converterCode);
        }

        private static string GetTemplateContent(string proto)
        {
            var pathHelper = CreateInstance<Generator>();
            var path = Path.GetDirectoryName(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(pathHelper)));
            DestroyImmediate(pathHelper);
            try
            {
                return File.ReadAllText(Path.Combine(path ?? "", proto));
            }
            catch
            {
                return null;
            }
        }

        private static string GetSavePath()
        {
            if (Directory.Exists(_convertersPath) == false)
            {
                Directory.CreateDirectory(_convertersPath);
            }

            return _convertersPath;
        }
    }
}