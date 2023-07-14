using Metrik.Data.Abstract;
using Metrik.Data.Concrete;
using Metrik.Data.Concrete.EntityFramework.Contexts;
using Metrik.Services.Abstract;
using Metrik.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Metrik.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection sc) {
            sc.AddSingleton<IEncryptionService, EncryptionManager>();
            sc.AddDbContext<MetrikContext>();
            sc.AddScoped<IUnitOfWork, UnitOfWork>();
            sc.AddScoped<IRoleService, RoleManager>();
            sc.AddScoped<IUserService, UserManager>();    
            return sc;
        }
    }
}
