using System.Collections.Generic;

namespace TradingCardGame.Interface.ServiceInterfaces
{
    public interface IDataService<T>
    {
        /// <summary>
        /// Modeli ilgili json dosyasına update geçer
        /// </summary>
        /// <param name="model"></param>
        void SaveJsomData(List<T> model);
        /// <summary>
        /// İlgili json dosyasını modele atayıp döndürür
        /// </summary>
        /// <returns></returns>
        List<T> GetJsonData();
        /// <summary>
        /// Id ye göre ilgili json dan döndürür
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetJsonDataById(int id);
        /// <summary>
        /// Name ye göre ilgili json dan döndürür
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        T GetJsonDataByName(string name);
    }
}
