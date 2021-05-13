using Dapper;
using System;
using System.Collections.Generic;

namespace OCodigoData
{
    public class DataAccess
    {
        private readonly ConnectionWrapper connectionWrapper;

        public DataAccess(ConnectionWrapper connectionWrapper)
        {
            this.connectionWrapper = connectionWrapper;
        }

        public IEnumerable<CustomerDTO> GetCustomers()
        {
            return connectionWrapper.DbConnection.Query<CustomerDTO>("SELECT CUSTOMERID, NAME FROM CUSTOMER");
        }
    }
}
