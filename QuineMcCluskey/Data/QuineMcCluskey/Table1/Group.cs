using System.Collections.Generic;

namespace QuineMcCluskey.Data.QuineMcCluskey.Table1
{
    internal class Group
    {
        public Group()
        {
            Records = new List<Record>();
        }

        public List<Record> Records { get; set; }
    }
}
