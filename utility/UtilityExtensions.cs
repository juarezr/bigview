using System;
using System.Reflection;

namespace bigview
{
    public static class UtilityExtensions
    {
        #region Is<Type>

        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsNullable(this object value)
        {
            if (value != null)
            {
                var type = value.GetType();
                return IsNullable(type);
            }
            return false;
        }

        public static bool IsAnonymousType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            // HACK: The only way to detect anonymous types right now.
            bool res = Attribute.IsDefined(type, typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), false);
            res = res && (type.IsGenericType && type.Name.Contains("AnonymousType"));
            res = res && (type.Name.StartsWith("<>", StringComparison.InvariantCulture) || type.Name.StartsWith("VB$", StringComparison.InvariantCulture));
            res = res && ((type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic);
            return res;
        }

        public static bool IsNumeric(this object value)
        {
            if (value != null)
            {
                var type = value.GetType();
                return IsNumeric(type);
            }
            return false;
        }

        public static bool IsNumeric(this Type type)
        {
            TypeCode typeCode = Type.GetTypeCode(type);

            switch (typeCode)
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsInteger(this object value)
        {
            if (value != null)
            {
                var type = value.GetType();
                return IsInteger(type);
            }
            return false;
        }

        public static bool IsBoolean(this object value)
        {
            var valueType = value.GetType();
            TypeCode typeCode = Type.GetTypeCode(valueType);

            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsInteger(this Type type)
        {
            TypeCode typeCode = Type.GetTypeCode(type);

            switch (typeCode)
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsFloatingPoint(this object value)
        {
            if (value != null)
            {
                var type = value.GetType();
                return IsFloatingPoint(type);
            }
            return false;
        }

        public static bool IsFloatingPoint(this Type type)
        {
            TypeCode typeCode = Type.GetTypeCode(type);

            switch (typeCode)
            {
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsClass(this object value)
        {
            if (value == null)
                if (value.GetType().IsClass)
                    return true;
            return false;
        }

        public static bool IsSubclassOf<T>(this object value)
        {
            if (value == null)
            {
                var vt = value.GetType();
                if (vt.IsClass)
                    if (vt.IsSubclassOf(typeof(T)))
                        return true;
            }
            return false;
        }

        public static Type GetTypeFromSimpleName(string typeName)
        {
            if (typeName == null)
                throw new ArgumentNullException("typeName");

            bool isArray = false, isNullable = false;

            if (typeName.IndexOf("[]") != -1)
            {
                isArray = true;
                typeName = typeName.Remove(typeName.IndexOf("[]"), 2);
            }

            if (typeName.IndexOf("?") != -1)
            {
                isNullable = true;
                typeName = typeName.Remove(typeName.IndexOf("?"), 1);
            }

            typeName = typeName.ToLower();

            string parsedTypeName = null;
            switch (typeName)
            {
                case "bool":
                case "boolean":
                    parsedTypeName = "System.Boolean";
                    break;
                case "byte":
                    parsedTypeName = "System.Byte";
                    break;
                case "char":
                    parsedTypeName = "System.Char";
                    break;
                case "datetime":
                    parsedTypeName = "System.DateTime";
                    break;
                case "datetimeoffset":
                    parsedTypeName = "System.DateTimeOffset";
                    break;
                case "decimal":
                    parsedTypeName = "System.Decimal";
                    break;
                case "double":
                    parsedTypeName = "System.Double";
                    break;
                case "float":
                    parsedTypeName = "System.Single";
                    break;
                case "int16":
                case "short":
                    parsedTypeName = "System.Int16";
                    break;
                case "int32":
                case "int":
                    parsedTypeName = "System.Int32";
                    break;
                case "int64":
                case "long":
                    parsedTypeName = "System.Int64";
                    break;
                case "object":
                    parsedTypeName = "System.Object";
                    break;
                case "sbyte":
                    parsedTypeName = "System.SByte";
                    break;
                case "string":
                    parsedTypeName = "System.String";
                    break;
                case "timespan":
                    parsedTypeName = "System.TimeSpan";
                    break;
                case "uint16":
                case "ushort":
                    parsedTypeName = "System.UInt16";
                    break;
                case "uint32":
                case "uint":
                    parsedTypeName = "System.UInt32";
                    break;
                case "uint64":
                case "ulong":
                    parsedTypeName = "System.UInt64";
                    break;
            }

            if (parsedTypeName != null)
            {
                if (isArray)
                    parsedTypeName = parsedTypeName + "[]";

                if (isNullable)
                    parsedTypeName = String.Concat("System.Nullable`1[", parsedTypeName, "]");
            }
            else
                parsedTypeName = typeName;

            // Expected to throw an exception in case the type has not been recognized.
            return Type.GetType(parsedTypeName);
        }

        #endregion Is<Type>


    }
}
