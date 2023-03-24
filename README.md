# LeoEcsLite Entity Converter
*Read this in other languages: [Русский](https://github.com/AndreyBirchenko/LeoEcsLiteEntityConverter/blob/master/README.md), [English](https://github.com/AndreyBirchenko/LeoEcsLiteEntityConverter/blob/master/README_en.md)*

Обеспечивает автоматическое создание сущностей с компонентами через инспектор.

> Проверено на Unity 2020.3 и содержит asmdef-описания для компиляции в виде отдельных сборок и уменьшения времени рекомпиляции основного проекта.

Для обратной связи https://t.me/AndreyBirchenko

# Установка
**Внимание!** Для работы этого расширения сначала установите [LeoEcsLite](https://github.com/Leopotam/ecslite)

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

# Конвертация по ходу исполнения программы
Вы можете конвертировать объекты при создании
```c#
    var objectView = EcsConverter.InstantiateAndCreateEntity(ObjectPrefab, _ecsWorld);
```
Для этого на префабе который вы собираетесь конвертировать должен быть `ComponentsContainer`
#
