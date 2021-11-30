﻿using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class Spisy
	{
		public static TRekord Wybierz<TRekord>(Kontekst kontekst, Func<SpisZAkcjami<TRekord>> generatorSpisu, string tytul, Ref<TRekord> biezacaWartosc)
			where TRekord : Rekord<TRekord>
		{
			var wybor = new WybierzRekordAkcja<TRekord>();
			using var nowyKontekst = new Kontekst(kontekst);
			using var transakcja = nowyKontekst.Transakcja();
			using var spis = generatorSpisu();
			using var dialog = new Dialog(tytul, spis, nowyKontekst);
			spis.Akcje.Insert(0, wybor);
			spis.Spis.Kontekst = nowyKontekst;
			spis.Spis.RekordPoczatkowy = biezacaWartosc;
			dialog.CzyPrzyciskiWidoczne = false;
			dialog.Size = new System.Drawing.Size(800, 450);
			if (dialog.ShowDialog() != DialogResult.OK) return default;
			transakcja.Zatwierdz();
			return wybor.WybranyRekord;
		}

		public static SpisZAkcjami<Faktura, FakturaSprzedazySpis> FakturySprzedazy()
		{
			return Utworz(new FakturaSprzedazySpis(),
				new FakturaSprzedazyAkcja(),
				new FakturaProformaAkcja(),
				new KorektaFakturyAkcja(),
				new EdytujRekordAkcja<Faktura, FakturaEdytor>("Edycja faktury"/*, pelnyEkran: true */),
				new UsunRekordAkcja<Faktura>()
			);
		}

		public static SpisZAkcjami<Faktura, FakturaZakupuSpis> FakturyZakupu()
		{
			return Utworz(new FakturaZakupuSpis(),
				new DodajRekordAkcja<Faktura, FakturaEdytor>("Nowa faktura zakupu", faktura => faktura.Rodzaj = RodzajFaktury.Zakup/*, pelnyEkran: true */),
				new KorektaFakturyAkcja(),
				new EdytujRekordAkcja<Faktura, FakturaEdytor>("Edycja faktury"/*, pelnyEkran: true */),
				new UsunRekordAkcja<Faktura>()
			);
		}

		public static SpisZAkcjami<JednostkaMiary, JednostkaMiarySpis> JednostkiMiar()
		{
			return Utworz(new JednostkaMiarySpis(),
				new DodajRekordAkcja<JednostkaMiary, JednostkaMiaryEdytor>("Nowa jednostka miary"),
				new EdytujRekordAkcja<JednostkaMiary, JednostkaMiaryEdytor>("Edycja jednostki miary"),
				new UsunRekordAkcja<JednostkaMiary>()
			);
		}

		public static SpisZAkcjami<Kontrahent, KontrahentSpis> Kontrahenci()
		{
			return Utworz(new KontrahentSpis(),
				new DodajRekordAkcja<Kontrahent, KontrahentEdytor>("Nowy kontrahent"),
				new EdytujRekordAkcja<Kontrahent, KontrahentEdytor>("Edycja kontrahenta"),
				new UsunRekordAkcja<Kontrahent>()
			);
		}

		public static SpisZAkcjami<Numerator, NumeratorSpis> Numeratory()
		{
			return Utworz(new NumeratorSpis(),
				new DodajRekordAkcja<Numerator, NumeratorEdytor>("Nowy numerator"),
				new EdytujRekordAkcja<Numerator, NumeratorEdytor>("Edycja numeratora"),
				new UsunRekordAkcja<Numerator>()
			);
		}

		public static SpisZAkcjami<Plik, PlikSpis> Pliki()
		{
			var spis = new PlikSpis();
			return Utworz(spis,
				new DodajPlikAction(spis),
				new PokazPlikAction(),
				new UsunRekordAkcja<Plik>()
			);
		}

		public static SpisZAkcjami<PozycjaFaktury, PozycjaFakturySpis> PozycjeFaktur()
		{
			var spis = new PozycjaFakturySpis();
			return Utworz(spis,
				new DodajRekordAkcja<PozycjaFaktury, PozycjaFakturyEdytor>("Nowa pozycja", pozycja => pozycja.FakturaRef = spis.FakturaRef),
				new EdytujRekordAkcja<PozycjaFaktury, PozycjaFakturyEdytor>("Edycja pozycji"),
				new UsunRekordAkcja<PozycjaFaktury>()
			);
		}

		public static SpisZAkcjami<SposobPlatnosci, SposobPlatnosciSpis> SposobyPlatnosci()
		{
			return Utworz(new SposobPlatnosciSpis(),
				new DodajRekordAkcja<SposobPlatnosci, SposobPlatnosciEdytor>("Nowy sposób płatności"),
				new EdytujRekordAkcja<SposobPlatnosci, SposobPlatnosciEdytor>("Edycja sposobu płatności"),
				new UsunRekordAkcja<SposobPlatnosci>()
			);
		}

		public static SpisZAkcjami<StanNumeratora, StanNumeratoraSpis> StanyNumeratorow()
		{
			var spis = new StanNumeratoraSpis();
			return Utworz(spis,
				new DodajRekordAkcja<StanNumeratora, StanNumeratoraEdytor>("Nowy stan", stanNumeratora => stanNumeratora.NumeratorRef = spis.NumeratorRef),
				new EdytujRekordAkcja<StanNumeratora, StanNumeratoraEdytor>("Edycja stanu"),
				new UsunRekordAkcja<StanNumeratora>()
			);
		}

		public static SpisZAkcjami<StawkaVat, StawkaVatSpis> StawkiVat()
		{
			return Utworz(new StawkaVatSpis(),
				new DodajRekordAkcja<StawkaVat, StawkaVatEdytor>("Nowa stawka VAT"),
				new EdytujRekordAkcja<StawkaVat, StawkaVatEdytor>("Edycja stawki VAT"),
				new UsunRekordAkcja<StawkaVat>()
			);
		}

		public static SpisZAkcjami<Towar, TowarSpis> Towary()
		{
			return Utworz(new TowarSpis(),
				new DodajRekordAkcja<Towar, TowarEdytor>("Nowy towar"),
				new EdytujRekordAkcja<Towar, TowarEdytor>("Edycja towaru"),
				new UsunRekordAkcja<Towar>()
			);
		}

		public static SpisZAkcjami<Waluta, WalutaSpis> Waluty()
		{
			return Utworz(new WalutaSpis(),
				new DodajRekordAkcja<Waluta, WalutaEdytor>("Nowa waluta"),
				new EdytujRekordAkcja<Waluta, WalutaEdytor>("Edycja waluty"),
				new UsunRekordAkcja<Waluta>()
			);
		}

		public static SpisZAkcjami<Wplata, WplataSpis> Wplaty()
		{
			var spis = new WplataSpis();
			return Utworz(spis,
				new DodajRekordAkcja<Wplata, WplataEdytor>("Nowa wpłata", wplata => wplata.FakturaRef = spis.FakturaRef),
				new EdytujRekordAkcja<Wplata, WplataEdytor>("Edycja wpłaty"),
				new UsunRekordAkcja<Wplata>()
			);
		}

		private static SpisZAkcjami<TRekord, TSpis> Utworz<TRekord, TSpis>(TSpis spis, params AkcjaNaSpisie<TRekord>[] akcje)
			where TRekord : Rekord<TRekord>
			where TSpis : Spis<TRekord>
		{
			var okno = new SpisZAkcjami<TRekord, TSpis>(spis);
			okno.Akcje.AddRange(akcje);
			return okno;
		}
	}
}
