using SolrNet.Attributes;

namespace Inisra.Solr.Documents
{
    public class CV
    {
        [SolrUniqueKey("id")]
        public string Id { get; set; }

        [SolrField("owner")]
        public string Owner { get; set; }

        [SolrField("title")]
        public string Title { get; set; }
    }
}
