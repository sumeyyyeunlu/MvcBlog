using System;
using System.Collections.Generic;

namespace MvcBlog.Models
{
    public partial class aspnet_Profile
    {
        public System.Guid UserId { get; set; }
        public string PropertyNames { get; set; }
        public string PropertyValuesString { get; set; }
        public byte[] PropertyValuesBinary { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
    }
}
