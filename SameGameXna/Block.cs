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
		Board board;
		Vector2 position;

		public BlockColor Color
		{
			get;
			set;
		}

		public Point BoardPosition
		{
			get;
			set;
		}

		public Block(Game game, Board board)
		{
			this.game = game;
			this.board = board;
		}

		public void Initialize(Point boardPosition)
		{
			this.BoardPosition = boardPosition;

			this.Color = this.game.Random.NextBlockColor();

			this.position = new Vector2(this.BoardPosition.X * Width, this.BoardPosition.Y * Height - this.board.Height);
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
			spriteBatch.Draw(blockTexture, position, new Rectangle(0, 0, 32, 32), this.Color.ToXnaColor());
		}
	}
}
