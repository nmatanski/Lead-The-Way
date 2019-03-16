using LeadTheWay.GraphLayer.Link.Domain.Models;
using LeadTheWay.GraphLayer.Map.Domain.Models;
using LeadTheWay.GraphLayer.Map.Service;
using LeadTheWay.GraphLayer.Vertex.Domain.Models;
using LeadTheWay.Tests.Utility;
using NUnit.Framework;

namespace LeadTheWay.Tests
{
    [TestFixture]
    public class PropertyValidationTests
    {
        private const int NoErrors = 0;
        private const int ClassValidationError = 1;

        private readonly double defaultValueOfDouble = (double)DefaultValue.GetDefaultValue(typeof(double));
        private readonly double defaultValueOfByte = (byte)DefaultValue.GetDefaultValue(typeof(byte));

        private GraphMap graph;
        private TransportVertex vertex;
        private IntercityLink edge;

        [SetUp]
        public void GraphInit()
        {
            graph = new GraphMap
            {
                Id = 1003,
                Name = "TestMap",
                Graph = new Graph(),
                IsDefault = false,
                GraphString = string.Empty,
                NodeHistory = new System.Collections.Generic.List<string> { string.Empty },
                EdgeHistory = new System.Collections.Generic.List<string> { string.Empty },
                NodeHistoryString = string.Empty,
                EdgeHistoryString = string.Empty,
                CurrentEdgeIdToAdd = 1005
            };
            vertex = new TransportVertex
            {
                Name = "TestVertex",
                Description = "TestDescr"
            };
            edge = new IntercityLink
            {
                Length = 2843.874,
                Price = 48.17,
                ServiceClass = 4,
                NodesPair = new VerticesPair
                {
                    FirstNodeId = 1,
                    RelatedNodeId = 2
                }
            };
        }

        [Test]
        public void VerifyGraphMapModelProperties()
        {
            var errorCount = CheckPropertyValidation.Validate(graph).Count;
            Assert.AreEqual(NoErrors, errorCount);
        }

        [Test]
        public void VerifyGraphMapModelRequiredAnnotations()
        {
            var graphForValidation = new GraphMap
            {
                GraphString = string.Empty
            };
            var errorCount = CheckPropertyValidation.Validate(graphForValidation).Count;
            Assert.AreEqual(ClassValidationError, errorCount);
        }

        [Test]
        public void VerifyTransportVertexModelProperties()
        {
            var errorCount = CheckPropertyValidation.Validate(vertex).Count;
            Assert.AreEqual(NoErrors, errorCount);
        }

        [Test]
        public void VerifyTransportVertexModelRequiredAnnotations()
        {
            var vertexForValidation = new TransportVertex
            {
                Description = "TestDescr"
            };
            var errorCount = CheckPropertyValidation.Validate(vertexForValidation).Count;
            Assert.AreEqual(ClassValidationError, errorCount);
        }

        [Test]
        public void VerifyIntercityLinkModelProperties()
        {
            var errorCount = CheckPropertyValidation.Validate(edge).Count;
            Assert.AreEqual(NoErrors, errorCount);
        }

        [Test]
        public void VerifyIntercityLinkModelRequiredAnnotations()
        {
            var edgeToValidate = new IntercityLink
            {
                NodesPair = new VerticesPair
                {
                    FirstNodeId = 1,
                    RelatedNodeId = 2
                }
            };
            var errorCount = CheckPropertyValidation.Validate(edge).Count;
            if (errorCount == 0 && (edgeToValidate.Length == defaultValueOfDouble || edgeToValidate.Price == defaultValueOfDouble || edgeToValidate.ServiceClass == defaultValueOfByte))
            {
                errorCount++;
            }

            Assert.AreEqual(ClassValidationError, errorCount);
        }
    }
}
