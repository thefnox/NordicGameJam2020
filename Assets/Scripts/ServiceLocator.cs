using System.Collections.Generic;

public static class ServiceLocator
{
    private static readonly Dictionary<System.Type, object>
        Services = new Dictionary<System.Type, object>();

    public static void Register<T>(object serviceInstance)
    {
        Services[typeof(T)] = serviceInstance;
    }

    public static T Resolve<T>()
    {
        return (T)Services[typeof(T)];
    }

    public static void Reset()
    {
        Services.Clear();
    }
}