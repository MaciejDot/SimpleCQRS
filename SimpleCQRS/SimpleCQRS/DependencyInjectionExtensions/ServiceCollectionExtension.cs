using Microsoft.Extensions.DependencyInjection;
using SimpleCQRS.Command;
using SimpleCQRS.Query;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace SimpleCQRS.DependencyInjectionExtensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSimpleCQRS(this IServiceCollection services, Assembly assembly) 
        {
            var queries = assembly
                .GetTypes()
                .Where(type => type.GetInterfaces()
                    .Any(inter => inter.GetTypeInfo().IsGenericType && 
                        inter.GetTypeInfo().GetGenericTypeDefinition() == typeof(IQuery<>)));

            var commands = assembly
                .GetTypes()
                .Where(type => 
                    type.GetInterfaces().Any(inter => 
                    inter.FullName == typeof(ICommand).FullName));

            var queryHandlers = queries.
                ToDictionary(query => query.FullName, query => assembly
                    .GetTypes()
                    .Where(type => 
                    type.GetInterfaces()
                        .Any(inter => inter.GetTypeInfo().IsGenericType &&
                            inter.GetTypeInfo().GetGenericTypeDefinition() == typeof(IQueryHandler<,>) &&
                            inter.GetTypeInfo().GetGenericArguments().First().FullName == query.FullName)).FirstOrDefault());
            
            queryHandlers
                .Where(x=>x.Value != null)
                .ToList()
                .ForEach(a => services.AddTransient(a.Value, a.Value));
                
            var commandHandlers = commands.
                ToDictionary(command => command.FullName, command => assembly
                    .GetTypes()
                    .Where(type => type.GetInterfaces()
                        .Any(inter => inter.GetTypeInfo().IsGenericType &&
                            inter.GetTypeInfo().GetGenericTypeDefinition() == typeof(ICommandHandler<>) &&
                            inter.GetTypeInfo().GetGenericArguments().First().FullName == command.FullName)).FirstOrDefault());

            commandHandlers
                .Where(x => x.Value != null)
                .ToList()
                .ForEach(a => services.AddTransient(a.Value, a.Value));

            services.AddScoped<ICommandDispatcher>(serviceProvider => new CommandDispatcher(serviceProvider, commandHandlers));

            services.AddScoped<IQueryProcessor>(serviceProvider => new QueryProcessor(serviceProvider, queryHandlers));

            return services;
        }
    }
}
