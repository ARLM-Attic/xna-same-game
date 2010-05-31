using System;
using System.Windows.Forms;
using SameGameXna.Forms;

namespace SameGameXna
{
	public class Game : IDisposable
	{
		public ServiceContainer Services
		{
			get;
			private set;
		}

		public GameForm GameForm
		{
			get;
			private set;
		}

		public Game()
		{
			this.Services = new ServiceContainer();

			this.GameForm = new GameForm(this);
		}

		public void Run()
		{
			Application.Run(this.GameForm);
		}

		public void Dispose()
		{
		}

		public static void Main()
		{
			using(Game game = new Game())
				game.Run();
		}
	}
}
