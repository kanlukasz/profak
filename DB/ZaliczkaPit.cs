﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class ZaliczkaPit : Rekord<ZaliczkaPit>
	{
		public DateTime Miesiac { get; set; } = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day).AddMonths(-1);

		public decimal Przychody { get; set; }
		public decimal Koszty { get; set; }
		public decimal SkladkiZus { get; set; }
		public decimal Podatek { get; set; }
		public decimal Przeniesiony { get; set; }
		public decimal DoPrzeniesienia { get; set; }
		public decimal DoWplaty { get; set; }

		public string MiesiacFmt => Miesiac.ToString("MM/yyyy");

		public List<Faktura> Faktury { get; set; }
	}
}
