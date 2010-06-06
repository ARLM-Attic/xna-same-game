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
				
		public ServiceContainer Services
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

		public GameForm GameForm
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

			this.Services = new ServiceContainer();

			this.GameForm = new GameForm(this);
			
			this.GraphicsDeviceService = new GameGraphicsDeviceService(this);

			this.Random = new Random();

			this.Board = new Board(this);
		}

		public void Run()
		{
			Application.EnableVisualStyles();
			Application.Idle += Application_Idle;
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

		public void Initialize()
		{
			this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
			this.Board.Initialize();
		}

		private void Application_Idle(object sender, EventArgs e)
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
