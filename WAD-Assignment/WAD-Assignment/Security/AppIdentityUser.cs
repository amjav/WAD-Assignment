﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WAD_Assignment.Security
{
    public class AppIdentityUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
