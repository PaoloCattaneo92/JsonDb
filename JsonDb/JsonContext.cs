using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonDb;

public class JsonContext
{
    protected string DataFolder { get; set; }

    public JsonContext(string dataFolder)
    {
        DataFolder = dataFolder;
        if(!Directory.Exists(DataFolder))
        {
            Directory.CreateDirectory(DataFolder);
        }
    }

    public static string NameOfFile<V>() => typeof(V).Name.ToLower() + ".json";
    public string FullNameOfFile<V>() => Path.Combine(DataFolder, NameOfFile<V>());

    public IEnumerable<V> Get<V>()
    {
        var file = Path.Combine(DataFolder, NameOfFile<V>());
        if (!File.Exists(file))
        {
            File.Create(file).Close();
            return Enumerable.Empty<V>();
        }

        return JsonConvert.DeserializeObject<IEnumerable<V>>(File.ReadAllText(file))
            ?? Enumerable.Empty<V>();
    }

    public void InsertUpdateList<V>(IEnumerable<V> newValues)
    {
        var props = typeof(V).GetProperties();
        var keyProperty = props.FirstOrDefault(p => p.GetCustomAttributes(typeof(JsonKey), false).Any());
        if (keyProperty == null)
        {
            keyProperty = props.FirstOrDefault(p => p.Name == "Id");
        }

        if (keyProperty == null)
        {
            throw new JsonException($"Cannot identify key from type {nameof(V)}, use attribute {nameof(JsonKey)}");
        }

        var values = Get<V>().ToList();
        foreach(var value in newValues)
        {
            var found = values.FirstOrDefault(v => keyProperty.GetValue(v)?.Equals(keyProperty.GetValue(value)) ?? false);
            if (found != null)
            {
                found = value;
            }
            else
            {
                values.Add(value);
            }
        }

        ApplyChanges(values);
    }

    public bool Delete<V>(object key)
    {
        var props = typeof(V).GetProperties();
        var keyProperty = props.FirstOrDefault(p => p.GetCustomAttributes(typeof(JsonKey), false).Any());
        if (keyProperty == null)
        {
            keyProperty = props.FirstOrDefault(p => p.Name == "Id");
        }

        if (keyProperty == null)
        {
            throw new JsonException($"Cannot identify key from type {nameof(V)}, use attribute {nameof(JsonKey)}");
        }

        var values = Get<V>();
        var found = values.FirstOrDefault(v => keyProperty.GetValue(v)?.Equals(key) ?? false);
        if (found == null)
        {
            return false;
        }

        var newValues = values.ToList();
        var result = newValues.Remove(found);
        ApplyChanges(newValues);
        return result;
    }

    public void InsertUpdate<V>(V value)
    {
        var props = typeof(V).GetProperties();
        var keyProperty = props.FirstOrDefault(p => p.GetCustomAttributes(typeof(JsonKey), false).Any());
        if (keyProperty == null)
        {
            keyProperty = props.FirstOrDefault(p => p.Name == "Id");
        }

        if(keyProperty == null)
        {
            throw new JsonException($"Cannot identify key from type {nameof(V)}, use attribute {nameof(JsonKey)}");
        }

        var values = Get<V>();
        var found = values.FirstOrDefault(v => keyProperty.GetValue(v)?.Equals(keyProperty.GetValue(value)) ?? false);
        if (found != null)
        {
            found = value;
            ApplyChanges(values);
            return;
        }

        var newList = values.ToList();
        newList.Add(value);
        ApplyChanges(newList);
    }

    public void Drop<V>()
    {
        var file = FullNameOfFile<V>();
        if (File.Exists(file))
        {
            File.Delete(file);
        }
    }

    protected void ApplyChanges<V>(IEnumerable<V> values)
    {
        var file = FullNameOfFile<V>();
        if (!File.Exists(file))
        {
            File.Create(file).Close();
        }

        File.WriteAllText(file, JsonConvert.SerializeObject(values, Formatting.Indented));
    }
}
