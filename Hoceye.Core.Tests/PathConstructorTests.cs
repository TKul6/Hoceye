using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HocEye.Core;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using Moq;
using NUnit.Framework;

namespace Hoceye.Core.Tests
{
    [TestFixture(Author = "Tomer K", Category = "Hoceye.Core")]
    public class PathConstructorTests
    {
     
        [Test(Description = "Validate simple path construction, when the all the path elements exist in the same line")]
        [TestCase("application.prod.resources.mongo.connection", "application.prod.resources.mongo",29)]
        [TestCase("application.prod.resources.mongo.connection{", "application.prod.resources.mongo",29)]
        [TestCase("application.prod.resources.mongo.connection{", "application.prod.resources.mongo.connection",40)]
        [TestCase("application.prod.resources.mongo.connection:", "application.prod.resources.mongo.connection",40)]
        [TestCase("application.prod.resources.mongo.connection", "application",5)]
        [TestCase("{application.prod.resources.mongo.connection", "application", 5)]
        [TestCase("..............", "",5)]
        [TestCase("}", "", 0)]
        public void When_Constucting_Elment_Path(string rawLine, string excpectedPath, int position)
        {
            //Act
            
            var constructor = new PathConstructor();


            //Act

            var pathResult = constructor.ConstructPathBackwards(rawLine, position);

            //Assert

            Assert.That(pathResult, Is.EqualTo(excpectedPath));

        }

        [Test(Description = "Validate getting the first name in the path")]
        [TestCase("application.prod.resources.mongo.connection", 28, "mongo")]  
        [TestCase("application.prod.resources.mongo.connection{", 40, "connection")]
        [TestCase("application.prod.resources.mongo.connection:", 40, "connection")]
        [TestCase("application.prod.resources.mongo.connection", 40, "connection")]
        [TestCase("application.prod.resources.mongo.connection", 5, "application")]
        [TestCase("{application.prod.resources.mongo.connection", 5, "application")]
        [TestCase("..............", 5, "")]
        public void When_Getting_First_Element(string expression, int position, string expectedResult)
        {

            var constructor = new PathConstructor();
            
            //Act
            
            var result = constructor.GetWord(expression, position);

            //Assert

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}