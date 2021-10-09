using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;

namespace Courses_Distribution_System.Services
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Add all repositories to the Autofac container
            builder.RegisterAssemblyTypes(
            GetType().GetTypeInfo().Assembly)
            .Where(c => c.Name.EndsWith("Repository"))
            .AsImplementedInterfaces();
        }
    }
}

