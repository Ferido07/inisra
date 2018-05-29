using CommonServiceLocator;
using Inisra.Solr.Documents;
using SolrNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inisra.Solr.Operations
{
    public class SolrOperations
    {
        public async Task AddCVAsync(Stream fileStream, string id, string resourceName, string owner, string title)
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<CV>>();
            var response = await solr.ExtractAsync(
                                new ExtractParameters(fileStream, id, resourceName)
                                {
                                    ExtractOnly = false,
                                    Fields = new[] {
                                        new ExtractField("owner", owner),
                                        new ExtractField("title", title)
                                    },
                                    AutoCommit = true
                                }
                           );
        }

        public async Task AddJobDescriptionAsync(Stream fileStream, string id, string resourceName, string owner, string title)
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<JobDescription>>();
            var response = await solr.ExtractAsync(
                                new ExtractParameters(fileStream, id, resourceName)
                                {
                                    ExtractOnly = false,
                                    Fields = new[] {
                                        new ExtractField("owner", owner),
                                        new ExtractField("title", title)
                                    },
                                    AutoCommit = true
                                }
                           );
        }

        public async Task<List<CV>> CVQueryAsync(string query)
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<CV>>();
            var cvs = await solr.QueryAsync(query);
            return cvs.ToList();
        }

        public async Task<List<JobDescription>> JobDescriptionQueryAsync(string query)
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<JobDescription>>();
            var jds = await solr.QueryAsync(query);
            return jds.ToList();
        }

    }
}
