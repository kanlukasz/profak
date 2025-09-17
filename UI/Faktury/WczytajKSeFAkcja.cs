﻿using ProFak.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class WczytajKSeFAkcja : DodajRekordAkcja<Faktura, FakturaEdytor>
	{
		public override string Nazwa => "➕ Wczytaj XML KSeF [CTRL-K]";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => true;
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => klawisz == Keys.K && modyfikatory == Keys.Control;

		protected override Faktura UtworzRekord(Kontekst kontekst, IEnumerable<Faktura> zaznaczoneRekordy)
		{
			using var dialog = new OpenFileDialog();
			dialog.Filter = "e-Faktura XML (*.xml)|*.xml|Wszystkie pliki (*.*)|*.*";
			dialog.Title = "Wybierz e-Fakturę do załadowania";
			dialog.RestoreDirectory = true;
			if (dialog.ShowDialog() != DialogResult.OK) return null;

			var podmiot = kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
			var xml = File.ReadAllText(dialog.FileName);
#if KSEF_1
			var faktura = IO.FA_2.Generator.ZbudujDB(kontekst.Baza, xml);
#else
			var faktura = IO.FA_3.Generator.ZbudujDB(kontekst.Baza, xml);
#endif
			faktura.DataKSeF = DateTime.Now;
			kontekst.Baza.Zapisz(faktura);
			return faktura;
		}
	}
}
