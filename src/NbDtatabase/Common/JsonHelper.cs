using System;
using System.Text;
using Newtonsoft.Json;

namespace NbDtatabase.Common
{
    public class MyJsonHelper
    {
        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Serialize(Object value)
        {
            string result = JsonConvert.SerializeObject(value);
            //var jsonSerializerSettings = new JsonSerializerSettings() {StringEscapeHandling = StringEscapeHandling.EscapeNonAscii};
            //var result = JsonConvert.SerializeObject(value, jsonSerializerSettings);
            return result;
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        /// <summary>
        /// 序列化文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="value"></param>
        /// <param name="encoding">如果为空，默认Encoding.UTF8</param>
        public static void SerializeFile(string filePath, Object value, Encoding encoding = null)
        {
            var text = Serialize(value);
            string message;
            MyIOHelper.TrySaveFile(filePath, text, encoding, out message);
        }

        /// <summary>
        /// 反序列化文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T DeserializeFile<T>(string filePath)
        {
            var readAllText = MyIOHelper.ReadAllText(filePath);
            var result = Deserialize<T>(readAllText);
            return result;
        }
    }
}
