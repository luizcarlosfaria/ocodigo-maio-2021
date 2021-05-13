using Dapper;
using System;
using System.Collections.Generic;

namespace OCodigoData
{
    public class DataAccess : IDataAccess
    {
        private readonly ConnectionManager connectionManager;

        public DataAccess(ConnectionManager connectionWrapper)
        {
            this.connectionManager = connectionWrapper;
        }

        public System.Threading.Tasks.Task<IEnumerable<CustomerDTO>> GetCustomersAsync()
        {
            return connectionManager.DbConnection.QueryAsync<CustomerDTO>("SELECT CUSTOMERID, NAME FROM CUSTOMER");
        }
    }
}
