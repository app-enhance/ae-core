namespace AE.Core.Serialization
{
    using System;

    public interface ISerializeService
    {
        string Serialize(object @object);

        T Deserialize<T>(string @object);

        object Deserialize(string @object, Type type);
    }
}