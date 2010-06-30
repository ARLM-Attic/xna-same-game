using System;
using System.Drawing;
using System.Windows.Forms;

namespace SameGameXna.Forms
{
	public class AboutForm : Form
	{
		const string AboutText = "Programmed by Zachary Snow\n\n" +
								 "Contributions by:\n" +
								 "Nils Stenbock";

		public AboutForm()
			: base()
		{
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.Padding = new Padding(5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Text = "About";
			this.Width = 200;
			this.Height = 150;
			this.StartPosition = FormStartPosition.CenterParent;
			this.ShowInTaskbar = false;

			var label = new Label()
			{
				Dock = DockStyle.Fill,
				Text = AboutText
			};

			this.Controls.Add(label);

			var close = new Button()
			{
				Height = 25,
				Location = new Point(this.ClientSize.Width - 105, this.ClientSize.Height - 30),
				Text = "Close",
				Width = 100
			};
			close.Click += (s, e) => { this.Close(); };
			this.Controls.Add(close);
			close.BringToFront();
		}
	}
}
