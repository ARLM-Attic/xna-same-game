using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SameGameXna
{
	public class Block
	{
		public const int Width = 32;
		public const int Height = 32;
		public const int Speed = 800;

		Game game;
		Vector2 position;

		public BlockColor Color
		{
			get;
			private set;
		}

		public BlockMultiplier Multiplier
		{
			get;
			private set;
		}

		public Point BoardPosition
		{
			get;
			set;
		}

		public bool Selected
		{
			get;
			set;
		}

		public bool Visible
		{
			get;
			set;
		}

		public Block(Game game)
		{
			this.game = game;
		}

		public void Initialize(Point boardPosition)
		{
			this.BoardPosition = boardPosition;

			this.Color = this.game.Random.NextBlockColor();
			this.Multiplier = this.game.Random.NextBlockMultipler();

			this.position = new Vector2(this.BoardPosition.X * Width, this.BoardPosition.Y * Height - this.game.Board.Height);
			this.Visible = true;
		}

		public void Update(TimeSpan elapsed)
		{
			Vector2 destination = new Vector2(this.BoardPosition.X * Width, this.BoardPosition.Y * Height);

			this.position.X = destination.X;

			if(this.position.Y < destination.Y)
				this.position.Y += Speed * (float)elapsed.TotalSeconds;

			if(this.position.Y > destination.Y)
				this.position.Y = destination.Y;
		}
				
		public void Draw(SpriteBatch spriteBatch, Texture2D blockTexture)
		{
			var position = new Vector2((int)this.position.X, (int)this.position.Y);
			
			var source = new Rectangle(0, 0, 32, 32);

			if(this.Multiplier == BlockMultiplier.x2)
				source.X = 32;
			else if(this.Multiplier == BlockMultiplier.x3)
				source.X = 64;
			else if(this.Multiplier == BlockMultiplier.x5)
				source.X = 96;

			spriteBatch.Draw(blockTexture, position, source, this.Color.ToXnaColor());

			if(this.Selected)
				spriteBatch.Draw(blockTexture, position, new Rectangle(128, 0, 32, 32), Microsoft.Xna.Framework.Graphics.Color.Gray); 
		}

		public void Swap(Block other)
		{

		}
	}
}
