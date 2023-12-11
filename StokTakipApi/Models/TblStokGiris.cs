using System;
using System.Collections.Generic;

namespace StokTakipApi.Models;

public partial class TblStokGiris
{
    public int IslemId { get; set; }

    public int UrunId { get; set; }

    public int TedarikciId { get; set; }

    public double Adet { get; set; }

    public double BirimFiyat { get; set; }

    public DateTime Tarih { get; set; }

    public int PersonelId { get; set; }

    public bool? Silindi { get; set; }

    public virtual TblKullanicilar Personel { get; set; } = null!;

    public virtual TblTedarikciler Tedarikci { get; set; } = null!;

    public virtual TblUrunler Urun { get; set; } = null!;
}
