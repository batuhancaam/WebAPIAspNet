using MvcKismi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MvcKismi.Controllers
{
    public class OgrencilersController : Controller
    {
        // GET: Ogrencilers
        public ActionResult Index()//ActionResult metodu tanımlanıyor
        {
            IEnumerable<MvcOgrenciModel> ogrList;//MvcOgrenci Modeli icerisinde bulunan veri tabanı bir listeye alınıyor
            HttpResponseMessage response = ServiceDegiskenler.ServiceClient.GetAsync("Ogrencilers").Result;//Sonrasında Wep api kısmından veriler json tipinde Client kısmına GetAsync metodunun icine yazılan Ogrencilers ile WepApi kısmındaki controller icindeki veriler Get ediliyor
            ogrList = response.Content.ReadAsAsync<IEnumerable<MvcOgrenciModel>>().Result;//Sonrasında yukarıdaki olusturduğumuz veritabanı listesi içerisine ReadAsAsync metodu ile json tipinde veriyi okuyarak aktarıyoruz .
            return View(ogrList);//Sonrasında index View'ine bu json tipindeki okunmuş veriyi dönüyoruz ve bu View Get olarak okunan verileri görüntülüyot.
        }

        public ActionResult EkleVeyaDuzenle(int id = 0)//Bu kısımda EkleVeyaDuzenleme kısmının Get kısmı yazılıyor
        {
            if (id == 0)//eğer id 0 ise yani veri yoksa 
            {

                return View(new MvcOgrenciModel());//Direk olarak veritabanını dönecek
            }

            else
            {
                HttpResponseMessage response = ServiceDegiskenler.ServiceClient.GetAsync("Ogrencilers/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MvcOgrenciModel>().Result);//Eğer veri varsa bu verileri tekrardan yukarıdaki Index Aciton'ınındaki işlemlerle servis tabanlı olarak dönecek
            }
        }
        [HttpPost]
        public ActionResult EkleVeyaDuzenle(MvcOgrenciModel ogr)//Sonrasında post kısmı yani verileri client'dan server'a gönderme kısmına geciliyor parametre olarak Veritabanı sınıfından bir nesne geciliyor bu nesne ile yapılan veritabanı işlemleri kolaylıkla yapılacak

        {

            if (ogr.OgrenciId == 0)//Eğer ögrenci id 'si sıfır ise 
            {

                HttpResponseMessage response = ServiceDegiskenler.ServiceClient.PostAsJsonAsync("Ogrencilers", ogr).Result;//Clientten alınan veriyi WepApi kısmına gönderir ve PostAsJsonAsync metodu ile Wep Api kısmındaki Ogrencilers Contollers kısmına giderek post metodunu calıstırır ve
                                    //Json tipinde bu veriyi veritabanına ekleyip sonrasında kaydeder.
                
            }

            else
            {
                HttpResponseMessage response = ServiceDegiskenler.ServiceClient.PutAsJsonAsync("Ogrencilers/"+ogr.OgrenciId , ogr).Result;//Eğer Ogrenci Id'si sıfır değilse o zaman bir güncelleme işlemi yapılmak isteniyor demektir.Web api kısmındaki Ogrencilers controllerına gidip Put metodunu cağırır ve oraya OgrenciId ile birlikte gider ki
                //hangi ıd üstünde değişiklik yapıldığını bilsin sonrasında değişiklik işlemini yapar.
                


            }
            return RedirectToAction("Index");//Bütün bu işlemler yapıldıktan sonra EkleVeyaDuzenle View'inden direk index View'ine gidirilir o yüzden return olarak direk Index dönülüyor.

        }

        public ActionResult Delete(int id)//Bu metod ise Veritabanından silme işlemi yapar
        {
            HttpResponseMessage response = ServiceDegiskenler.ServiceClient.DeleteAsync("Ogrencilers/"+id.ToString() ).Result;//id ile birlikte Wep kısmındaki Delete Metoduna gidilir ve o metod calısarak silme işlemini gercekleştirir.Burada Json olarak bir metod olmamasının sebebi zaten bir veri guncelleme ekleme veya client tarafına gönderme
            //işlemi yapmadığımızdan direk olarak sildiğimizden Json tipinde bir veriyi gondermemize gerek yok.
            return RedirectToAction("Index");//Aynı şekilde veri silindikten sonra direk index sayfasına yönlediriliyor.
        }
    }
}