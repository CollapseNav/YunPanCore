using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Util
{
    public class LayuiData
    {
        public static string LayuiTableData<T>(List<T> items)
        {
            StringBuilder strB = new StringBuilder();
            strB.Append("{\"code\":\"0\",\"msg\":\"\",\"count\":\"");
            strB.Append(items.Count.ToString());
            strB.Append("\",\"data\":");
            strB.Append(JsonConvert.SerializeObject(items));
            strB.Append("}");
            return strB.ToString();
        }

        public static string SerializeData<T>(T item)
        {
            return JsonConvert.SerializeObject(item); ;
        }
    }
}
