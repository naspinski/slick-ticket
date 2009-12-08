using System.Linq;
using Naspinski.Utilities;
using System.Web;
using System.Collections.Generic;

namespace SlickTicket.DomainModel.Objects
{
    public class Units
    {
        public static int Default //defaults to the lowest permission subgroup
        { get { return new stDataContext().sub_units.OrderBy(x => x.access_level).First().id; } }

        public static IEnumerable<unit> List(stDataContext db, int access_level)
        {
            return (from p in db.sub_units
                    where
                        p.access_level <= access_level
                    orderby p.unit.unit_name
                    select p.unit).Distinct();
        }

        public static unit GetByID(stDataContext db, int id)
        {
            return db.units.First(u => u.id == id);
        }

        public static void Add(stDataContext db, string unitName)
        {
            unit newUnit = new unit();
            newUnit.unit_name = HttpUtility.HtmlEncode(unitName);
            db.units.InsertOnSubmit(newUnit);
            db.SubmitChanges();
        }

        public static IEnumerable<ticket> OpenTickets(stDataContext db, int unit_ref)
        {
            return from p in Tickets.GetTickets(db, true) where p.sub_unit.unit_ref == unit_ref && p.closed == Tickets.NullDate select p;
        }

        public static IEnumerable<ticket> ClosedTickets(stDataContext db, int unit_ref)
        {
            return from p in Tickets.GetTickets(db, true) where p.sub_unit.unit_ref == unit_ref && p.closed != Tickets.NullDate select p;
        }

        public class SubUnits
        {
            public static sub_unit Get(int id) { return Get(new stDataContext(), id); }
            public static sub_unit Get(stDataContext db, int id) { return db.Get<sub_unit>(id); }

            public static IEnumerable<sub_unit> List(stDataContext db, int unit_ref, int access_level)
            {
                var query =
                    from p in db.sub_units
                    where
                        p.unit_ref == unit_ref &&
                        p.access_level <= access_level
                    orderby p.sub_unit_name
                    select p;

                return query;
            }

            public static IEnumerable<ticket> OpenTickets(stDataContext db, int sub_unit_ref)
            {
                return from p in Tickets.GetTickets(db, true) where p.sub_unit.id == sub_unit_ref && p.closed == Tickets.NullDate select p;
            }

            public static IEnumerable<ticket> ClosedTickets(stDataContext db, int sub_unit_ref)
            {
                return from p in Tickets.GetTickets(db, true) where p.sub_unit.id == sub_unit_ref && p.closed != Tickets.NullDate select p;
            }

            public static sub_unit GetByID(stDataContext db, int id)
            {
                return db.sub_units.First(su => su.id == id);
            }

            public static void Add(stDataContext db, string subUnitName, int unit, int access_level, string email)
            {
                sub_unit newSubUnit = new sub_unit();
                newSubUnit.sub_unit_name = HttpUtility.HtmlEncode(subUnitName);
                newSubUnit.unit_ref = unit;
                newSubUnit.access_level = access_level;
                newSubUnit.mailto = HttpUtility.HtmlEncode(email);
                db.sub_units.InsertOnSubmit(newSubUnit);
                db.SubmitChanges();
            }
        }
    }
}
