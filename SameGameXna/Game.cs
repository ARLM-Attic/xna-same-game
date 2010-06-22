using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SameGameXna.Forms;
using SameGameXna.Xna;
using SameGameXna.Win32;

namespace SameGameXna
{
	public class Game : IDisposable
	{
		GameClock gameClock;

		SpriteBatch spriteBatch;
				
		public GameServiceContainer Services
		{
			get;
			private set;
		}

		public GameSettings Settings
		{
			get;
			private set;
		}

		public HighScores HighScores
		{
			get;
			private set;
		}

		public IGameGraphicsDeviceService GraphicsDeviceService
		{
			get;
			private set;
		}
		
		public GraphicsDevice GraphicsDevice
		{
			get
			{
				if(this.GraphicsDeviceService == null)
					return null;

				return this.GraphicsDeviceService.GraphicsDevice;
			}
		}

		public IGameWindow Window
		{
			get;
			private set;
		}

		public Random Random
		{
			get;
			private set;
		}

		public Board Board
		{
			get;
			private set;
		}

		public Game()
		{
			this.gameClock = new GameClock();

			this.Services = new GameServiceContainer();

			this.Settings = GameSettings.LoadOrCreate();

			this.HighScores = HighScores.LoadOrCreate();

			this.Random = new Random();

			this.Board = new Board(this);
			this.Board.GameOver += GameOver;

			this.Window = new GameForm(this);
			this.Window.Idle += GameWindow_Idle;
			
			this.GraphicsDeviceService = new GameGraphicsDeviceService(this);
		}

		public void Dispose()
		{
		}

		public void Run()
		{
			this.Window.Run();
			this.Settings.Save();
			this.HighScores.Save();
		}

		public static void Main()
		{
			using(Game game = new Game())
				game.Run();
		}

		public void Initialize()
		{
			this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
			this.Board.Initialize();
		}

		private void GameWindow_Idle(object sender, EventArgs e)
		{
			Win32Message msg;
			while(!Win32Methods.PeekMessage(out msg, IntPtr.Zero, 0, 0, 0))
				Idle();
		}

		public void Idle()
		{
			this.gameClock.Tick();

			if(this.gameClock.ShouldUpdate)
			{
				Update(this.gameClock.ElapsedSinceUpdate);
				this.gameClock.ResetShouldUpdate();
			}
								
			if(this.gameClock.ShouldDraw)
			{				
				Draw();
				this.gameClock.ResetShouldDraw();
			}
		}

		public void Update(TimeSpan elapsed)
		{
			this.Board.Update(elapsed);
		}

		public void Draw()
		{
			this.GraphicsDevice.Clear(Color.CornflowerBlue);

			this.spriteBatch.Begin();
			this.Board.Draw(this.spriteBatch);
			this.spriteBatch.End();
			
			this.GraphicsDeviceService.Present();
		}

		private void GameOver(object sender, EventArgs e)
		{
			var score = this.Board.Score;

			if(this.HighScores.IsHighScore(score))
			{
				string name = this.Window.CollectHighScoreName(this.HighScores.GetRank(score));
				this.HighScores.AddHighScore(name, score);
			}

			this.Window.ShowMessage(GameMessages.GameOver, score);
		}

		public void NewGame()
		{
			this.Board.Initialize();
		}

		public void Exit()
		{
			Application.Exit();
		}
	}
}
