using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgoMini.Models.NaoPersistidos
{
    public class FileUpload
    {
        public HttpPostedFileBase File { get; set; }

        public string Nome { get; set; }
    }
}