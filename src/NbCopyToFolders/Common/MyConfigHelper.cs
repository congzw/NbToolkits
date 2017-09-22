using System;
using System.Configuration;
using System.Xml;

namespace NbCopyToFolders.Common
{
    /// <summary>
    /// 配置帮助类
    /// </summary>
    public interface IMyConfigHelper
    {
        /// <summary>
        /// 读取配置（AppSetting）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        string GetAppSettingValue(string key, string defaultValue);

        /// <summary>
        /// 读取配置（AppSetting）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        bool GetAppSettingValueAsBool(string key, bool defaultValue);

        /// <summary>
        /// 转换成T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        T GetAppSettingValueAs<T>(string key, T defaultValue);
    }

    /// <summary>
    /// 配置帮助类
    /// </summary>
    public class MyConfigHelper : IMyConfigHelper
    {
        /// <summary>
        /// 读取配置（AppSetting）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        string IMyConfigHelper.GetAppSettingValue(string key, string defaultValue)
        {
            string result = defaultValue;
            //如果后台有设置，以config的设置为准
            string settingValue = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrWhiteSpace(settingValue))
            {
                result = settingValue;
            }
            return result;
        }

        /// <summary>
        /// 读取配置（AppSetting）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        bool IMyConfigHelper.GetAppSettingValueAsBool(string key, bool defaultValue)
        {
            bool result = defaultValue;
            //如果后台有设置，以config的设置为准
            string settingValue = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrWhiteSpace(settingValue))
            {
                bool.TryParse(settingValue, out result);
            }
            return result;
        }

        /// <summary>
        /// 转换成T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        T IMyConfigHelper.GetAppSettingValueAs<T>(string key, T defaultValue)
        {
            T result = defaultValue;
            //如果后台有设置，以config的设置为准
            string settingValue = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrWhiteSpace(settingValue))
            {
                result = MyConvert<T>(settingValue);
            }
            return result;
        }

        //helpers
        private static T MyConvert<T>(object data)
        {
            return (T)Convert.ChangeType(data, typeof(T));
        }

        #region For Ioc Extensions

        /// <summary>
        /// 当前的实例
        /// </summary>
        public static IMyConfigHelper Resolve()
        {
            return defaultFactoryFunc.Invoke();
        }
        /// <summary>
        /// 重新设置工厂方法
        /// </summary>
        /// <param name="func"></param>
        public static void SetFactoryFunc(Func<IMyConfigHelper> func)
        {
            if (func != null)
            {
                defaultFactoryFunc = func;
            }
        }

        private static Lazy<MyConfigHelper> lazyInstance = new Lazy<MyConfigHelper>(() => new MyConfigHelper());

        private static Func<IMyConfigHelper> defaultFactoryFunc = () => lazyInstance.Value;

        #endregion

        #region static method only keep for old versions

        /// <summary>
        /// 读取配置（AppSetting）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetAppSettingValue(string key, string defaultValue)
        {
            return Resolve().GetAppSettingValue(key, defaultValue);
        }

        /// <summary>
        /// 读取配置（AppSetting）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetAppSettingValueAsBool(string key, bool defaultValue)
        {
            return Resolve().GetAppSettingValueAsBool(key, defaultValue);
        }

        /// <summary>
        /// 转换成T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetAppSettingValueAs<T>(string key, T defaultValue)
        {
            return Resolve().GetAppSettingValueAs(key, defaultValue);
        }

        #endregion
    }
}
