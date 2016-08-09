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
using NUnit.Framework.Interfaces;

namespace Hoceye.Core.Tests
{
    [TestFixture(Author = "Tomer K", Category = "Hoceye.Core")]
    public class PathConstructorTests
    {

        [Test(Description = "Validate simple path construction, when the all the path elements exist in the same line")]
        [TestCase(" application.prod.resources.mongo.connection",5, "application")]
        [TestCase("application.prod.resources.mongo.connection", 29,"application.prod.resources.mongo")]
        [TestCase("application.prod.resources.mongo.connection{", 29,"application.prod.resources.mongo")]
        [TestCase("application.prod.resources.mongo.connection{", 40,"application.prod.resources.mongo.connection")]
        [TestCase("application.prod.resources.mongo.connection:",40 ,"application.prod.resources.mongo.connection")]
        [TestCase("application.prod.resources.mongo.connection",5 ,"application")]
        [TestCase("{application.prod.resources.mongo.connection", 5,"application")]
        [TestCase("..............", 5,"")]
        [TestCase("}",0, "")]
        public void When_Constucting_Elment_Path(string rawLine, int position, string excpectedPath)
        {
            //Act

            var constructor = new PathConstructor();

            //Act

            var lines = new[] { rawLine };

            var pathResult = constructor.ConstructPathBackwards(lines.Reverse().GetEnumerator(), position);

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


        [Test(Description = "Verify the PathConstructor can handle multiline path")]
        [TestCaseSource("ProvideLines")]
        public void When_Construting_Multiline_Path(string [] lines, int position, string excpectedPath)
        {
            //Act

            var constructor = new PathConstructor();

            //Act

            var linesEnumerator = lines.Reverse().GetEnumerator();

            var pathResult = constructor.ConstructPathBackwards(linesEnumerator, position);

            //Assert

            Assert.That(pathResult, Is.EqualTo(excpectedPath));
        }

        public static IEnumerable<object[]> ProvideLines()
        {

            yield return
                    new object[] {new[] { "{application.prod", ".resources.mongo.connection" },12,
                        "application.prod.resources.mongo"};
            yield return
                new object[] { new[] { "{application.prod.resources", ".mongo.connection{" },4,
                    "application.prod.resources.mongo"};
            yield return
                new object[]{new[] { "{application.prod.resources.", "mongo.connection{" },10,
                    "application.prod.resources.mongo.connection"};
            yield return
                new object[]
                {
                    new[] {"{application", ".prod.resources.mongo.connection:"},25,
                    "application.prod.resources.mongo.connection"
                };
            
        } 
    }
}