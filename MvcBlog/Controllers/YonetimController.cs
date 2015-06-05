using MvcBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.IO;
using System.Drawing;

namespace MvcBlog.Controllers
{ 
    using Models;
    using System.Web.Security;
    [Authorize(Roles = "Admin")]
  //  [Authorize(Roles = "Yazar")]
    public class YonetimController : Controller
    {
       
        //
        // GET: /Yonetim/
        BlogContext context = new BlogContext();
        public ActionResult Index()
        {
          
            
           // ViewBag.Tip = 1;
            
           return View(context.Makales.ToList());
        }
        /*
        public ActionResult MakaleYaz() {
            ViewBag.Tip = 1;
            ViewBag.Kategoriler = context.Kategoris.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult MakaleYaz(Makale makale, HttpPostedFileBase Resim,string etiketler) {
           
            if (makale != null) {

                Kullanici aktif = Session["Kullanici"] as Kullanici;
                makale.YayimTarihi = DateTime.Now;
                makale.MakaleTipID = 1;
                makale.YazarID = aktif.Id;
               // makale.KapakResimID = ResimKaydet(Resim,HttpContext);
                context.Makales.Add(makale);
                context.SaveChanges();

                string[] etikets = etiketler.Split(',');

                foreach (string etiket in etikets){
                    Etiket etk = context.Etikets.FirstOrDefault(x=>x.Adi.ToLower()==etiket.ToLower().Trim());
               
                if(etk==null)
                {
                    //etiket yok
                etk = new Etiket();
                etk.Adi = etiket;
                context.Etikets.Add(etk);
                context.SaveChanges();
                    
                }

                  
                 makale.Etikets.Add(etk);
                    context.SaveChanges();
                    //etiket var
                }

            
            }
            return RedirectToAction("Index");
        }
        
       
        public static int ResimKaydet(HttpPostedFileBase Resim , HttpContextBase ctx)
        {
            
                //resimlerin boyutlarini belirtmemiz lazim
                BlogContext context = new BlogContext();

                int kucukWidth = Convert.ToInt32(ConfigurationManager.AppSettings["kw"]);
                int kucukHeight = Convert.ToInt32(ConfigurationManager.AppSettings["kh"]);
                int ortaWidth = Convert.ToInt32(ConfigurationManager.AppSettings["ow"]);
                int ortaHeight = Convert.ToInt32(ConfigurationManager.AppSettings["oh"]);
                int buyukWidth = Convert.ToInt32(ConfigurationManager.AppSettings["bw"]);
                int buyukHeight = Convert.ToInt32(ConfigurationManager.AppSettings["bh"]);



                string newName = Path.GetFileNameWithoutExtension(Resim.FileName) + "-" + Guid.NewGuid() + Path.GetExtension(Resim.FileName);

                Image orjRes = Image.FromStream(Resim.InputStream);

                Bitmap kucukRes = new Bitmap(orjRes, kucukWidth, kucukHeight);
                Bitmap ortaRes = new Bitmap(orjRes, ortaWidth, ortaHeight);
                Bitmap buyukRes = new Bitmap(orjRes);


                kucukRes.Save(ctx.Server.MapPath("~/Content/Resim/Kucuk/" + newName));
                ortaRes.Save(ctx.Server.MapPath("~/Content/Resim/Orta/" + newName));
                buyukRes.Save(ctx.Server.MapPath("~/Content/Resim/Buyuk/" + newName));

                Kullanici k = (Kullanici)ctx.Session["Kullanici"];

                Resim dbRes = new Resim();
                dbRes.Adi = Resim.FileName;
                dbRes.BuyukResimYol = "/Content/Resim/Buyuk/" + newName;
                dbRes.OrtaResimYol = "/Content/Resim/Orta/" + newName;
                dbRes.KucukResimYol = "/Content/Resim/Kucuk/" + newName;

                dbRes.EklenmeTarihi = DateTime.Now;
                dbRes.EkleyenID = k.Id;

                context.Resims.Add(dbRes);
                context.SaveChanges();
            
            
            return dbRes.Id;

         }
        */
        //makaleleri kontrol ediyoruz.
        public ActionResult Makale() {
            return View();
           // ViewBag.Tip = 1;
          //  ViewBag.Kategoriler = context.Kategoris.ToList();
           // return View(context.Makales.ToList());  
        }
        public ActionResult MakaleEkle() {

            ViewBag.KategoriID = new SelectList(context.Kategoris, "Id", "Adi");
           // ViewBag.TipID = new SelectList(context.MakaleTips, "Id", "Adi");
            return View();
        }

        string ResimKaydet(HttpPostedFileBase file) {

            Image orj = Image.FromStream(file.InputStream);
            Bitmap kck = new Bitmap(orj , 150, 150);
            Bitmap orta = new Bitmap(orj, 350, 350);
            string dosyaAdi = Path.GetFileNameWithoutExtension(file.FileName) + Guid.NewGuid() + Path.GetExtension(file.FileName);
            orj.Save(Server.MapPath("~/Content/Resim/Buyuk/" + dosyaAdi));
            kck.Save(Server.MapPath("~/Content/Resim/Kucuk/" + dosyaAdi));

            return dosyaAdi;
        }
        [HttpPost]
        public ActionResult MakaleEkle(Makale makale ,HttpPostedFileBase Resim,string etiketler)
        {
            if (ModelState.IsValid) {

                makale.Goruntulenme = 0;
                makale.Begeni = 0;
                makale.YazarID = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
                string dosyaAdi = ResimKaydet(Resim);
                makale.Resim.KucukResimYol = "/Content/Resim/Kucuk/" + dosyaAdi;
                makale.Resim.BuyukResimYol = "/Content/Resim/Buyuk" + dosyaAdi;

                context.Makales.Add(makale);
                context.SaveChanges();


                string[] etikets = etiketler.Split(',');

                foreach (string etiket in etikets)
                {
                    Etiket etk = context.Etikets.FirstOrDefault(x => x.Adi.ToLower() == etiket.ToLower().Trim());

                    if (etk == null)
                    {
                        //etiket yok
                        etk = new Etiket();
                        etk.Adi = etiket;
                        context.Etikets.Add(etk);
                        context.SaveChanges();

                    }

                    makale.Etikets.Add(etk);
                    context.SaveChanges();
                    //etiket var
                } 
                
            }
           return RedirectToAction("Makale");
        }
        public ActionResult MakaleListele() {

            return View(context.Makales);
        
        }
        public ActionResult MakaleSil(int id) {

            //context.Yorums.RemoveRange(context.Yorums.Where(x => x.MakaleID == id));
            context.Makales.Remove(context.Makales.FirstOrDefault(x => x.Id == id));
            context.SaveChanges();
            return RedirectToAction("Makale");
        
        }
        public ActionResult MakaleDuzenle(int id) {

            Makale secili = context.Makales.FirstOrDefault(x => x.Id == id);
            ViewBag.KategoriID = new SelectList(context.Kategoris, "Id", "Adi",secili.KategoriID);
           // ViewBag.TipID = new SelectList(context.MakaleTips, "Id", "Adi");
            ViewBag.YazarID = new SelectList(context.Kullanicis, "Id", "Adi",secili.YazarID);

            return View(secili);
        
        }
        //kategori kontrolleri yaptik
        public ActionResult Kategori() {
           
            
            ViewBag.Tip = 1;
            return View(context.Kategoris.ToList());
        
        }

        public ActionResult KategoriEkle() {
           
            ViewBag.Tip = 1;
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(Kategori kat)
        {
            context.Kategoris.Add(kat);
            context.SaveChanges();
            return RedirectToAction("Kategori");
        }
        public ActionResult KategoriDuzenle(int id) {


            ViewBag.Tip = 1;
            return View(context.Kategoris.FirstOrDefault(x => x.Id == id));
        
        }
        [HttpPost]
        public ActionResult KategoriDuzenle(Kategori kat)
        {


            context.Entry(kat).State = System.Data.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Kategori");
        }

        public ActionResult KategoriSil(int id) {

            context.Kategoris.Remove(context.Kategoris.FirstOrDefault(x => x.Id == id));
            context.SaveChanges();
            return RedirectToAction("Kategori");


        }

        

    }
}
