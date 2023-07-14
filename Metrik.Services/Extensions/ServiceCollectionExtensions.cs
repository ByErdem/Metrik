using Metrik.Data.Abstract;
using Metrik.Data.Concrete;
using Metrik.Data.Concrete.EntityFramework.Contexts;
using Metrik.Services.Abstract;
using Metrik.Services.AutoMapper.Profiles;
using Metrik.Services.Concrete;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Metrik.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection sc, IConfiguration configuration) {
            sc.AddJwt(configuration);
            sc.AddSingleton<IEncryptionService, EncryptionManager>();
            sc.AddDbContext<MetrikContext>();
            sc.AddScoped<IUnitOfWork, UnitOfWork>();
            sc.AddScoped<IRoleService, RoleManager>();
            sc.AddScoped<IUserService, UserManager>();
            sc.AddSingleton<ITokenService, TokenManager>();
            sc.AddAutoMapper(typeof(RoleProfile), typeof(UserProfile));
            sc.AddSwaggerGenerator();
            sc.AddControllers(opt => opt.Filters.Add(new AuthorizeFilter()));
            sc.AddEndpointsApiExplorer();
            return sc;
        }
    }
}