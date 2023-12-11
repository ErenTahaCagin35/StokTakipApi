using System;
using System.Collections.Generic;

namespace StokTakipApi.Models;

public partial class TblKullanicilar
{
    public int KullaniciId { get; set; }

    public string KullaniciAdi { get; set; } = null!;

    public string Parola { get; set; } = null!;

    public int? Yetki { get; set; }

    public virtual ICollection<TblStokCiki> TblStokCikis { get; set; } = new List<TblStokCiki>();

    public virtual ICollection<TblStokGiris> TblStokGirises { get; set; } = new List<TblStokGiris>();
}
