using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace MvcKismi
{
    public static class ServiceDegiskenler
    {
        public static HttpClient ServiceClient = new HttpClient();//Http Client kısmı olusturuluyor.Olusturduğumuz nesne ile işlemler yapacağız

        static ServiceDegiskenler()
        {
            ServiceClient.BaseAddress = new Uri("http://localhost:60378/api/");//Buraya Api'dan alacağımız bilgilerin tam olarak nerden alacağımızı belirtiyoz WepApiKısmı projesinin local host adresini girmemiz gerekiyor
            ServiceClient.DefaultRequestHeaders.Clear();//Sonrasında client tarafından okunan bilgileri önce her ihtimale karsı temizliyoruz
            ServiceClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//Sonrasında client tarafına bu bilgilerin bir json uygulaması tipinde geliceğini bildiriyoruz

        }

    }
}