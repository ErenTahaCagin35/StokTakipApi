using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StokTakipApi.Models;
using StokTakipApi.Protocol;

namespace StokTakipWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[action]")]
    public class StokTakipController : Controller
    {
        StokTakip1927Context context = new StokTakip1927Context();

        [HttpGet]
        public string Test()
        {
            return "Api ile Bağlantı çalıştı";
        }

        [HttpGet]
        public ApiCevap KullanicilariGetir()
        {
            ApiCevap cevap = new ApiCevap();
            var list = context.TblKullanicilars.ToList();

            cevap.BasariliMi = true;
            cevap.Data = list;

            return cevap;
        }

        [HttpGet]
        public ApiCevap KategorileriGetir()
        {
            ApiCevap cevap = new ApiCevap();
            var list = context.TblKategorilers.ToList();
            cevap.BasariliMi = true;
            cevap.Data = list;

            return cevap;
        }

        [HttpGet]
        public ApiCevap TedarikcileriGetir()
        {
            ApiCevap cevap = new ApiCevap();
            var list = context.TblTedarikcilers.ToList();

            cevap.BasariliMi = true;
            cevap.Data = list;

            return cevap;
        }

        [HttpPost]
        public ApiCevap KategoriEkle(string kategoriAdi)
        {
            ApiCevap cevap = new ApiCevap();
            TblKategoriler kategori = new TblKategoriler()
            {
                KategoriAd = kategoriAdi
            };

            context.TblKategorilers.Add(kategori);
            context.SaveChanges();

            cevap.BasariliMi = true;
            cevap.Data = kategori;

            return cevap;
        }


        [HttpPost]
        public ApiCevap KullaniciEkle(string kullaniciAdi, int yetkilendir, string sifre)
        {
            ApiCevap cevap = new ApiCevap();
            TblKullanicilar kullanici = new TblKullanicilar()
            {
                KullaniciAdi = kullaniciAdi,
                Yetki = yetkilendir,
                Parola = sifre
            };

            int karakter = sifre.Length;

            if (yetkilendir > 5)
            {
                cevap.BasariliMi = false;
                cevap.HataMesaji = "Hatalı yetkilendirme lütfen 5 in altında yetki giriniz.";
                return cevap;
            }
            if (karakter < 8 || sifre == null)
            {
                cevap.BasariliMi = false;
                cevap.HataMesaji = "Sifre bos veya 8 karakterin altında olamaz lütfen değiştirin..";
                return cevap;
            }

            context.TblKullanicilars.Add(kullanici);
            context.SaveChanges();

            cevap.BasariliMi = true;
            cevap.Data = kullanici;

            return cevap;
        }

        [HttpPost]
        public ApiCevap TedarikciEkle(string firmaAdi, string yetkili, string adres, string tel, string mail)
        {
            ApiCevap cevap = new ApiCevap();
            TblTedarikciler tedarikci = new TblTedarikciler()
            {
               FirmaAdi = firmaAdi,
               YetkiliAdSoyad = yetkili,
               Adres = adres,
               Tel = tel,
               Mail = mail
            };

            if (firmaAdi == null || yetkili == null)
            {
                cevap.BasariliMi = false;
                cevap.HataMesaji = "Firma adı veya yetkili kişiyi yazmayı unutmuş olabilirsiniz, lütfen tekrar kontrol ediniz.";
                return cevap;
            }
            if (adres == null || mail == null)
            {
                cevap.BasariliMi = false;
                cevap.HataMesaji = "Firmanın iletişim adreslerini yazmayı unutmuş olabilirsiniz, lütfen tekrar kontrol ediniz.";
                return cevap;
            }

            context.TblTedarikcilers.Add(tedarikci);
            context.SaveChanges();

            cevap.BasariliMi = true;
            cevap.Data = tedarikci;

            return cevap;
        }

        [HttpPost]
        public ApiCevap KategoriSil(int kategoriId)
        {
            ApiCevap cevap = new ApiCevap();

            var kategori = context.TblKategorilers.FirstOrDefault(x => x.KategoriId == kategoriId);

            if (kategori == null)//olmayan bir kategoriyi silemem
            {
                cevap.BasariliMi = false;
                cevap.HataMesaji = "Olmayan bir kategoriId gönderdiniz.";
                return cevap;
            }

            context.TblKategorilers.Remove(kategori);
            context.SaveChanges();
            cevap.BasariliMi = true;

            return cevap;

        }


        [HttpPost]
        public ApiCevap KullaniciSil(int kullaniciId)
        {
            ApiCevap cevap = new ApiCevap();

            var kullanici = context.TblKullanicilars.FirstOrDefault(x => x.KullaniciId == kullaniciId);

            if (kullanici == null)
            {
                cevap.BasariliMi = false;
                cevap.HataMesaji = "Olmayan bir kullaniciID gönderdiniz.";
                return cevap;
            }


            context.TblKullanicilars.Remove(kullanici);
            context.SaveChanges();
            cevap.BasariliMi = true;

            return cevap;

        }

        [HttpPost]
        public ApiCevap TedarikciSil(int tedarikciId)
        {
            ApiCevap cevap = new ApiCevap();

            var tedarikci = context.TblTedarikcilers.FirstOrDefault(x => x.TedarikciId == tedarikciId);

            if (tedarikci == null)
            {
                cevap.BasariliMi = false;
                cevap.HataMesaji = "Olmayan bir tedarikciID gönderdiniz.";
                return cevap;
            }


            context.TblTedarikcilers.Remove(tedarikci);
            context.SaveChanges();
            cevap.BasariliMi = true;

            return cevap;

        }

        [HttpPost]
        public ApiCevap KategoriGuncelle(int kategoriId, string kategoriAdi)
        {
            ApiCevap cevap = new ApiCevap();

            var kategori = context.TblKategorilers.FirstOrDefault(x => x.KategoriId == kategoriId);

            if (kategori == null)//olmayan bir kategoriyi güncelleyemem
            {
                cevap.BasariliMi = false;
                cevap.HataMesaji = "Olmayan bir kategoriId gönderdiniz.";
                return cevap;
            }

            kategori.KategoriAd = kategoriAdi;
            context.SaveChanges();
            cevap.BasariliMi = true;
            cevap.Data = kategori;

            return cevap;
        }
        [HttpPost]
        public ApiCevap KullaniciGuncelle(int kullaniciId, string kullaniciAdi, string sifre, int yetkilendir)
        {
            ApiCevap cevap = new ApiCevap();

            var kullanici = context.TblKullanicilars.FirstOrDefault(x => x.KullaniciId == kullaniciId);

            if (kullanici == null)
            {
                cevap.BasariliMi = false;
                cevap.HataMesaji = "Olmayan bir kullaniciID gönderdiniz.";
                return cevap;
            }
            int karakter = sifre.Length;

            if (yetkilendir > 5)
            {
                cevap.BasariliMi = false;
                cevap.HataMesaji = "Hatalı yetkilendirme lütfen 5 in altında yetki giriniz.";
                return cevap;
            }
            if (karakter < 8 || sifre == null)
            {
                cevap.BasariliMi = false;
                cevap.HataMesaji = "Sifre bos veya 8 karakterin altında olamaz lütfen değiştirin..";
                return cevap;
            }

            kullanici.KullaniciAdi = kullaniciAdi;
            kullanici.Yetki = yetkilendir;
            kullanici.Parola = sifre;
            context.SaveChanges();
            cevap.BasariliMi = true;
            cevap.Data = kullanici;

            return cevap;

        }
    }

}