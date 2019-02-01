using NHibernate;
using NHibernate.Cfg;
using System.Web;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ISession = NHibernate.ISession;
using YNAET.Models;
//using Microsoft.AspNetCore.Http.IHttpContextAccessor;

namespace YNAET.Nibernate
{
    public class NHibernateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            //var configurationPath = HttpContext.Current.Server.MapPath(@"~\Models\hibernate.cfg.xml");
            //configuration.Configure(configurationPath);
            //var bookConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Mappings\Expenses.hbm.xml");
            //configuration.AddFile(bookConfigurationFile);
            var assembly = typeof(Expense).Assembly;
            configuration.AddAssembly(assembly);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
