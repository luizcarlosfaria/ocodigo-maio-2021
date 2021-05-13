using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCodigoData
{
    public class ConnectionManager : IDisposable
    {
        private bool _disposed;
        private IDbConnection _dbConnection { get; }

        public ConnectionManager(IDbConnection dbConnection)
        {
            this._dbConnection = dbConnection;

        }

        public IDbConnection DbConnection
        {
            get
            {
                if (this._dbConnection.State == ConnectionState.Closed)
                    this._dbConnection.Open();

                return this._dbConnection;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if(this._dbConnection.State != ConnectionState.Closed)
                    this._dbConnection.Close();

                this._dbConnection.Dispose();
            }

            _disposed = true;
        }

    }
}
