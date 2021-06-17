using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Domain.Entities;

namespace Marketplace.Domain.Entities
{
    public static class PictureRules
    {
        public static bool HasCorrectSize(this Picture picture)
        {
            return picture is not null &&
                   picture.Size.Width >= 800 &&
                   picture.Size.Height >= 600;
        }
    }
}
