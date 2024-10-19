using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BuildingBlocks.Behaviors
{
    public static class PipelineBehaviors
    {
        public static void AddPipelinesMiddlewares(this IServiceCollection services) 
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
