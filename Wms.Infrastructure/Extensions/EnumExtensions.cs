using static Wms.Infrastructure.Enums;

namespace Wms.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static int Length(Type enumType)
        {
            return Enum.GetValues(enumType).Length;
        }

        public static List<string> Names(Type enumType)
        {
            return Enum.GetNames(enumType).ToList();
        }

        // unfortunatelly cant use true extensions mechanism on Enums
        //public static int Length(this Enum value)
        //{
        //    return Enum.GetValues(value.GetType()).Length;
        //}

        //public static List<string> Names(this Enum value)
        //{
        //    return Enum.GetNames(typeof(RoleType)).ToList();
        //}


    }

}
