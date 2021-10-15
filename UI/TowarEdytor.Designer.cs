﻿
namespace ProFak.UI
{
	partial class TowarEdytor
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.comboBoxStawkaVat = new System.Windows.Forms.ComboBox();
			this.bindingSourceStawkaVat = new System.Windows.Forms.BindingSource(this.components);
			this.buttonStawkaVat = new System.Windows.Forms.Button();
			this.comboBoxWidocznosc = new System.Windows.Forms.ComboBox();
			this.numericUpDownCenaBrutto = new System.Windows.Forms.NumericUpDown();
			this.comboBoxJednostkaMiary = new System.Windows.Forms.ComboBox();
			this.bindingSourceJednostkaMiary = new System.Windows.Forms.BindingSource(this.components);
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxSposobLiczenia = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.comboBoxRodzaj = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBoxNazwa = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDownCenaNetto = new System.Windows.Forms.NumericUpDown();
			this.buttonJednostkaMiary = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
			this.tableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSourceStawkaVat)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownCenaBrutto)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSourceJednostkaMiary)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownCenaNetto)).BeginInit();
			this.SuspendLayout();
			// 
			// bindingSource
			// 
			this.bindingSource.DataSource = typeof(ProFak.DB.Towar);
			this.bindingSource.DataSourceChanged += new System.EventHandler(this.bindingSource_DataSourceChanged);
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.Controls.Add(this.comboBoxStawkaVat, 1, 3);
			this.tableLayoutPanel.Controls.Add(this.buttonStawkaVat, 2, 3);
			this.tableLayoutPanel.Controls.Add(this.comboBoxWidocznosc, 1, 7);
			this.tableLayoutPanel.Controls.Add(this.numericUpDownCenaBrutto, 1, 5);
			this.tableLayoutPanel.Controls.Add(this.comboBoxJednostkaMiary, 1, 6);
			this.tableLayoutPanel.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.comboBoxSposobLiczenia, 1, 2);
			this.tableLayoutPanel.Controls.Add(this.label3, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.label4, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.label5, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.label6, 0, 4);
			this.tableLayoutPanel.Controls.Add(this.label7, 0, 5);
			this.tableLayoutPanel.Controls.Add(this.comboBoxRodzaj, 1, 1);
			this.tableLayoutPanel.Controls.Add(this.label8, 0, 6);
			this.tableLayoutPanel.Controls.Add(this.textBoxNazwa, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.label1, 0, 7);
			this.tableLayoutPanel.Controls.Add(this.numericUpDownCenaNetto, 1, 4);
			this.tableLayoutPanel.Controls.Add(this.buttonJednostkaMiary, 2, 6);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 9;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(300, 243);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// comboBoxStawkaVat
			// 
			this.comboBoxStawkaVat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxStawkaVat.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSource, "StawkaVatRef", true));
			this.comboBoxStawkaVat.DataSource = this.bindingSourceStawkaVat;
			this.comboBoxStawkaVat.DisplayMember = "Skrot";
			this.comboBoxStawkaVat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxStawkaVat.FormattingEnabled = true;
			this.comboBoxStawkaVat.Location = new System.Drawing.Point(149, 91);
			this.comboBoxStawkaVat.Name = "comboBoxStawkaVat";
			this.comboBoxStawkaVat.Size = new System.Drawing.Size(116, 23);
			this.comboBoxStawkaVat.TabIndex = 4;
			this.comboBoxStawkaVat.ValueMember = "Ref";
			// 
			// bindingSourceStawkaVat
			// 
			this.bindingSourceStawkaVat.DataSource = typeof(ProFak.DB.StawkaVat);
			// 
			// buttonStawkaVat
			// 
			this.buttonStawkaVat.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.buttonStawkaVat.AutoSize = true;
			this.buttonStawkaVat.Location = new System.Drawing.Point(271, 90);
			this.buttonStawkaVat.Name = "buttonStawkaVat";
			this.buttonStawkaVat.Size = new System.Drawing.Size(26, 25);
			this.buttonStawkaVat.TabIndex = 5;
			this.buttonStawkaVat.Text = "...";
			this.buttonStawkaVat.UseVisualStyleBackColor = true;
			this.buttonStawkaVat.Click += new System.EventHandler(this.buttonStawkaVat_Click);
			// 
			// comboBoxWidocznosc
			// 
			this.comboBoxWidocznosc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.SetColumnSpan(this.comboBoxWidocznosc, 2);
			this.comboBoxWidocznosc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxWidocznosc.FormattingEnabled = true;
			this.comboBoxWidocznosc.Items.AddRange(new object[] {
            "widoczny",
            "ukryty"});
			this.comboBoxWidocznosc.Location = new System.Drawing.Point(149, 210);
			this.comboBoxWidocznosc.Name = "comboBoxWidocznosc";
			this.comboBoxWidocznosc.Size = new System.Drawing.Size(148, 23);
			this.comboBoxWidocznosc.TabIndex = 10;
			// 
			// numericUpDownCenaBrutto
			// 
			this.numericUpDownCenaBrutto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.SetColumnSpan(this.numericUpDownCenaBrutto, 2);
			this.numericUpDownCenaBrutto.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource, "CenaBrutto", true));
			this.numericUpDownCenaBrutto.DecimalPlaces = 2;
			this.numericUpDownCenaBrutto.Location = new System.Drawing.Point(149, 150);
			this.numericUpDownCenaBrutto.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.numericUpDownCenaBrutto.Name = "numericUpDownCenaBrutto";
			this.numericUpDownCenaBrutto.Size = new System.Drawing.Size(148, 23);
			this.numericUpDownCenaBrutto.TabIndex = 7;
			this.numericUpDownCenaBrutto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// comboBoxJednostkaMiary
			// 
			this.comboBoxJednostkaMiary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxJednostkaMiary.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSource, "JednostkaMiaryRef", true));
			this.comboBoxJednostkaMiary.DataSource = this.bindingSourceJednostkaMiary;
			this.comboBoxJednostkaMiary.DisplayMember = "Nazwa";
			this.comboBoxJednostkaMiary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxJednostkaMiary.FormattingEnabled = true;
			this.comboBoxJednostkaMiary.Location = new System.Drawing.Point(149, 180);
			this.comboBoxJednostkaMiary.Name = "comboBoxJednostkaMiary";
			this.comboBoxJednostkaMiary.Size = new System.Drawing.Size(116, 23);
			this.comboBoxJednostkaMiary.TabIndex = 8;
			this.comboBoxJednostkaMiary.ValueMember = "Ref";
			// 
			// bindingSourceJednostkaMiary
			// 
			this.bindingSourceJednostkaMiary.DataSource = typeof(ProFak.DB.JednostkaMiary);
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(101, 7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Nazwa";
			// 
			// comboBoxSposobLiczenia
			// 
			this.comboBoxSposobLiczenia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.SetColumnSpan(this.comboBoxSposobLiczenia, 2);
			this.comboBoxSposobLiczenia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSposobLiczenia.FormattingEnabled = true;
			this.comboBoxSposobLiczenia.Items.AddRange(new object[] {
            "według netto",
            "według brutto"});
			this.comboBoxSposobLiczenia.Location = new System.Drawing.Point(149, 61);
			this.comboBoxSposobLiczenia.Name = "comboBoxSposobLiczenia";
			this.comboBoxSposobLiczenia.Size = new System.Drawing.Size(148, 23);
			this.comboBoxSposobLiczenia.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(101, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 15);
			this.label3.TabIndex = 1;
			this.label3.Text = "Rodzaj";
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(27, 65);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(116, 15);
			this.label4.TabIndex = 1;
			this.label4.Text = "Sposób liczenia ceny";
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(76, 95);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(67, 15);
			this.label5.TabIndex = 1;
			this.label5.Text = "Stawka VAT";
			// 
			// label6
			// 
			this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 125);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(135, 15);
			this.label6.TabIndex = 1;
			this.label6.Text = "Cena jednostkowa netto";
			// 
			// label7
			// 
			this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(3, 154);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(140, 15);
			this.label7.TabIndex = 1;
			this.label7.Text = "Cena jednostkowa brutto";
			// 
			// comboBoxRodzaj
			// 
			this.comboBoxRodzaj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.SetColumnSpan(this.comboBoxRodzaj, 2);
			this.comboBoxRodzaj.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSource, "Rodzaj", true));
			this.comboBoxRodzaj.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxRodzaj.FormattingEnabled = true;
			this.comboBoxRodzaj.Location = new System.Drawing.Point(149, 32);
			this.comboBoxRodzaj.Name = "comboBoxRodzaj";
			this.comboBoxRodzaj.Size = new System.Drawing.Size(148, 23);
			this.comboBoxRodzaj.TabIndex = 2;
			// 
			// label8
			// 
			this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(51, 184);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(92, 15);
			this.label8.TabIndex = 1;
			this.label8.Text = "Jednostka miary";
			// 
			// textBoxNazwa
			// 
			this.textBoxNazwa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.SetColumnSpan(this.textBoxNazwa, 2);
			this.textBoxNazwa.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Nazwa", true));
			this.textBoxNazwa.Location = new System.Drawing.Point(149, 3);
			this.textBoxNazwa.Name = "textBoxNazwa";
			this.textBoxNazwa.Size = new System.Drawing.Size(148, 23);
			this.textBoxNazwa.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(72, 214);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "Widoczność";
			// 
			// numericUpDownCenaNetto
			// 
			this.numericUpDownCenaNetto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.SetColumnSpan(this.numericUpDownCenaNetto, 2);
			this.numericUpDownCenaNetto.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource, "CenaNetto", true));
			this.numericUpDownCenaNetto.DecimalPlaces = 2;
			this.numericUpDownCenaNetto.Location = new System.Drawing.Point(149, 121);
			this.numericUpDownCenaNetto.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.numericUpDownCenaNetto.Name = "numericUpDownCenaNetto";
			this.numericUpDownCenaNetto.Size = new System.Drawing.Size(148, 23);
			this.numericUpDownCenaNetto.TabIndex = 6;
			this.numericUpDownCenaNetto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// buttonJednostkaMiary
			// 
			this.buttonJednostkaMiary.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.buttonJednostkaMiary.AutoSize = true;
			this.buttonJednostkaMiary.Location = new System.Drawing.Point(271, 179);
			this.buttonJednostkaMiary.Name = "buttonJednostkaMiary";
			this.buttonJednostkaMiary.Size = new System.Drawing.Size(26, 25);
			this.buttonJednostkaMiary.TabIndex = 9;
			this.buttonJednostkaMiary.Text = "...";
			this.buttonJednostkaMiary.UseVisualStyleBackColor = true;
			this.buttonJednostkaMiary.Click += new System.EventHandler(this.buttonJednostkaMiary_Click);
			// 
			// TowarEdytor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel);
			this.MinimumSize = new System.Drawing.Size(300, 240);
			this.Name = "TowarEdytor";
			this.Size = new System.Drawing.Size(300, 243);
			((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSourceStawkaVat)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownCenaBrutto)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSourceJednostkaMiary)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownCenaNetto)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.BindingSource bindingSource;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxNazwa;
		private System.Windows.Forms.ComboBox comboBoxRodzaj;
		private System.Windows.Forms.NumericUpDown numericUpDownCenaNetto;
		private System.Windows.Forms.NumericUpDown numericUpDownCenaBrutto;
		private System.Windows.Forms.ComboBox comboBoxWidocznosc;
		private System.Windows.Forms.ComboBox comboBoxJednostkaMiary;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxSposobLiczenia;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox comboBoxStawkaVat;
		private System.Windows.Forms.Button buttonStawkaVat;
		private System.Windows.Forms.Button buttonJednostkaMiary;
		private System.Windows.Forms.BindingSource bindingSourceStawkaVat;
		private System.Windows.Forms.BindingSource bindingSourceJednostkaMiary;
	}
}
