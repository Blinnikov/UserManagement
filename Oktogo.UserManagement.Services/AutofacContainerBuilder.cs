using Autofac;

using Oktogo.UserManagement.DataAccess;

namespace Oktogo.UserManagement.Services
{
    public static class AutofacContainerBuilder
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserManagementService>().As<IUserManagementService>();
            builder.RegisterType<DataAccessService>().As<IDataAccessService>();
            return builder.Build();
        }
    }
}