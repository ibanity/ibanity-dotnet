using Ibanity.Apis.Client.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ibanity.Apis.Client.Tests.Models
{
    [TestClass]
    public class FilterTest
    {
        [TestMethod]
        public void Foo()
        {
            var target = new Filter("foo", FilterOperator.Contains, "bar");
            var result = target.ToString();
            Assert.AreEqual("filter[foo][contains]=bar", result);
        }
    }
}
