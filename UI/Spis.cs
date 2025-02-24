﻿using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class Spis : DataGridView
	{
		public Spis()
		{
			DoubleBuffered = true;
			AllowUserToAddRows = false;
			AllowUserToDeleteRows = false;
			AllowUserToResizeRows = true;
			AllowUserToOrderColumns = true;
			RowHeadersVisible = true;
			RowHeadersWidth = 16;
			ShowCellToolTips = true;
			EnableHeadersVisualStyles = false;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button != MouseButtons.Left) return;
			var hit = HitTest(e.X, e.Y);
			if (hit.Type == DataGridViewHitTestType.ColumnHeader || hit.Type == DataGridViewHitTestType.RowHeader) DoubleBuffered = false;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (!DoubleBuffered) DoubleBuffered = true;
		}
	}

	abstract class Spis<T> : Spis
		where T : Rekord<T>
	{
		private const string WYSOKOSC_WIERSZA = "(wysokość)";
		private readonly Container container;
		private readonly BindingSource bindingSource;
		private IEnumerable<T> oryginalneRekordy;
		private Func<T, bool> filtr;
		private List<(string kolumna, bool malejaco, Func<T, IComparable> metoda)> kolumnyKolejnosci;
		private Dictionary<int, Func<T, string>> tooltipyDlaKolumn;
		private bool rekordyPodczasZmiany;
		private bool kolumnyZmienione;

		public Kontekst Kontekst { get; set; }
		public IEnumerable<T> WybraneRekordy
		{
			get => SelectedRows.Cast<DataGridViewRow>().Select(row => row.DataBoundItem).Cast<T>();

			set
			{
				var pierwszy = true;
				foreach (DataGridViewRow row in Rows)
				{
					if (!(row.DataBoundItem is T rekord)) continue;
					var wybrany = value.Contains(rekord);
					row.Selected = wybrany;
					if (pierwszy && wybrany)
					{
						CurrentCell = row.Cells.Cast<DataGridViewCell>().FirstOrDefault(e => e.Visible);
						pierwszy = false;
					}
				}
			}
		}

		public IEnumerable<T> Rekordy
		{
			get => bindingSource.DataSource as IEnumerable<T>;
			set
			{
				var zaznaczoneRekordy = WybraneRekordy.ToList();
				oryginalneRekordy = value;
				var rekordy = Sortuj(value.Where(filtr)).ToList();
				rekordyPodczasZmiany = true;
				bindingSource.DataSource = rekordy;
				rekordyPodczasZmiany = false;
				RekordyZmienione?.Invoke();
				WybraneRekordy = zaznaczoneRekordy;
				ZaznaczPosortowaneKolumny();
			}
		}

		public virtual string Podsumowanie
		{
			get
			{
				var liczbaWszystkich = Rekordy.Count();
				var liczbaWybranych = WybraneRekordy.Count();
				string tekst;
				if (liczbaWszystkich == 0) tekst = "Spis nie zawiera danych";
				else tekst = $"Liczba pozycji: <{liczbaWszystkich}>";
				if (liczbaWybranych > 1) tekst += $"\nLiczba zaznaczonych: <{liczbaWybranych}>";
				return tekst;
			}
		}

		public event Action RekordyZmienione;
		public event Action ZaznaczenieZmienione;

		public Ref<T> RekordPoczatkowy { get; set; }

		public Spis()
		{
			AutoGenerateColumns = false;
			ReadOnly = true;
			SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			TabIndex = 50;

			kolumnyKolejnosci = new List<(string kolumna, bool malejaco, Func<T, IComparable> metoda)>();
			filtr = x => true;

			container = new Container();
			bindingSource = new BindingSource(container);
			bindingSource.DataSource = typeof(T);
			DataSource = bindingSource;
			MinimumSize = new System.Drawing.Size(500, 100);
			Rows.CollectionChanged += Rows_CollectionChanged;
		}

		private void Rows_CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if (RekordPoczatkowy == default) return;

			foreach (DataGridViewRow row in Rows)
			{
				if (row.DataBoundItem is not T rekord) continue;
				if (rekord.Ref != RekordPoczatkowy) continue;
				bindingSource.Position = row.Index;
				break;
			}
		}

		private void OdswiezWiersze()
		{
			Rekordy = oryginalneRekordy;
		}

		protected override void OnBindingContextChanged(EventArgs e)
		{
			base.OnBindingContextChanged(e);
			ZaznaczPosortowaneKolumny();
		}

		protected override void OnSelectionChanged(EventArgs e)
		{
			if (rekordyPodczasZmiany) return;
			if (bindingSource.DataSource == null) return;
			ZaznaczenieZmienione?.Invoke();
			base.OnSelectionChanged(e);
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			if (Kontekst != null) PrzeladujBezpiecznie();
			bindingSource.ResetBindings(true);
			WczytajKonfiguracje();
		}

		public void PrzeladujBezpiecznie()
		{
			try
			{
				Przeladuj();
			}
			catch (Exception exc)
			{
				OknoBledu.Pokaz(exc);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) container.Dispose();
			base.Dispose(disposing);
		}

		public DataGridViewTextBoxColumn DodajKolumne(string wlasciwosc, string naglowek, bool wyrownajDoPrawej = false, bool rozciagnij = false, string format = null, int? szerokosc = null, Func<T, string> tooltip = null)
		{
			var kolumna = new DataGridViewTextBoxColumn();
			kolumna.HeaderText = naglowek;
			kolumna.DataPropertyName = wlasciwosc;
			kolumna.Name = wlasciwosc;
			kolumna.DefaultCellStyle.Alignment = wyrownajDoPrawej ? DataGridViewContentAlignment.MiddleRight : DataGridViewContentAlignment.MiddleLeft;
			kolumna.AutoSizeMode = rozciagnij ? DataGridViewAutoSizeColumnMode.Fill : DataGridViewAutoSizeColumnMode.NotSet;
			kolumna.Visible = szerokosc != 0;
			if (!String.IsNullOrEmpty(format)) kolumna.DefaultCellStyle.Format = format;
			if (szerokosc.HasValue) kolumna.Width = szerokosc.Value * DeviceDpi / 96;
			if (rozciagnij) kolumna.MinimumWidth = 50;
			Columns.Add(kolumna);
			if (tooltip != null)
			{
				if (tooltipyDlaKolumn == null) tooltipyDlaKolumn = new Dictionary<int, Func<T, string>>();
				tooltipyDlaKolumn[kolumna.Index] = tooltip;
			}
			return kolumna;
		}

		public DataGridViewCheckBoxColumn DodajKolumneBool(string wlasciwosc, string naglowek, int? szerokosc = null, Func<T, string> tooltip = null)
		{
			var kolumna = new DataGridViewCheckBoxColumn();
			kolumna.HeaderText = naglowek;
			kolumna.DataPropertyName = wlasciwosc;
			kolumna.Name = wlasciwosc;
			if (szerokosc.HasValue) kolumna.Width = szerokosc.Value * DeviceDpi / 96;
			kolumna.SortMode = DataGridViewColumnSortMode.Programmatic;
			kolumna.Visible = szerokosc != 0;
			Columns.Add(kolumna);
			if (tooltip != null)
			{
				if (tooltipyDlaKolumn == null) tooltipyDlaKolumn = new Dictionary<int, Func<T, string>>();
				tooltipyDlaKolumn[kolumna.Index] = tooltip;
			}
			return kolumna;
		}

		public DataGridViewTextBoxColumn DodajKolumneData(string wlasciwosc, string naglowek, string format = Format.Data, int? szerokosc = 120, Func<T, string> tooltip = null) => DodajKolumne(wlasciwosc, naglowek, wyrownajDoPrawej: true, format: format, szerokosc: szerokosc, tooltip: tooltip);
		public DataGridViewTextBoxColumn DodajKolumneKwota(string wlasciwosc, string naglowek, string format = Format.Kwota, int? szerokosc = 80, Func<T, string> tooltip = null) => DodajKolumne(wlasciwosc, naglowek, wyrownajDoPrawej: true, format: format, szerokosc: szerokosc, tooltip: tooltip);
		public DataGridViewTextBoxColumn DodajKolumneId() => DodajKolumne("Id", "Id", wyrownajDoPrawej: true, szerokosc: 60);

		protected abstract void Przeladuj();

		protected override void OnCellToolTipTextNeeded(DataGridViewCellToolTipTextNeededEventArgs e)
		{
			if (tooltipyDlaKolumn != null && e.RowIndex != -1 && tooltipyDlaKolumn.TryGetValue(e.ColumnIndex, out var tooltip)) e.ToolTipText = tooltip((T)Rows[e.RowIndex].DataBoundItem);
			else base.OnCellToolTipTextNeeded(e);
		}

		protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
		{
			base.OnCellPainting(e);
			if (e.RowIndex == -1)
			{
				e.CellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
			}
			else if (e.ColumnIndex != -1)
			{
				if (Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending) e.CellStyle.BackColor = Color.FromArgb(210, 242, 167);
				else if (Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending) e.CellStyle.BackColor = Color.FromArgb(242, 219, 167);

				UstawStylWiersza((T)Rows[e.RowIndex].DataBoundItem, Columns[e.ColumnIndex].DataPropertyName, e.CellStyle);
			}
		}

		protected virtual void UstawStylWiersza(T rekord, string kolumna, DataGridViewCellStyle styl)
		{
		}

		protected override void OnCellClick(DataGridViewCellEventArgs e)
		{
			base.OnCellClick(e);
			if (e.ColumnIndex == -1)
			{
				SelectionMode = DataGridViewSelectionMode.FullRowSelect;
				if (e.RowIndex != -1) Rows[e.RowIndex].Selected = true;
			}
			else if (e.RowIndex == -1)
			{
				UstawKolejnosc(Columns[e.ColumnIndex].DataPropertyName, ModifierKeys != Keys.Control && ModifierKeys != Keys.Shift);
				OdswiezWiersze();
				ZaznaczPosortowaneKolumny();
				kolumnyZmienione = true;
			}
		}

		protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
		{
			if ((ModifierKeys & Keys.Alt) == Keys.Alt)
			{
				SelectionMode = DataGridViewSelectionMode.CellSelect;
			}
			if (e.Button == MouseButtons.Right && e.RowIndex == -1)
			{
				PokazKonfiguracjeSpisu();
			}
			base.OnCellMouseDown(e);
		}

		public override DataObject GetClipboardContent()
		{
			var dataObject = base.GetClipboardContent();
			if (dataObject == null) return null;

			var poprawiony = new DataObject();
			void PoprawFormatowanie(TextDataFormat format)
			{
				if (!dataObject.ContainsText(format)) return;
				var tekst = dataObject.GetText(format);
				if (tekst == null) return;
				tekst = tekst.Replace("\u00A0", "");
				poprawiony.SetText(tekst, format);
			}

			PoprawFormatowanie(TextDataFormat.Text);
			PoprawFormatowanie(TextDataFormat.UnicodeText);
			PoprawFormatowanie(TextDataFormat.CommaSeparatedValue);
			// Bez HTML

			return poprawiony;
		}

		private void PokazKonfiguracjeSpisu()
		{
			using var nowyKontekst = new Kontekst(Kontekst);
			using var transakcja = nowyKontekst.Transakcja();

			var spis = GetType().Name;
			var kolumny = Kontekst.Baza.KolumnySpisow.Where(e => e.Spis == spis).ToList();
			foreach (DataGridViewColumn kolumna in Columns)
			{
				if (kolumny.Any(e => e.Kolumna == kolumna.Name)) continue;
				var konfiguracjaKolumny = new KolumnaSpisu { Spis = spis, Kolumna = kolumna.Name };
				konfiguracjaKolumny.Kolejnosc = kolumna.DisplayIndex;
				konfiguracjaKolumny.Szerokosc = kolumna.Visible ? kolumna.AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill ? -1 : kolumna.Width : 0;
				kolumny.Add(konfiguracjaKolumny);
			}

			using var edytor = new KonfiguracjaSpisu(kolumny);
			using var okno = new Dialog("Konfiguracja spisu", edytor, nowyKontekst);
			okno.Przyciski.Controls.Add(new Button()
			{
				AutoSize = true,
				Text = "Przywróć domyślne ustawienia",
				DialogResult = DialogResult.Ignore
			});
			var wynik = okno.ShowDialog();
			if (wynik == DialogResult.Cancel) return;
			if (wynik == DialogResult.OK) nowyKontekst.Baza.Zapisz(kolumny);
			if (wynik == DialogResult.Ignore)
			{
				nowyKontekst.Baza.Usun(kolumny.Where(e => e.Id > 0));
				MessageBox.Show("Domyślne ustawienia zostaną załadowane po ponownym wyświetleniu spisu." , "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			transakcja.Zatwierdz();
			WczytajKonfiguracje();
		}

		public void UstawFiltr(string wyrazenieFiltra)
		{
			if (String.IsNullOrWhiteSpace(wyrazenieFiltra))
			{
				filtr = rekord => true;
			}
			else
			{
				var fragmenty = Regex.Matches(wyrazenieFiltra, @"(?:[^\s""]+|""[^""]*"")+");
				List<Func<T, bool>> dopasowania = new List<Func<T, bool>>();
				foreach (Match fragment in fragmenty)
				{
					if (!fragment.Success) continue;
					var fraza = fragment.Value;
					Func<T, bool> dopasowanieFragmentu = rekord => rekord.CzyPasuje(fraza);
					dopasowania.Add(dopasowanieFragmentu);
				}
				filtr = (Func<T, bool>)Delegate.Combine(dopasowania.ToArray());
			}

			Rekordy = oryginalneRekordy;
		}

		protected virtual Func<T, IComparable> KolumnaDlaSortowania(string kolumna)
		{
			var getter = typeof(T).GetProperty(kolumna)?.GetGetMethod();
			if (getter == null) return null;
			if (!getter.ReturnType.IsAssignableTo(typeof(IComparable))) return null;
			var parametr = Expression.Parameter(typeof(T), "rekord");
			var wartoscExpr = Expression.Call(parametr, getter);
			var wartoscCompExpr = Expression.Convert(wartoscExpr, typeof(IComparable));
			var lambdaExpr = Expression.Lambda<Func<T, IComparable>>(wartoscCompExpr, parametr);
			var metoda = lambdaExpr.Compile();
			return metoda;
		}

		public void UstawKolejnosc(string kolumna, bool zastap)
		{
			var getter = KolumnaDlaSortowania(kolumna);
			if (getter == null) return;

			bool dodaj;
			if (zastap)
			{
				for (int i = 0; i < kolumnyKolejnosci.Count; i++)
				{
					if (kolumnyKolejnosci[i].kolumna == kolumna)
					{
						kolumnyKolejnosci[i] = (kolumna, !kolumnyKolejnosci[i].malejaco, kolumnyKolejnosci[i].metoda);
						zastap = false;
						break;
					}
				}

				if (zastap)
				{
					kolumnyKolejnosci.Clear();
					dodaj = true;
				}
				else
				{
					dodaj = false;
				}
			}
			else
			{
				dodaj = true;
				for (int i = 0; i < kolumnyKolejnosci.Count; i++)
				{
					if (kolumnyKolejnosci[i].kolumna == kolumna)
					{
						kolumnyKolejnosci[i] = (kolumna, !kolumnyKolejnosci[i].malejaco, kolumnyKolejnosci[i].metoda);
						dodaj = false;
						break;
					}
				}
			}

			if (dodaj)
			{
				kolumnyKolejnosci.Add((kolumna, false, getter));
			}
		}

		private IEnumerable<T> Sortuj(IEnumerable<T> rekordy)
		{
			if (kolumnyKolejnosci.Count == 0) return rekordy;

			var posortowane = rekordy.OrderBy(r => 0);
			foreach (var kolumna in kolumnyKolejnosci)
			{
				if (kolumna.malejaco) posortowane = posortowane.ThenByDescending(kolumna.metoda);
				else posortowane = posortowane.ThenBy(kolumna.metoda);
			}

			return posortowane;
		}

		private void ZaznaczPosortowaneKolumny()
		{
			foreach (DataGridViewColumn kolumna in Columns)
			{
				kolumna.HeaderCell.SortGlyphDirection = SortOrder.None;
			}

			foreach (var kolumna in kolumnyKolejnosci)
			{
				Columns[kolumna.kolumna].HeaderCell.SortGlyphDirection = kolumna.malejaco ? SortOrder.Descending : SortOrder.Ascending;
			}
		}

		protected override void OnColumnDisplayIndexChanged(DataGridViewColumnEventArgs e)
		{
			base.OnColumnDisplayIndexChanged(e);
			kolumnyZmienione = true;
		}

		protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
		{
			base.OnColumnWidthChanged(e);
			kolumnyZmienione = true;
		}

		protected override void OnColumnHeadersHeightChanged(EventArgs e)
		{
			base.OnColumnHeadersHeightChanged(e);
			if (kolumnyZmienione) return;
			kolumnyZmienione = true;
			var wysokosc = ColumnHeadersHeight;
			foreach (DataGridViewRow row in Rows) row.Height = wysokosc;
		}

		protected override void OnRowHeightChanged(DataGridViewRowEventArgs e)
		{
			base.OnRowHeightChanged(e);
			if (e.Row.Index == -1) return;
			if (kolumnyZmienione) return;
			kolumnyZmienione = true;
			var wysokosc = e.Row.Height;
			foreach (DataGridViewRow row in Rows) row.Height = wysokosc;
			ColumnHeadersHeight = wysokosc;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (kolumnyZmienione)
			{
				kolumnyZmienione = false;
				ZapiszKonfiguracje();
			}
		}

		private void WczytajKonfiguracje()
		{
			var spis = GetType().Name;
			var kolumny = Kontekst.Baza.KolumnySpisow.Where(e => e.Spis == spis).OrderBy(e => e.Kolejnosc);
			kolumnyKolejnosci.Clear();

			foreach (var kolumna in kolumny.Where(e => e.PoziomSortowania != 0).OrderBy(e => Math.Abs(e.PoziomSortowania)))
			{
				UstawKolejnosc(kolumna.Kolumna, false);
				if (kolumna.PoziomSortowania < 0) UstawKolejnosc(kolumna.Kolumna, false);
			}

			if (kolumnyKolejnosci.Any() && oryginalneRekordy != null) OdswiezWiersze();

			foreach (var kolumna in kolumny)
			{
				if (kolumna.Kolumna == WYSOKOSC_WIERSZA)
				{
					foreach (DataGridViewRow row in Rows) row.Height = kolumna.Szerokosc;
					RowTemplate.Height = kolumna.Szerokosc;
					ColumnHeadersHeight = kolumna.Szerokosc;
					continue;
				}
				var kolumnaSpisu = Columns[kolumna.Kolumna];
				if (kolumnaSpisu == null) continue;
				if (kolumna.Szerokosc > 0)
				{
					kolumnaSpisu.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
					kolumnaSpisu.Width = kolumna.Szerokosc;
				}
				if (kolumna.Szerokosc == -1) kolumnaSpisu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				kolumnaSpisu.Visible = kolumna.Szerokosc != 0;
				kolumnaSpisu.DisplayIndex = kolumna.Kolejnosc;
				kolumnaSpisu.HeaderCell.SortGlyphDirection = kolumna.PoziomSortowania < 0 ? SortOrder.Descending : kolumna.PoziomSortowania > 0 ? SortOrder.Ascending : SortOrder.None;
			}
			kolumnyZmienione = false;
		}

		private void ZapiszKonfiguracje()
		{
			var spis = GetType().Name;
			var stareKolumny = Kontekst.Baza.KolumnySpisow.Where(e => e.Spis == spis).ToList();
			Kontekst.Baza.Usun(stareKolumny);

			var sortowanie = new Dictionary<string, int>();
			for (var i = 0; i < kolumnyKolejnosci.Count; i++)
			{
				sortowanie[kolumnyKolejnosci[i].kolumna] = (i + 1) * (kolumnyKolejnosci[i].malejaco ? -1 : 1);
			}

			var doZapisu = new List<KolumnaSpisu>();
			doZapisu.Add(new KolumnaSpisu { Spis = spis, Kolumna = WYSOKOSC_WIERSZA, Kolejnosc = -1, Szerokosc = ColumnHeadersHeight });
			foreach (DataGridViewColumn kolumna in Columns)
			{
				var konfiguracjaKolumny = new KolumnaSpisu { Spis = spis, Kolumna = kolumna.Name };
				konfiguracjaKolumny.Kolejnosc = kolumna.DisplayIndex;
				konfiguracjaKolumny.Szerokosc = kolumna.Visible ? kolumna.AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill ? -1 : kolumna.Width : 0;
				konfiguracjaKolumny.PoziomSortowania = sortowanie.GetValueOrDefault(kolumna.Name);
				doZapisu.Add(konfiguracjaKolumny);
			}
			Kontekst.Baza.Zapisz(doZapisu);
		}
	}
}
