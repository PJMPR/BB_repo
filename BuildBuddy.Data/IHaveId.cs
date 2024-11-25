using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildBuddy.Data
{
    public interface IHaveId<TId>
    {
        TId Id { get; }
    }
}
