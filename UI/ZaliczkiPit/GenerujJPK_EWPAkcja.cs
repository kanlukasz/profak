﻿using ProFak.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class GenerujJPK_EWPAkcja : AkcjaNaSpisie<ZaliczkaPit>
	{
		public override string Nazwa => "Generuj JPK_EWP";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<ZaliczkaPit> zaznaczoneRekordy) => zaznaczoneRekordy.Any();

		public override void Uruchom(Kontekst kontekst, ref IEnumerable<ZaliczkaPit> zaznaczoneRekordy)
		{
			using var dialog = new SaveFileDialog();
			dialog.Filter = "Deklaracja JPK_EWP (*.xml)|*.xml|Wszystkie pliki (*.*)|*.*";
			dialog.Title = "Wybierz miejsce do zapisu JPK";
			dialog.RestoreDirectory = true;
			if (zaznaczoneRekordy.Count() == 1) dialog.FileName = $"jpk-ewp-{zaznaczoneRekordy.Single().Miesiac:yyyy-MM}.xml";
			else dialog.FileName = $"jpk-ewp-{zaznaczoneRekordy.Min(e => e.Miesiac):yyyy-MM}-{zaznaczoneRekordy.Max(e => e.Miesiac):yyyy-MM}.xml";
			if (dialog.ShowDialog() != DialogResult.OK) return;

			using var nowyKontekst = new Kontekst(kontekst);
			foreach (var deklaracja in zaznaczoneRekordy) nowyKontekst.Dodaj(deklaracja);
			IO.JPK_EWP.Generator.Utworz(dialog.FileName, nowyKontekst.Baza, zaznaczoneRekordy);
			MessageBox.Show("Plik został zapisany pomyślnie.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
