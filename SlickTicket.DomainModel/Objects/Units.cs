using System.Linq;
using Naspinski.Utilities;

namespace SlickTicket.DomainModel.Objects
{
    public class Units
    {
        public static int Default //defaults to the lowest permission subgroup
        { get { return new stDataContext().sub_units.OrderBy(x => x.access_level).First().id; } }


        public class SubUnits
        {
            public static sub_unit Get(int id) { return Get(new stDataContext(), id); }
            public static sub_unit Get(stDataContext db, int id) { return db.Get<sub_unit>(id); }
        }
    }
}
