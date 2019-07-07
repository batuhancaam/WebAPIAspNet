using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiKismi.Models;

namespace WebApiKismi.Controllers
{
    public class OgrencilersController : ApiController//Api Controlleri bu contoller ve proje bir api projesi olarak olusturulduğan özel olarak bir api 
        //controller ekleyebiliyoruz
    {
        private DBModel db = new DBModel();//veritabanının bir nesnesi oluşturuluyor 

        // GET: api/Ogrencilers
        public IQueryable<Ogrenciler> GetOgrencilers()//veritabanı icerisindeki ogrenciler tablosu bir listeye aktarılıyor
        {
            return db.Ogrencilers;
        }

        // GET: api/Ogrencilers/5
        [ResponseType(typeof(Ogrenciler))]
        public IHttpActionResult GetOgrenciler(int id)//Ogrenciler tablosu GET ile okunuyor yani Mvc tarafında bu bilgiler kullanılarak html sayfasında gözükmesi sağlanacak
        {
            Ogrenciler ogrenciler = db.Ogrencilers.Find(id);//Orenciler tablosundan bir nesne ogrenciler nesnesi oluşturularak Find metodu ile id'lerine göre veriler bulunup 
            if (ogrenciler == null)//eğer veri yoksa notfound
            {
                return NotFound();
            }

            return Ok(ogrenciler);//eğer varsa bulunmus veri teker teker geri dönüyor
        }

        // PUT: api/Ogrencilers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOgrenciler(int id, Ogrenciler ogrenciler)//Bu metod veritabanı üzerinde değişiklikler yani update işlemleri yapabilmemiz için yazılmıştır
        {
         

            if (id != ogrenciler.OgrenciId)//eğer beklenen id ile update etmek istediğimiz verinin id'si uyuşmuyorsa BadRequest dönecek
            {
                return BadRequest();
            }

            db.Entry(ogrenciler).State = EntityState.Modified;//eğer uyuşursa entry metodu ile veritabanına giriş yapacak state ile bildirecek
            //Sonrasında EntrySatete.Modefied ile Veritabanına degiştirilmesi gerektiğini söylerek değişiklikleri yapacak

            try
            {
                db.SaveChanges();//ve bir sorun cıkmazsa Degisiklikleri veritabanına kaydedecek bu metod veritabanında herhangi bir değişiklik yapıldığında kullanılır.
            }
            catch (DbUpdateConcurrencyException)//bir sorun oldugun yani catch bloğu icerisinde yazılı hata ile karışılasılırsa program duracak
            {
                if (!OgrencilerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Ogrencilers
        [ResponseType(typeof(Ogrenciler))]
        public IHttpActionResult PostOgrenciler(Ogrenciler ogrenciler)//Bu metod ogrenciler tablosuna yeni veri eklememizi sağlar
        {
           

            db.Ogrencilers.Add(ogrenciler);//veri tabanındaki Ogrenciler tablosuna yeni girilen kayıtları Add metodu ile listeye yani veri tabanına ekler
            db.SaveChanges();//Sonrasında değişikleri veri tabanına kaydeder

            return CreatedAtRoute("DefaultApi", new { id = ogrenciler.OgrenciId }, ogrenciler);//Ve geriye girilen kayıdın id'sini ve Veriyi döndürür.
        }

        // DELETE: api/Ogrencilers/5
        [ResponseType(typeof(Ogrenciler))]
        public IHttpActionResult DeleteOgrenciler(int id)//Bu metod veritabanından bir kaydı silmemizi sağlar
        {
            Ogrenciler ogrenciler = db.Ogrencilers.Find(id);//Burada tekrardan id'ye göre bil silme işlemi yapılacağından önce id'lere göre verilet bulunuyor
            if (ogrenciler == null)//eğer zaten kayıt yoksa NotFound dönüyor
            {
                return NotFound();
            }

            db.Ogrencilers.Remove(ogrenciler);//Var ise Remove Komutu ile ogrenciler tablosundan id'sine göre bulunan veri siliniyor
            db.SaveChanges();//değişiklikler kaydediliyor

            return Ok(ogrenciler);//ve geriye ogrenciler tablosu dönüyor
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OgrencilerExists(int id)
        {
            return db.Ogrencilers.Count(e => e.OgrenciId == id) > 0;
        }
    }
}