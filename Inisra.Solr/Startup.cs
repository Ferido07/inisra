using System.Configuration;
using Inisra.Solr.Documents;

namespace Inisra.Solr
{
    public class Startup
    {
        private static readonly string solrCVUrl = ConfigurationManager.AppSettings["solrCVUrl"];
        private static readonly string solrJDUrl = ConfigurationManager.AppSettings["solrJDUrl"];

        public static void Init()
        {    
            SolrNet.Startup.Init<CV>(solrCVUrl);
            SolrNet.Startup.Init<JobDescription>(solrJDUrl);
        }
    }
}
