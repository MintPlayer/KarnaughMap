﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KarnaughMap
{
    /// <summary>This enum defines a cell state for a karnaugh map cell</summary>
    internal enum eCellValue
    {
        /// <summary>0</summary>
        Zero,
        /// <summary>1</summary>
        One,
        /// <summary>X</summary>
        DontCare,
        /// <summary>-</summary>
        Undefined
    }
}
