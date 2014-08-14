using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Model.ViewModel
{
    public class UrlRedirect
    {

        public UrlRedirect(string url, bool isValidAppClient)
        {
            this.Url = string.IsNullOrWhiteSpace(url) ? "/" : url;
            this.IsValidAppClient = isValidAppClient;
        }
        public string Url { get; private set; }
        public bool IsValidAppClient { get; private set; }
    }
}
