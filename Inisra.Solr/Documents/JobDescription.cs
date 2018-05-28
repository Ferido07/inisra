using SolrNet.Attributes;

namespace Inisra.Solr.Documents
{
    internal class JobDescription
    {
        [SolrUniqueKey("id")]
        public string Id { get; set; }

        [SolrField("owner")]
        public string Owner { get; set; }
    }
}