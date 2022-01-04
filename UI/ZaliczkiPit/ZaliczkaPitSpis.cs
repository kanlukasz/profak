﻿using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class ZaliczkaPitSpis : Spis<ZaliczkaPit>
	{
		public override string Podsumowanie
		{
			get
			{
				var podsumowanie = base.Podsumowanie;
				if (WybraneRekordy.Count() > 1)
				{
					podsumowanie += $"\nRazem: {WybraneRekordy.Sum(zaliczka => zaliczka.Podatek).ToString("n2")}";
				}
				return podsumowanie;
			}
		}

		public ZaliczkaPitSpis()
		{
			DodajKolumne(nameof(ZaliczkaPit.MiesiacFmt), "Miesiąc");
			DodajKolumneKwota(nameof(ZaliczkaPit.Przychody), "Przychody");
			DodajKolumneKwota(nameof(ZaliczkaPit.Koszty), "Koszty");
			DodajKolumneKwota(nameof(ZaliczkaPit.DoWplaty), "Do wpłaty");
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.ZaliczkiPit.OrderBy(deklaracja => deklaracja.Miesiac).ToList();
		}
	}
}
