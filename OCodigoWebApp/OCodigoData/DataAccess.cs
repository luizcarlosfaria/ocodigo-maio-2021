using Dapper;
using System;
using System.Collections.Generic;

namespace OCodigoData
{
    public class DataAccess : IDataAccess
    {
        private readonly ConnectionWrapper connectionWrapper;

        public DataAccess(ConnectionWrapper connectionWrapper)
        {
            this.connectionWrapper = connectionWrapper;
        }

        public async System.Threading.Tasks.Task<IEnumerable<CustomerDTO>> GetCustomersAsync()
        {
            connectionWrapper.Open();

            var result = await connectionWrapper.DbConnection.QueryAsync<CustomerDTO>("SELECT CUSTOMERID, NAME FROM CUSTOMER");

            connectionWrapper.Close();
            
            return result;
        }
    }
}
