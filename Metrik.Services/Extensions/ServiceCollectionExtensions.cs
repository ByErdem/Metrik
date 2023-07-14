using Metrik.Data.Abstract;
using Metrik.Data.Concrete;
using Metrik.Data.Concrete.EntityFramework.Contexts;
using Metrik.Services.Abstract;
using Metrik.Services.AutoMapper.Profiles;
using Metrik.Services.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Metrik.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection sc) {
            IConfiguration? configuration = sc.BuildServiceProvider().GetService<IConfiguration>();
            sc.AddJwt(configuration);
            sc.AddSingleton<IEncryptionService, EncryptionManager>();
            sc.AddDbContext<MetrikContext>();
            sc.AddScoped<IUnitOfWork, UnitOfWork>();
            sc.AddScoped<IRoleService, RoleManager>();
            sc.AddScoped<IUserService, UserManager>();
            sc.AddAutoMapper(typeof(RoleProfile), typeof(UserProfile));
            sc.AddSwaggerGenerator();
            return sc;
        }
    }
}