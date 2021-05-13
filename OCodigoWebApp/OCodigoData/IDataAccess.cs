using System.Collections.Generic;

namespace OCodigoData
{
    public interface IDataAccess
    {
        System.Threading.Tasks.Task<IEnumerable<CustomerDTO>> GetCustomersAsync();
    }
}