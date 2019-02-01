using System;

using Microsoft.OpenApi;

namespace Aliencube.AzureFunctions.Extensions.OpenApi.Extensions
{
    /// <summary>
    /// This represents the extension entity for enums.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Converts the <see cref="TypeCode"/> value into data type specified in Open API spec.
        /// </summary>
        /// <param name="enum"><see cref="TypeCode"/> value.</param>
        /// <returns>Data type specified in Open API spec.</returns>
        public static string ToDataType(this TypeCode @enum)
        {
            switch (@enum)
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return "integer";

                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return "number";

                case TypeCode.Boolean:
                    return "boolean";

                case TypeCode.DateTime:
                case TypeCode.String:
                    return "string";

                case TypeCode.Object:
                    return "object";

                case TypeCode.Empty:
                case TypeCode.DBNull:
                case TypeCode.Char:
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                default:
                    throw new InvalidOperationException("Invalid data type");
            }
        }

        /// <summary>
        /// Converts the <see cref="TypeCode"/> value into data format specified in Open API spec.
        /// </summary>
        /// <param name="enum"><see cref="TypeCode"/> value.</param>
        /// <returns>Data format specified in Open API spec.</returns>
        public static string ToDataFormat(this TypeCode @enum)
        {
            switch (@enum)
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                    return "int32";

                case TypeCode.Int64:
                    return "int64";

                case TypeCode.Single:
                    return "float";

                case TypeCode.Double:
                case TypeCode.Decimal:
                    return "double";

                case TypeCode.DateTime:
                    return "date-time";

                case TypeCode.Char:
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Boolean:
                case TypeCode.String:
                case TypeCode.Object:
                    return null;

                case TypeCode.Empty:
                case TypeCode.DBNull:
                default:
                    throw new InvalidOperationException("Invalid data type");
            }
        }

        /// <summary>
        /// Gets the content type.
        /// </summary>
        /// <param name="format"><see cref="OpenApiFormat"/> value.</param>
        /// <returns>The content type.</returns>
        public static string GetContentType(this OpenApiFormat format)
        {
            switch (format)
            {
                case OpenApiFormat.Json:
                    return "application/json";

                case OpenApiFormat.Yaml:
                    return "application/yaml";

                default:
                    throw new InvalidOperationException("Invalid Open API format");
            }
        }
    }
}