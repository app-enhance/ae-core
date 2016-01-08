namespace AE.Core.DI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Microsoft.Extensions.DependencyInjection;

    public static class ServicesDescriber
    {
        private static readonly Type DependencyType = typeof(IDependency);

        private static readonly Type TransientDependencyType = typeof(ITransientDependency);

        private static readonly Type SingletonDependencyType = typeof(ISingletonDependency);

        private static readonly Type NotRegisterDependencyType = typeof(INotRegisterDependency);

        public static IEnumerable<ServiceDescriptor> DescribeFromAssemblies(params Assembly[] assembliesToScan)
        {
            var typesToRegistration = RetrieveTypesToRegistration(assembliesToScan);
            var serviceDescriptors = CreateDescriptors(typesToRegistration);

            return serviceDescriptors;
        }

        private static IEnumerable<Type> RetrieveTypesToRegistration(IEnumerable<Assembly> assembliesToScan)
        {
            return assembliesToScan.SelectMany(x => x.ExportedTypes).Where(IsTypeToAutoRegistration);
        }

        private static bool IsTypeToAutoRegistration(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return DependencyType.IsAssignableFrom(type) && !NotRegisterDependencyType.IsAssignableFrom(type) && typeInfo.IsClass
                   && !typeInfo.IsAbstract && !typeInfo.IsGenericTypeDefinition;
        }

        private static IEnumerable<ServiceDescriptor> CreateDescriptors(IEnumerable<Type> typesToRegistration)
        {
            foreach (var type in typesToRegistration)
            {
                var interfaces = type.GetInterfaces();
                var scope = RetieveScope(interfaces);

                foreach (var @interface in interfaces)
                {
                    yield return new ServiceDescriptor(@interface, type, scope);
                }

                yield return new ServiceDescriptor(type, type, scope);
            }
        }

        private static ServiceLifetime RetieveScope(IEnumerable<Type> interfaces)
        {
            var scopes = ValidateDependencyScopes(interfaces);

            if (scopes.Any(x => x == TransientDependencyType))
            {
                return ServiceLifetime.Transient;
            }

            if (scopes.Any(x => x == SingletonDependencyType))
            {
                return ServiceLifetime.Singleton;
            }

            return ServiceLifetime.Scoped;
        }

        private static IEnumerable<Type> ValidateDependencyScopes(IEnumerable<Type> interfaces)
        {
            var validScopes = interfaces.Where(x => x == DependencyType || x == TransientDependencyType || x == SingletonDependencyType);
            if (validScopes.Count() > 2)
            {
                throw new DependencyDescriptionException("Cannot set more than one dependency lifetime");
            }

            return validScopes.ToArray();
        }
    }
}