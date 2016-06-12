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
        [TestCase("application.prod.resources.mongo.connection",",application.prod.resources.mongo")]
        public void When_Constucting_Elment_Path(string rawLine, string excpectedPath)
        {
            //Act

            var textNavigator = new Mock<ITextStructureNavigator>();

            textNavigator.Setup(t => t.GetExtentOfWord(It.IsAny<SnapshotPoint>())).Returns((SnapshotPoint point) =>
            {
                var snapshot = point.Snapshot;

             

                var end = rawLine.LastIndexOf('.');
                var start = rawLine.LastIndexOf('.', 0, end - 1);

                var snapshotSpan = new SnapshotSpan(snapshot,start,end);
                

                var textExtent = new TextExtent(snapshotSpan, false);

                return textExtent;
                
            });

            
            



            var textSnapshot = new Mock<ITextSnapshot>();

            textSnapshot.SetupGet(snapshot => snapshot.Length).Returns(rawLine.Length);

            
            var constructor = new PathConstructor(textNavigator.Object);

            //Act

            var pathResult = constructor.ConstructPathBackwards(textSnapshot.Object,excpectedPath.Length-3);

            //Assert

            Assert.That(pathResult, Is.EqualTo(excpectedPath));

        }
    }
}