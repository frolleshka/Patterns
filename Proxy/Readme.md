﻿### Links
* [Wikipedia](https://ru.wikipedia.org/wiki/%D0%97%D0%B0%D0%BC%D0%B5%D1%81%D1%82%D0%B8%D1%82%D0%B5%D0%BB%D1%8C_(%D1%88%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD_%D0%BF%D1%80%D0%BE%D0%B5%D0%BA%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F))
* [refguru](https://refactoring.guru/ru/design-patterns/proxy)
---
### Note
Паттер распростронен, но только в разных вариациях, и в основном интуитивно имплементирован) Есть несколько видов заместителя, на refactoring.guru подробно все описано. Иногда его не используется и класс который нужно проксировать обрастает всякой лишней логикой, которая раздувает его ответственнность.

Если инициализация объекта занимает много времени, то лучше делать через Lazy, чтобы не перегружать этой работой конструктор.

В примере с DI намеренно не сделал фабрику для создания Worker. Не хотел использовать др паттерны. Вариант с контейнером нужных зависимостей по мне более гибкий, тем более когда их больше 3-5, и тем более когда нужно переиспользовать один набор зависимостей в некоторых объектах.

Есть оч тонкая грань между заместителем и декоратаром. Осн разница в том что жизнь объекта который проксируется контролирует именно прокси, а в декораторе эта ответственность клиента.