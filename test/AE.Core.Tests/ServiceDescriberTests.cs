namespace AE.Core.Tests
{
    using System.Linq;
    using System.Reflection;

    using DI;

    using Microsoft.Extensions.DependencyInjection;

    using Xunit;

    public class ServiceDescriberTests
    {
        [Fact]
        public void When_Assembly_has_services_as_dependencies_describer_should_describe_them_correct()
        {
            // Arrange
            var assembly = GetTestAssembly();

            // Act
            var serviceDescriptions = ServicesDescriber.DescribeFromAssemblies(assembly);

            // Assert
            Assert.NotEmpty(serviceDescriptions);
            var description = serviceDescriptions.First();
            Assert.Equal(ServiceLifetime.Scoped, description.Lifetime);
            Assert.Equal(typeof(ITestDependency), description.ServiceType);
            Assert.Equal(typeof(TestServiceDependency), description.ImplementationType);
        }

        private Assembly GetTestAssembly()
        {
            return typeof(ServiceDescriberTests).GetTypeInfo().Assembly;
        }

        public interface ITestDependency : IDependency
        {
        }

        public class TestServiceDependency : ITestDependency
        {
        }
    }
}