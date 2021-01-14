### Links
* [Wikipedia](https://ru.wikipedia.org/wiki/%D0%9E%D0%B4%D0%B8%D0%BD%D0%BE%D1%87%D0%BA%D0%B0_(%D1%88%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD_%D0%BF%D1%80%D0%BE%D0%B5%D0%BA%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F))
* [refguru](https://refactoring.guru/ru/design-patterns/singleton)
---
### Note
1. Обычно везде живет Di и там все реализовано через Singletone конкретного контейнера. Это как бы не совсем синглтон, но как бы и он) разница хорошо описана тут 8.3.1 Singleton - Mark Seemann "Dependecy injection in .NET"
1. Классический потокобезовасный синглтон `ClassicThreadSave` описан у Рихтера, и почему мы должны его имплементировать именно так. 4 издание, глава "Блокировка с двойной проверкой"(стр 875)
1. в 9 шарпе (`Sharp9_ModuleInitializer`) появилась [эта штука](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/proposals/csharp-9.0/module-initializers), как вариант... но еще особо не тестил 