using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TradingCardGame.Core.Extensions;
using TradingCardGame.Interface.Models.Base;
using TradingCardGame.Interface.ServiceInterfaces;

namespace TradingCardGame.Business.Services
{
    public class DataService<T> : IDataService<T>
        where T : class, IModelBase
    {
        private static string _jsonFolderName => @"DemoData\";
        private static string _jsonFileName => $@"{_jsonFolderName}{_typeName}.json";
        private static string _jsonFilePath => _jsonFileName.ToApplicationPath();
        private static string _typeName => $"{typeof(T).Name}s";


        public void SaveJsomData(List<T> model)
        {
            var data = GetJsonData();
            if (data != null)
                model.AddRange(data.Where(x => model.All(y => x.Id != y.Id)).ToList());

            model = model.OrderBy(x => x.Id).ToList();

            var newDataJson = JsonConvert.SerializeObject(model);
            File.WriteAllText(_jsonFilePath, newDataJson);
        }

        public List<T> GetJsonData()
        {
            CreateIfNotExists();
            var dataJson = File.ReadAllText(_jsonFilePath);
            return JsonConvert.DeserializeObject<List<T>>(dataJson);
        }

        public T GetJsonDataById(int id)
        {
            CreateIfNotExists();
            var dataJson = File.ReadAllText(_jsonFilePath);
            return JsonConvert.DeserializeObject<List<T>>(dataJson)?.FirstOrDefault(x => x.Id == id);
        }

        public T GetJsonDataByName(string name)
        {
            CreateIfNotExists();
            var dataJson = File.ReadAllText(_jsonFilePath);
            return JsonConvert.DeserializeObject<List<T>>(dataJson)?.FirstOrDefault(x => x.Name == name);
        }

        private void CreateIfNotExists()
        {
            _jsonFilePath.CreateDirectoryIfNotExists();
            _jsonFilePath.CreateFileIfNotExists();

        }
    }
}
