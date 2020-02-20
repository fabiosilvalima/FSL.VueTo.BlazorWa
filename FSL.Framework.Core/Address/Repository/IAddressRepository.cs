using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSL.Framework.Core.Address.Repository
{
    public interface IAddressRepository
    {
        Task<Models.Address> GetAddressAsync(
            string zipCode);

        Task<IEnumerable<Models.Address>> GetAddressRangeAsync(
            string start,
            string end);
    }
}
