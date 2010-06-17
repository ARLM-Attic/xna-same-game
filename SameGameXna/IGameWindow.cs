using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SameGameXna
{
	/// <summary>
	/// Interface through which the Game class communicates with the UI.
	/// </summary>
	public interface IGameWindow
	{
		/// <summary>
		/// Triggered when the UI is idle.
		/// </summary>
		event EventHandler Idle;

		/// <summary>
		/// Starts the UI.
		/// </summary>
		void Run();

		void ShowMessage(GameMessages messages);

		void ShowGameOverMessage(UInt64 score, bool isHighScore, string rank);
	}
}
