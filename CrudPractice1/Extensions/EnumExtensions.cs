using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CrudPractice1.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetField(enumValue.ToString())?
                            .GetCustomAttribute<DisplayAttribute>()?
                            .Name ?? enumValue.ToString();
        }
    }
}
