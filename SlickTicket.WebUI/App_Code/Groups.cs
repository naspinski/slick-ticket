//Slick-Ticket v2.0 - 2009
//http://slick-ticket.com :: http://naspinski.net
//Developed by Stan Naspinski - stan@naspinski.net

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SlickTicket.DomainModel;
using SlickTicket.DomainModel.Objects;

/// <summary>
/// Summary description for Groups
/// </summary>
public static class Groups
{
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
        return from p in Tickets.GetTickets(db, true) where p.sub_unit.unit_ref == unit_ref && p.closed == DateTime.Parse("1/1/2001") select p;
    }

    public static IEnumerable<ticket> ClosedTickets(stDataContext db, int unit_ref)
    {
        return from p in Tickets.GetTickets(db, true) where p.sub_unit.unit_ref == unit_ref && p.closed != DateTime.Parse("1/1/2001") select p;
    }

    public class SubGroups
    {
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
            return from p in Tickets.GetTickets(db, true) where p.sub_unit.id == sub_unit_ref && p.closed == DateTime.Parse("1/1/2001") select p;
        }

        public static IEnumerable<ticket> ClosedTickets(stDataContext db, int sub_unit_ref)
        {
            return from p in Tickets.GetTickets(db, true) where p.sub_unit.id == sub_unit_ref && p.closed != DateTime.Parse("1/1/2001") select p;
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
