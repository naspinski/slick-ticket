using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickTicket.DomainModel
{
    public class Units
    {
        public static int Default //defaults to the lowest permission subgroup
        { get { return new dbDataContext().sub_units.OrderBy(x => x.access_level).First().id; } }
    }
}
