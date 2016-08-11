using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HocEye.Core;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Hoceye.Core.Tests
{
    [TestFixture(Category = "Hoceye.Core")]
    public class HoconRetrieverTests
    {

        private const int IRRELEVAENT_INT = -1;

        [Test(Description = "Test getting hocon single value with simple path")]
        public void When_Getting_Value()
        {
            //Arrange 

            string hocon = @"{key: 6}";

            IEnumerator<string> enumerator = (new string[] {hocon}).AsEnumerable().GetEnumerator();

            var pathConstructorMock = GetPathConstructorMock();

            var retriever = new HoconRetriever(hocon,pathConstructorMock.Object);
            
            //Act

            var hoconConfig = retriever.GetHoconObject(enumerator, IRRELEVAENT_INT);

            //Assert
         
            Assert.That(hoconConfig, Is.EqualTo("key : 6"));


        }

        [Test(Description = "Testing getting hocon ovject with simple path")]
        public void When_Getting_Simple_Object()
        {
            //Arrange

            string hocon = @"{key : { 'paramName1' : 6, 'param2' : 7}}";

            var enumerator = (new[] { hocon }).AsEnumerable().GetEnumerator();

            var pathCtorMock = GetPathConstructorMock();

            var retriever = new HoconRetriever(hocon,pathCtorMock.Object);

            //Act

            var result = retriever.GetHoconObject(enumerator, IRRELEVAENT_INT);

            //Assert

            var excpectedResult = @"key : {
  'paramName1' : 6
  'param2' : 7
}";

            Assert.That(result,Is.EqualTo(excpectedResult));
        }

        private Mock<IPathConstructor> GetPathConstructorMock()
        {
            var pathConstructorMock = new Mock<IPathConstructor>();

            pathConstructorMock.Setup(
                ctor => ctor.ConstructPathBackwards(It.IsAny<IEnumerator<string>>(), IRRELEVAENT_INT)).Returns("key");

            return pathConstructorMock;
        }
    }
}
