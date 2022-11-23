using System.Security.Cryptography;

namespace ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.Aws.Extensions
{
    internal static class S3Extensions
    {
        public static byte[] FromBase64String(this string value)
        {
            return Convert.FromBase64String(value);
        }

        public static string ToS3ETagString(this string value)
        {
            return value.Trim('"').ToLowerInvariant();
        }

        public static string ToBase64String(this byte[] value)
        {
            return Convert.ToBase64String(value);
        }

        public static string ToS3ETagString(this byte[] value)
        {
            return BitConverter.ToString(value).Replace("-", string.Empty).ToLowerInvariant();
        }

        public static byte[] Md5Hash(this Stream stream)
        {
            using (var md5 = MD5.Create())
            {
                stream.Position = 0;
                byte[] hash = md5.ComputeHash(stream);
                stream.Position = 0;

                return hash;
            }
        }
    }
}
