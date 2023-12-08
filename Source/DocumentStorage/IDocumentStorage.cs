namespace DocumentStorage;

using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

public interface IDocumentStorage
{
    void RegisterConverter(params JsonConverter[] jsonConverters);

    Task<T?> ReadAsync<T>(string file);

    Task WriteAsync<T>(T? data, string file);

    Task UpdateAsync<T>(string file, Action<T> updateAction)
        where T : new();
}