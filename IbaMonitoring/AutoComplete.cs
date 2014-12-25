using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using safnet.iba;
using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;

namespace IbaMonitoring
{
    /// <summary>
    /// Holds methods used by auto-complete event handlers.
    /// </summary>
    public static class AutoComplete
    {
        /// <summary>
        /// Provides alpha code and common name of all available species
        /// </summary>
        /// <param name="prefixText">Restricts results to those that begin with this value</param>
        /// <param name="count">Maximum number of results to return</param>
        /// <param name="contextKey">Key to provide information on the page context</param>
        /// <returns>String array</returns>
        [WebMethod]
        [ScriptMethod]
        public static string[] GetSpeciesList(string prefixText, int count, string contextKey)
        {
            List<string> arrayList = new List<string>();
            foreach (Species s in GlobalMap.GetInstance().SpeciesList)
            {
                arrayList.Add(s.AlphaCode + " (" + s.CommonName + ")");
            }
            return arrayList.ToArray<string>();
        }


        [WebMethod]
        [ScriptMethod]
        public static string[] GetSpeciesList(string prefixText, int count)
        {
            IEnumerable<Species> list =
                SpeciesMapper.SelectAll()
                    .Where(x => x.AlphaCode.StartsWith(prefixText))
                    .OrderBy(x => x.AlphaCode)
                    .Take(count);
            List<string> arrayList = new List<string>();
            foreach (Species s in list)
            {
                arrayList.Add(s.AlphaCode + " (" + s.CommonName + ")");
            }
            return arrayList.ToArray<string>();
        }
    }
}