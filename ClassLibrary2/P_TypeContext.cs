﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class P_TypeContext : DbContext
    {
        public DbSet<P_Type> P_Types { get; set; }
    }
}
