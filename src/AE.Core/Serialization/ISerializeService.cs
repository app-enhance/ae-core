namespace AE.Core.Serialization
{
    using System;
    using System.Threading.Tasks;

    public interface ISerializeService
    {
        Task<string> Serialize(object @object);

        Task<T> Deserialize<T>(string @object);

        Task<object> Deserialize(string @object, Type type);
    }
}