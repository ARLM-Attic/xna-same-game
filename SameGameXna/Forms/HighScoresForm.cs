using System;
using System.Drawing;
using System.Windows.Forms;

namespace SameGameXna.Forms
{
	/// <summary>
	/// Form for displaying high scores.
	/// </summary>
	public class HighScoresForm : Form
	{
		Game game;

		public HighScoresForm(Game game)
			: base()
		{
			this.game = game;

			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.Height = 315;
			this.MinimizeBox = false;
			this.MaximizeBox = false;
			this.Padding = new Padding(5);
			this.Text = "High Scores";

			var panel = new TableLayoutPanel()
			{
				CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset,
				ColumnCount = 2,
				Dock = DockStyle.Fill,
				Margin = new Padding(0, 0, 0, 5),
				RowCount = this.game.HighScores.Count
			};
			this.Controls.Add(panel);

			var ok = new Button()
			{
				Dock = DockStyle.Bottom,
				Text = "OK"
			};
			ok.Click += (s, e) => { this.Close(); };
			this.Controls.Add(ok);

			for(int i = 0; i < this.game.HighScores.Count; i++)
			{
				var highScore = this.game.HighScores[i];
				panel.Controls.Add(new Label() { Text = highScore.name, TextAlign = ContentAlignment.MiddleLeft }, 0, i);
				panel.Controls.Add(new Label() { Text = highScore.score.ToString("#,0"), TextAlign = ContentAlignment.MiddleLeft }, 1, i);
			}
		}
	}
}
