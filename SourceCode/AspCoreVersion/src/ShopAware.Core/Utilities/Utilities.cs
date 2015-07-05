using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using ShopAware.Core.Attributes;
using ShopAware.Core.DataObjects;

namespace ShopAware.Core
{
    public sealed class Utilities
    {
        #region Public Methods

        /// <summary>
        ///     Fixes String values
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string AddBackslash(string str)
        {
            if (str != null && !str.EndsWith("\\"))
            {
                str += "\\";
            }

            return str;
        }

        /// <summary>
        ///     Fixes String values
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string AddForwardSlash(string str)
        {
            if (str != null && !str.EndsWith("/"))
            {
                str += "/";
            }

            return str;
        }

        /// <summary>
        ///     Converts a string date to a Date Object
        /// </summary>
        /// <param name="dateString">21050102</param>
        /// <param name="dateFormat">yyyyMMdd</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DateTime ConvertDateStringToDate(string dateString, string dateFormat)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return default(DateTime);
            }

            var result = default(DateTime);

            switch (dateFormat)
            {
                case "yyyyMMdd":
                    result = DateTime.ParseExact(dateString, "yyyyMMdd", CultureInfo.CurrentCulture);
                    break;
            }

            return result;
        }

        public static string GetEnumAttribute<T>(object value)
        {
            var fieldInfo = value.GetType().
                                  GetField(value.ToString());
            var attributes = fieldInfo.CustomAttributes;
            var attribute = attributes.FirstOrDefault(c => c.AttributeType == typeof (T));

            if (attribute != null && attribute.ConstructorArguments.Any())
            {
                return (string) attribute.ConstructorArguments[0].Value;
            }

            return Enum.GetName(value.GetType(), value);
        }

        public static Coordinates GetEnumCoordinates(object value)
        {
            var fieldInfo = value.GetType().
                                  GetField(value.ToString());
            var attributes = fieldInfo.CustomAttributes;
            var attribute = attributes.FirstOrDefault(c => c.AttributeType == typeof (CoordinatesAttribute));

            if (attribute != null && attribute.ConstructorArguments.Any())
            {
                return new Coordinates()
                       {
                           Latitude = (double) attribute.ConstructorArguments[0].Value,
                           Longitude = (double) attribute.ConstructorArguments[0].Value
                       };
            }

            return new Coordinates();
        }

        public static string GetEnumDefaultValue(object value)
        {
            return GetEnumAttribute<DefaultValueAttribute>(value);
        }

        public static string GetEnumDescription(object value)
        {
            return GetEnumAttribute<DescriptionAttribute>(value);
        }

        public static object GetJTokenObject(JToken item, string name)
        {
            return item.Value<object>(name) ?? null;
        }

        public static string GetJTokenString(JToken item, string name)
        {
            return item.Value<string>(name) ?? string.Empty;
        }

        /// <summary>
        ///     Checks for token validity
        /// </summary>
        /// <param name="jToken"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool IsJTokenValid(JToken jToken)
        {
            var result = false;

            if (jToken != null)
            {
                if (!string.IsNullOrEmpty(jToken.ToString()))
                {
                    result = true;
                }
            }

            return result;
        }

        #endregion
    }
}