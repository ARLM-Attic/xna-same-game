using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SameGameXna.Forms
{
	public class NewHighScoreForm : Form
	{
		TextBox nameBox;

		public string EnteredName
		{
			get { return nameBox.Text; }
		}

		public NewHighScoreForm(int rank)
			: base()
		{
			
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.Height = 140;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Padding = new Padding(5);
			this.ShowInTaskbar = false;
			this.Text = "New High Score";
			this.Width = 240;

			this.nameBox = new TextBox()
			{
				Dock = DockStyle.Fill
			};
			this.Controls.Add(this.nameBox);

			var label = new Label()
			{
				Dock = DockStyle.Top,
				Height = 50,
				Text = "Congratulations!\nYou ranked " + GetRankString(rank) + ".\nPlease enter your name:"
			};
			this.Controls.Add(label);

			var ok = new Button()
			{
				Dock = DockStyle.Bottom,
				Text = "OK"
			};
			ok.Click += (s, e) => { this.Close(); };
			this.Controls.Add(ok);
		}

		/// <summary>
		/// Returns the rank string of a score. (1st, 2nd, 3rd...)
		/// </summary>
		/// <param name="score"></param>
		/// <returns></returns>
		private string GetRankString(int rank)
		{
			switch(rank)
			{
				case 1: return "1st";
				case 2: return "2nd";
				case 3: return "3rd";
				case 4: return "4th";
				case 5: return "5th";
				case 6: return "6th";
				case 7: return "7th";
				case 8: return "8th";
				case 9: return "9th";
				case 10: return "10th";
			}

			return "";
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if(this.nameBox.Text == "")
			{
				MessageBox.Show(this, "Please provide a name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				e.Cancel = true;
			}

			base.OnClosing(e);
		}
	}
}
