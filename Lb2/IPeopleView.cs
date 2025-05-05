using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lb2
{
    internal interface IPeopleView
    {
       void UpdatePeopleTable(DataTable table);
    }
}
