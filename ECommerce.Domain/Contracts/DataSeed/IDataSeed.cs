using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts.DataSeed
{
    #region DataSeed
    public interface IDataSeed
    {
        public Task SeedDataAsync();
    }
    #region
}
