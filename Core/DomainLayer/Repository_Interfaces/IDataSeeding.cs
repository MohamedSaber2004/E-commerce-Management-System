using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Repository_Interfaces
{
    public interface IDataSeeding
    {
        Task DataSeedAsync();

        Task IdentityDataSeedAsync();
    }
}
