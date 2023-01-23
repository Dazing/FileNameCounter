using System.Reflection;
using Xunit.Sdk;

namespace App.Tests.Common.Attributes
{
    public sealed class EmbeddedResourceDataAttribute : DataAttribute
    {
        private readonly Type _type;
        private readonly string[] _args;

        public EmbeddedResourceDataAttribute(Type type, params string[] args)
        {
            _type = type;
            _args = args;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var result = new object[_args.Length];
            for (var index = 0; index < _args.Length; index++)
            {
                result[index] = ReadManifestData(_args[index]);
            }

            return new[] { result };
        }

        public string ReadManifestData(string resourceName)
        {
            resourceName = resourceName.Replace("\\", ".").Replace("/", ".");
            var resourcePath = $"{_type.Namespace}.{resourceName}";
            using var stream = _type.Assembly.GetManifestResourceStream(resourcePath);
            if (stream is null)
            {
                throw new InvalidOperationException("Could not load manifest resource stream.");
            }

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}