using System.Linq;

namespace SlickTicket.DomainModel.Objects
{
    class Units
    {
        public static int Default //defaults to the lowest permission subgroup
        { get { return new stDataContext().sub_units.OrderBy(x => x.access_level).First().id; } }
    }
}
