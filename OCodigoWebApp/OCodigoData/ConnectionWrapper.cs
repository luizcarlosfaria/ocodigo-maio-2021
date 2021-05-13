using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCodigoData
{
    public class ConnectionWrapper : IDisposable
    {
        private bool _disposed;

        public ConnectionWrapper(IDbConnection dbConnection)
        {
            this.DbConnection = dbConnection;

        }

        public void Open()
        {
            this.DbConnection.Open();
        }

        public void Close()
        {
            this.DbConnection.Close();
            this.DbConnection.Dispose();
        }

        public IDbConnection DbConnection { get; }

        public void Dispose()
        {
            Dispose(true);
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
                this.Close();
            }

            _disposed = true;
        }

    }
}
