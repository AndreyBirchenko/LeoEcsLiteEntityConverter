# LeoEcsLite Entity Converter
*Read this in other languages: [Русский](https://github.com/AndreyBirchenko/LeoEcsLiteEntityConverter/blob/master/README.md), [English](https://github.com/AndreyBirchenko/LeoEcsLiteEntityConverter/blob/master/README_en.md)*

Provides automatic creation of entities with components through the inspector.

> Tested on Unity 2020.3 and contains asmdef descriptions for compilation in the form of separate assemblies and reducing the recompilation time of the main project.
> 
> For feedback https://t.me/AndreyBirchenko

# Installation
**Attention!** For this extension to work, first install [LeoEcsLite](https://github.com/Leopotam/ecslite)

## As a unity module
Installation as a unity module via a git link in PackageManager or direct editing of `Packages/manifest.json`:
```
"com.anbi.leoecslite.entityconverter": "https://github.com/AndreyBirchenko/LeoEcsLiteEntityConverter.git",
```

# Integration
```c#
var systems = new EcsSystems (new EcsWorld ());
systems
    .Add (new System1 ())
    .AddWorld (new EcsWorld (), "events")
    // ...
    .ConvertScene()
    .Init ();
```

# Usage

To add a component to a game object, first create a converter class and inherit it from `ComponentConverter<T>`

**IMPORTANT:** The component must have the attribute `[Serializable]` if you want its fields to be displayed in the inspector.
```c#
    [Serializable]
    public struct TestComponent
    {
        public int Value;
    }
    
    public class TestComponentConverter : ComponentConverter<TestComponent> 
    {
    }
```

After that, you can add the component to the game object via the **AddComponent button**

![alt text](https://github.com/AndreyBirchenko/LeoEcsLiteEntityConverter/blob/master/img/1.jpg)

Along with your converter, a `ComponentsContainer` will be added automatically. It is needed for conversion.

If you want all unnecessary components to be removed from the game object after conversion, check the box `DestroyAfterConversion`

Now when you start the project, an entity with the `TestComponent` component and the `Value` value from the inspector will be automatically created

![alt text](https://github.com/AndreyBirchenko/LeoEcsLiteEntityConverter/blob/master/img/2.jpg)
# Conversion during the execution of the program
You can convert objects when instantiating
```c#
    var object View = Epsconverter.Instantiate And Create Entity(Object Prefab, _eco World);
```
To do this, the prefab that you are going to convert must have a `ComponentsContainer`
#
