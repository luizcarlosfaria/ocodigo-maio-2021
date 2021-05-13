using Dapper;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace OCodigoData
{
    public class CachedDataAccess : IDataAccess
    {
        const string key = "customer";

        private readonly DataAccess innerDataAccess;
        private readonly IRedisDatabase redis;

        public CachedDataAccess(DataAccess innerDataAccess, IRedisDatabase redis)
        {
            this.innerDataAccess = innerDataAccess;
            this.redis = redis;
        }

        public async System.Threading.Tasks.Task<IEnumerable<CustomerDTO>> GetCustomersAsync()
        {
            IEnumerable<CustomerDTO>  customers = await this.redis.GetAsync<IEnumerable<CustomerDTO>>(key);
            if (customers == null)
            {
                customers = await GetCustomersInternal();
                await this.redis.AddAsync(key, customers);
            }
            return customers;
        }

        private async System.Threading.Tasks.Task<IEnumerable<CustomerDTO>> GetCustomersInternal()
        {
            return await innerDataAccess.GetCustomersAsync();
        }
    }
}
