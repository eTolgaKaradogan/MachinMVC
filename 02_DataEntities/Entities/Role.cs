﻿using AppCore.DataAccess.Repositories.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_DataEntities.Entities
{
    public class Role : ObligatoryProperities
    {
        public string Name { get; set; }

        public  List<User> Users { get; set; }
    }
}
