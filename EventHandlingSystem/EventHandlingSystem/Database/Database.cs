using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class Database
    {
        public static  readonly EventHandlingDataModelContainer Context = new EventHandlingDataModelContainer();
    }
}