﻿namespace Mlib.UI.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IContextMenuItem
    {
        string Name { get; }
        bool IsSelected { get; set; }
    }
}
