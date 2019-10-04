using System;
using System.Collections.Generic;
using System.Text;

namespace QuineMcCluskey
{
    public class RequiredLoop
    {
        internal RequiredLoop(Data.QuineMcCluskey.Table1.Loop loop)
        {
            MinTerms = loop.MinTerms;
            Text = loop.ToString();
        }

        public int[] MinTerms { get; private set; }
        public string Text { get; private set; }
    }
}
