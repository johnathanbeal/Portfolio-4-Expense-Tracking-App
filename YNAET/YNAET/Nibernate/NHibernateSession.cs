using NHibernate;
using NHibernate.Cfg;
using ISession = NHibernate.ISession;
using YNAET.Models;

namespace YNAET.Nibernate
{
    public interface INHibernateSession
    {
        ISession OpenSession();
    }

    public class NHibernateSession : INHibernateSession
    {
        public ISession OpenSession()
        {
            var configuration = new Configuration();
            configuration.AddAssembly(typeof(Expense).Assembly);
            configuration.Configure();
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
