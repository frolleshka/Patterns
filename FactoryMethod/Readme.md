### Links
* [Wikipedia](https://ru.wikipedia.org/wiki/%D0%A4%D0%B0%D0%B1%D1%80%D0%B8%D1%87%D0%BD%D1%8B%D0%B9_%D0%BC%D0%B5%D1%82%D0%BE%D0%B4_(%D1%88%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD_%D0%BF%D1%80%D0%BE%D0%B5%D0%BA%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F))
* [refguru](https://refactoring.guru/ru/design-patterns/abstract-factory)
---
### Note
1. В примере с DI реализован разный резолв, возможно есть еще варианты. Так же в каждом TransportRunner ао разному создается конкретный транспорт.
1. Разновидностью так же является замена коструктора статическим методом возвращающем экземпляр. Можно юзать когда в классе полно конструкторов с разными параметрами, или когда необходимо сделать создание экземпляра "понятным", или провалидировать значения и тд...
```c#
// Пример
public class OperationResult<T>
{
    public bool IsError => !string.IsNullOrEmpty(Message);

    public string Message { get; init; }

    public T Result { get; init; }

    public static OperationResult<T> CreateSucces(T result)
    {
        return new OperationResult<T> { Result = result };
    }

    public static OperationResult<T> CreateError(string errorMessage)
    {
        return new OperationResult<T> { Message = errorMessage};
    }

    public static OperationResult<T> CreateDefault()
    {
        return new OperationResult<T> { Result = default };
    }
}
```