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
			set;
		}

		public BlockMultiplier Multiplier
		{
			get;
			set;
		}

		public Vector2 Position
		{
			get { return this.position; }
			set { this.position = value; }
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

		public float Scale
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

			this.Selected = false;

			this.Scale = 1.0f;
			this.Visible = true;
		}

		public void Update(TimeSpan elapsed)
		{
			Vector2 destination = new Vector2(this.BoardPosition.X * Width, this.BoardPosition.Y * Height);

			if(this.position.X > destination.X)
				this.position.X -= Speed * (float)elapsed.TotalSeconds;

			if(this.position.X < destination.X)
				this.position.X = destination.X;

			if(this.position.Y < destination.Y)
				this.position.Y += Speed * (float)elapsed.TotalSeconds;

			if(this.position.Y > destination.Y)
				this.position.Y = destination.Y;
		}
				
		public void Draw(SpriteBatch spriteBatch, Texture2D blockTexture)
		{
			var position = new Vector2((int)this.position.X + (Width / 2), (int)this.position.Y + (Height / 2));
			
			var source = new Rectangle(0, 0, Width, Width);

			if(this.Multiplier == BlockMultiplier.x2)
				source.X = Width;
			else if(this.Multiplier == BlockMultiplier.x3)
				source.X = Width * 2;
			else if(this.Multiplier == BlockMultiplier.x5)
				source.X = Width * 3;

			spriteBatch.Draw(blockTexture, position, source, this.Color.ToXnaColor(),
						     0, new Vector2(16, 16), this.Scale, SpriteEffects.None, 0);

			if(this.Selected)
			{
				spriteBatch.Draw(blockTexture, position, new Rectangle(128, 0, Width, Height), new Microsoft.Xna.Framework.Graphics.Color(180, 180, 180, 180),
								 0, new Vector2(16, 16), this.Scale, SpriteEffects.None, 0);
			}
		}
	}
}
