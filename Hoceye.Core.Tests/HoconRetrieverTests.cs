using System.Collections.Generic;
using System.Linq;
using HocEye.Core;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Hoceye.Core.Tests
{
    [TestFixture(Category = "Hoceye.Core")]
    public class HoconRetrieverTests
    {

        private const int IRRELEVAENT_INT = -1;

        [Test(Description = "Test getting hocon single value with simple path")]
        [TestCaseSource("ProvideData")]
        public void When_Getting_Value(string hocon,string path, string expectedResult)
        {
            //Arrange 
            
            IEnumerator<string> enumerator = (new[] {hocon}).AsEnumerable().GetEnumerator();

            var pathConstructorMock = new Mock<IPathConstructor>();

            pathConstructorMock.Setup(
                ctor => ctor.ConstructPathBackwards(enumerator, IRRELEVAENT_INT)).Returns(path);

            var retriever = new HoconRetriever(hocon,pathConstructorMock.Object);
            
            //Act

            var hoconConfig = retriever.GetHoconObject(enumerator, IRRELEVAENT_INT);

            //Assert
         
            Assert.That(hoconConfig, Is.EqualTo(expectedResult));


        }


        public static IEnumerable<ITestCaseData> ProvideData()
        {
            yield return new TestCaseData("{key: 6}","key", "key : 6").SetDescription("Test getting hocon single value with simple path");

            yield return new TestCaseData("{key : { 'paramName1' : 6, 'param2' : 7}}","key", @"key : {
  'paramName1' : 6
  'param2' : 7
}").SetDescription("Testing getting hocon ovject with simple path");

            yield return new TestCaseData("{key : { 'paramName1' : 6, 'innerObject' : {'MyParam' : 7}}}", "key", @"key : {
  'paramName1' : 6
  'innerObject' : {
    'MyParam' : 7
  }
}").SetDescription("Testing getting hocon object with simple path");


        }  
    }
}
