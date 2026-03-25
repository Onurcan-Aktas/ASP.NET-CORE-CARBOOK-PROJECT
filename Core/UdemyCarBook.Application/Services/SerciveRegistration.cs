using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR; // Eksik olan buydu
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Services
{
    public static class SerciveRegistration
    {
        public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            // MediatR v11 için doğru kayıt yöntemi budur:
            services.AddMediatR(typeof(SerciveRegistration).Assembly);
        }
    }
}