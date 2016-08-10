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

        [Test(Description = "Test getting hocon object with simple path")]
        public void When_Getting_Simple_Object()
        {
            //Arrange 

            string hocon = @"{key: 6}";

            IEnumerator<string> enumerator = (new string[] {hocon}).AsEnumerable().GetEnumerator();

            var pathConstructorMock = new Mock<IPathConstructor>();

            pathConstructorMock.Setup(
                ctor => ctor.ConstructPathBackwards(enumerator,IRRELEVAENT_INT)).Returns("key");

            var retriever = new HoconRetriever(hocon,pathConstructorMock.Object);
            
            //Act

            var hoconConfig = retriever.GetHoconObject(enumerator, IRRELEVAENT_INT);

            //Assert
         
            Assert.That(hoconConfig, Is.EqualTo("key : 6"));


        }
    }
}
