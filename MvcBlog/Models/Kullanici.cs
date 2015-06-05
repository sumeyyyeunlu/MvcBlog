using System;
using System.Collections.Generic;

namespace MvcBlog.Models
{
    public partial class Kullanici
    {
        public Kullanici()
        {
            this.Makales = new List<Makale>();
            this.Resims = new List<Resim>();
            this.SiteTakips = new List<SiteTakip>();
            this.Yorums = new List<Yorum>();
        }

        public System.Guid Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Mail { get; set; }
        public System.DateTime KayitTarihi { get; set; }
        public string Nick { get; set; }
        public int? ResimID { get; set; }
        public int YazarMi { get; set; }
        public bool Aktif { get; set; }
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual ICollection<Makale> Makales { get; set; }
        public virtual ICollection<Resim> Resims { get; set; }
        public virtual ICollection<SiteTakip> SiteTakips { get; set; }
        public virtual Resim Resim { get; set; }
        public virtual ICollection<Yorum> Yorums { get; set; }
    }
}
