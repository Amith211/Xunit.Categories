using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Categories
{
    public class IntegrationTestDiscoverer:ITraitDiscoverer
    {
        internal const string DiscovererTypeName = DiscovererUtil.AssemblyName + "." + nameof(IntegrationTestDiscoverer);

        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            var mainAtt = new KeyValuePair<string, string>("Category", "IntegrationTest");
            bool skipWhenLiveUnitTesting = false;

            var attributeInfo = traitAttribute as ReflectionAttributeInfo;
            if (attributeInfo?.Attribute is IntegrationTestAttribute testCaseAttribute)
            {
                skipWhenLiveUnitTesting = testCaseAttribute.SkipWhenLiveUnitTesting;
            }

            switch (skipWhenLiveUnitTesting)
            {
                case true:
                    return new[] 
                    {
                        mainAtt,
                        new KeyValuePair<string, string>("Category", "SkipWhenLiveUnitTesting")
                    };
                default:
                    return new[] { mainAtt };
            };
        }
    }
}