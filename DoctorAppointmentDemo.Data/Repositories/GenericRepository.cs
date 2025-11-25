using DoctorAppointmentDemo.Data.Configuration;
using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace MyDoctorAppointment.Data.Repositories
{
    public abstract class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : Auditable
    {
        public abstract string Path { get; set; }

        public abstract int LastId { get; set; }

        public TSource Create(TSource source)
        {
            source.Id = ++LastId;
            source.CreatedAt = DateTime.Now;

            if (!RepositorySettings.UseXml)
            {
                File.WriteAllText(Path, JsonConvert.SerializeObject(GetAll().Append(source), Formatting.Indented));
                SaveLastId();
            }
            else
            {
                var items = GetAll().ToList();
                items.Add(source);

                CreateDirectory(Path);

                var serializer = new XmlSerializer(typeof(List<TSource>));
                using (var writer = new StreamWriter(Path))
                {
                    serializer.Serialize(writer, items);
                }

                SaveLastId();
            }


            return source;
        }

        public bool Delete(int id)
        {
            if (GetById(id) is null)
                return false;

            if (!RepositorySettings.UseXml)
            {
                File.WriteAllText(Path, JsonConvert.SerializeObject(GetAll().Where(x => x.Id != id), Formatting.Indented));
            }
            else
            {
                var items = GetAll().Where(x => x.Id != id).ToList();

                CreateDirectory(Path);

                var serializer = new XmlSerializer(typeof(List<TSource>));
                using (var writer = new StreamWriter(Path))
                {
                    serializer.Serialize(writer, items);
                }
            }
            return true;
        }

        public IEnumerable<TSource> GetAll()
        {
            if (!File.Exists(Path))
            {
                if (!RepositorySettings.UseXml)
                {
                    File.WriteAllText(Path, "[]");
                }
                else
                {
                    CreateDirectory(Path);

                    var serializer = new XmlSerializer(typeof(List<TSource>));

                    using (var writer = new StreamWriter(Path))
                    {
                        serializer.Serialize(writer, new List<TSource>());
                    }
                }
            }

            if (!RepositorySettings.UseXml)
            {
                var json = File.ReadAllText(Path);

                if (string.IsNullOrWhiteSpace(json))
                {
                    File.WriteAllText(Path, "[]");
                    json = "[]";
                }
                return JsonConvert.DeserializeObject<List<TSource>>(json)!;
            }
            else
            {
                var fileInfo = new FileInfo(Path);
                if (fileInfo.Length == 0)
                {
                    return new List<TSource>();
                }

                var serializer = new XmlSerializer(typeof(List<TSource>));
                using (var stream = File.OpenRead(Path))
                {
                    var deserialized = serializer.Deserialize(stream) as List<TSource>;
                    return deserialized ?? new List<TSource>();
                }
            }
        }

        public TSource? GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public TSource Update(int id, TSource source)
        {
            source.UpdatedAt = DateTime.Now;
            source.Id = id;

            if (!RepositorySettings.UseXml)
            {
                File.WriteAllText(Path, JsonConvert.SerializeObject(GetAll().Select(x => x.Id == id ? source : x), Formatting.Indented));
            }
            else
            {
                var items = GetAll().Select(x => x.Id == id ? source : x).ToList();
                CreateDirectory(Path);
                var serializer = new XmlSerializer(typeof(List<TSource>));
                using (var writer = new StreamWriter(Path))
                {
                    serializer.Serialize(writer, items);
                }
            }

                return source;
        }

        public abstract void ShowInfo(TSource source);

        protected abstract void SaveLastId();

        protected dynamic ReadFromAppSettings()
        {
            var selectedAppSettingsPath = RepositorySettings.UseXml ? Constants.AppSettingsXmlPath : Constants.AppSettingsJsonPath;
            return JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(selectedAppSettingsPath))!;
        }

        protected void CreateDirectory(string filePath)
        {
            var dir = System.IO.Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}
