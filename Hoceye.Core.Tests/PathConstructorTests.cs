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
        [Ignore("Unignore when the When_Getting_First_Element tests are completed")]
        [Test(Description = "Validate simple path construction, when the all the path elements exist in the same line")]
        [TestCase("application.prod.resources.mongo.connection",",application.prod.resources.mongo")]
        public void When_Constucting_Elment_Path(string rawLine, string excpectedPath)
        {
            //Act

            var textNavigator = new Mock<ITextStructureNavigator>();
            
            
            var constructor = new PathConstructor();
            

            //Act

            var pathResult = constructor.ConstructPathBackwards(rawLine,excpectedPath.Length-3);

            //Assert

            Assert.That(pathResult, Is.EqualTo(excpectedPath));

        }

        [Test(Description = "Validate getting the first name in the path")]
        [TestCase("application.prod.resources.mongo.connection",28,"mongo")]
        [TestCase("application.prod.resources.mongo.connection{", 40,"connection")]
        public void When_Getting_First_Element(string expression,int position,string expectedResult)
        {

            var constructor = new PathConstructor();


           var builder = new StringBuilder();

            //Act

            var newPosition = constructor.AppendFirstWord(expression, position, builder);

            //Assert

            var expectedNewPosition = expression.LastIndexOf('.', position - 1) -1;

            Assert.That(newPosition, Is.EqualTo(expectedNewPosition));

            Assert.That(builder.ToString(),Is.EqualTo(expectedResult));
        }
    }
}