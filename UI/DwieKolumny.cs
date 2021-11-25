﻿using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class DwieKolumny : TableLayoutPanel
	{
		private int szerokoscEtykiet;
		private int szerokoscKontrolek;

		public DwieKolumny()
		{
			ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
		}

		protected override void OnCreateControl()
		{
			RowCount++;
			RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			base.OnCreateControl();
		}

		public void DodajWiersz(Control kontrolka, string etykieta)
		{
			szerokoscKontrolek = Math.Max(szerokoscKontrolek, kontrolka.GetPreferredSize(default).Width);

			RowCount++;
			RowStyles.Add(new RowStyle());

			if (!String.IsNullOrEmpty(etykieta))
			{
				var label = new Label();
				label.Anchor = AnchorStyles.Right;
				label.AutoSize = true;
				label.Text = etykieta;
				Controls.Add(label, 0, RowCount - 1);
				szerokoscEtykiet = Math.Max(szerokoscEtykiet, label.GetPreferredSize(default).Width);
			}

			Controls.Add(kontrolka, 1, RowCount - 1);

			var minimalnaSzerokosc = szerokoscEtykiet + szerokoscKontrolek + Margin.Left + Margin.Right + Padding.Left + Padding.Right;
			if (Width < minimalnaSzerokosc) Width = minimalnaSzerokosc;
		}

		public TextBox DodajTextBox(string etykieta)
		{
			var textBox = new TextBox();
			textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			DodajWiersz(textBox, etykieta);
			return textBox;
		}

		public CheckBox DodajCheckBox(string etykieta)
		{
			var checkBox = new CheckBox();
			checkBox.AutoSize = true;
			checkBox.Anchor = AnchorStyles.Left;
			checkBox.Text = etykieta;
			DodajWiersz(checkBox, null);
			return checkBox;
		}

		public NumericUpDown DodajNumericUpDown(string etykieta, int poprzecinku = 2)
		{
			var numericUpDown = new NumericUpDown();
			numericUpDown.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			numericUpDown.TextAlign = HorizontalAlignment.Right;
			numericUpDown.DecimalPlaces = poprzecinku;
			DodajWiersz(numericUpDown, etykieta);
			return numericUpDown;
		}

		public DateTimePicker DodajDatePicker(string etykieta)
		{
			var dateTimePicker = new DateTimePicker();
			dateTimePicker.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			DodajWiersz(dateTimePicker, etykieta);
			return dateTimePicker;
		}
	}
}
