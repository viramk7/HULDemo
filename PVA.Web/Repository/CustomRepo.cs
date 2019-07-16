using PVA.Web.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace PVA.Web.Repository
{
    public class BaseContext
    {
        public static PhysicalVerification_HUL_NewEntities GetDbContext()
        {
            PhysicalVerification_HUL_NewEntities context = new PhysicalVerification_HUL_NewEntities();
            context.Configuration.ProxyCreationEnabled = false;
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
            objectContext.CommandTimeout = int.MaxValue;
            return context;
        }
    }

    public class CustomRepo
    {
    }
}