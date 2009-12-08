using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SlickTicket.DomainModel;

namespace SlickTicket.Tests.DomainModel
{
    [TestFixture]
    public class HtmlFilterTests
    {
        [Test]
        public void Filter_takes_out_script_tags()
        {
            string html = "<div><script>bad stuff</script></div>";
            string filtered = HtmlFilter.Filter(html);
            Assert.AreEqual("<div></div>", filtered);
        }

        [Test]
        public void Filter_wont_change_text()
        {
            string text = "#123456;";
            string filtered = HtmlFilter.Filter(text);
            Assert.AreEqual(text, filtered);
        }
    }
}
