using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Core.Entities;
using Microsoft.AspNetCore.Http;
using SaasKit.Multitenancy;


namespace BookStore.Infrastructure.Services
{
    public class TenantResolver : ITenantResolver<Tenant>
    {
        private readonly BookStoreContext _dbContext;

        public TenantResolver(BookStoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<TenantContext<Tenant>> ResolveAsync(HttpContext context)
        {
            TenantContext<Tenant> tenantContext = null;

            var tenant = _dbContext.Tenants.FirstOrDefault(
                t => t.Domain.Equals(context.Request.Host.Value.ToLower()));

            if (tenant != null)
            {
                tenantContext = new TenantContext<Tenant>(tenant);
            }

            return Task.FromResult(tenantContext);
        }

    }
}
