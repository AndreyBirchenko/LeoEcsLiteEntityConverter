# LeoEcsLite Entity Converter
Обеспечивает автоматическое создание сущностей с компонентами через инспектор.

> Проверено на Unity 2020.3 (не зависит от Unity) и содержит asmdef-описания для компиляции в виде отдельных сборок и уменьшения времени рекомпиляции основного проекта.

# Установка

## В виде unity модуля
Поддерживается установка в виде unity-модуля через git-ссылку в PackageManager или прямое редактирование `Packages/manifest.json`:
```
"com.anbi.leoecslite.entityconverter": "https://github.com/AndreyBirchenko/LeoEcsLiteEntityConverter.git",
```

# Интеграция
```c#
var systems = new EcsSystems (new EcsWorld ());
systems
    .Add (new System1 ())
    .AddWorld (new EcsWorld (), "events")
    // ...
    .ConvertScene()
    .Init ();
```

# Использование

Чтобы добавить компонент на гейм объект сначала создайте класс конвертер и унаследуйте его от `ComponentConverter<T>`

**ВАЖНО:** У компонента должен быть атрибут `[Serializable]` если вы хотите чтобы его поля отображались в инспекторе.
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

После этого вы сможете добавить компонент на гейм объект через кнопку **AddComponent**

![alt text](https://github.com/AndreyBirchenko/LeoEcsLiteEntityConverter/blob/master/img/1.jpg)

Вместе с вашим конвертером автоматически добавится ещё и `ComponentsContainer` он нужен для конвертации.

Если вы хотите чтобы после конвертации все ненужные компоненты удалились с гейм объекта поставьте галочку `DestroyAfterConversion`

Теперь когда вы запустите проект будет автоматически создана сущность с компонентом `TestComponent` и значением `Value` из инспектора

![alt text](https://github.com/AndreyBirchenko/LeoEcsLiteEntityConverter/blob/master/img/2.jpg)

#
