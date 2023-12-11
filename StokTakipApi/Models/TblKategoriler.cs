using System;
using System.Collections.Generic;

namespace StokTakipApi.Models;

public partial class TblKategoriler
{
    public int KategoriId { get; set; }

    public string KategoriAd { get; set; } = null!;

    public virtual ICollection<TblUrunler> TblUrunlers { get; set; } = new List<TblUrunler>();
}
