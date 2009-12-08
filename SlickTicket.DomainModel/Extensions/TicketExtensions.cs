using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickTicket.DomainModel.Extensions
{
    public static class TicketExtensions
    {
        public static IEnumerable<comment> ActiveComments(this ticket t)
        {
            return t.comments.Where(x => x.active);
        }
    }
}
