﻿using ProFak.DB;
using ProFak.IO.JPK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class GenerujJPKAkcja : AkcjaNaSpisie<DeklaracjaVat>
	{
		public override string Nazwa => "Generuj JPK";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<DeklaracjaVat> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;

		public override void Uruchom(Kontekst kontekst, ref IEnumerable<DeklaracjaVat> zaznaczoneRekordy)
		{
			var deklaracja = zaznaczoneRekordy.Single();
			using var dialog = new SaveFileDialog();
			dialog.Filter = "Deklaracja JPK_V7M (*.xml)|*.xml|Wszystkie pliki (*.*)|*.*";
			dialog.Title = "Wybierz miejsce do zapisu JPK";
			dialog.RestoreDirectory = true;
			dialog.FileName = $"jpk-v7m-{deklaracja.Miesiac:yyyy-MM}.xml";
			if (dialog.ShowDialog() != DialogResult.OK) return;

			using var nowyKontekst = new Kontekst(kontekst);
			JPK_V7M.Utworz(dialog.FileName, nowyKontekst.Baza, deklaracja);
			MessageBox.Show("Plik został zapisany pomyślnie.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
