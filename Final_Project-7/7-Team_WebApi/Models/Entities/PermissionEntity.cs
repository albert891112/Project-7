﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{
    public class PermissionEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }
}