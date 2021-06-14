using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Framework.ApplicationServices
{
    public interface IApplicationService
    {
        Task Handle(object command);
    }
}
