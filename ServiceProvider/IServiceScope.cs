using System;

public interface IServiceScope
{
    bool TryGetService<T>(out T service);
    bool TryGetService(Type type, out object service);
}