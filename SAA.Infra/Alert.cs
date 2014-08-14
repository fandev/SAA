using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Infra
{
    public class Alert
    {
       public string Message { get; set; }
        public AlertType Type { get; set; }
        public string HTMLClass { get; private set; }
        public string UrlHelp { get; set; }
        public string UrlName { get; set; }
        public string UrlRedirect { get; set; }

        public Alert(string message, AlertType type)
        {
            this.Message = message;
            this.Type = type;
            SetTypeHTMLClass(type);
        }

        public Alert(string message, AlertType type, string urlRedirect)
        {
            this.Message = message;
            this.Type = type;
            this.UrlRedirect = urlRedirect;
            SetTypeHTMLClass(type);
        }

        public Alert(string message, AlertType type, string urlHelp, string urlName)
        {
            this.Message = message;
            this.Type = type;
            this.UrlHelp = urlHelp;
            this.UrlName = urlName;
            SetTypeHTMLClass(type);

        }

        private void SetTypeHTMLClass(AlertType type)
        {
            switch (this.Type)
            {
                case AlertType.Warning:
                    this.HTMLClass = "alert-warning";
                    break;
                case AlertType.Error:
                    this.HTMLClass = "alert-danger";
                    break;
                case AlertType.Success:
                    this.HTMLClass = "alert-success";
                    break;
                case AlertType.Info:
                    this.HTMLClass = "alert-info";
                    break;
            }
        }
    }

    public enum AlertType
    {
        Warning = 'w',
        Error = 'e',
        Success = 's',
        Info = 'i'
    }
}

