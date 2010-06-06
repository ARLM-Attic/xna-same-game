using System;
using System.Diagnostics;

namespace SameGameXna
{
	public class GameClock
	{
		TimeSpan lastElapsedSinceStart;
		
		
		Stopwatch stopwatch;

		int pause;

		public int DesiredUpdateInterval
		{
			get;
			set;
		}

		public TimeSpan ElapsedSinceUpdate
		{
			get;
			private set;
		}

		public bool ShouldUpdate
		{
			get;
			private set;
		}

		public int DesiredDrawInterval
		{
			get;
			set;
		}

		public TimeSpan ElapsedSinceDraw
		{
			get;
			private set;
		}
				
		public bool ShouldDraw
		{
			get;
			private set;
		}

		public GameClock()
		{
			this.lastElapsedSinceStart = TimeSpan.Zero;

			this.DesiredDrawInterval = 60;
			this.DesiredUpdateInterval = 60;

			this.stopwatch = new Stopwatch();
			this.stopwatch.Start();			
		}

		public void Tick()
		{
			TimeSpan elapsedSinceStart = this.stopwatch.Elapsed;

			if(this.pause <= 0)
			{
				var elapsedTime = elapsedSinceStart - this.lastElapsedSinceStart;

				this.ElapsedSinceUpdate += elapsedTime;
				if(this.ElapsedSinceUpdate.TotalMilliseconds > (1000.0f / (float)this.DesiredUpdateInterval))
					this.ShouldUpdate = true;

				this.ElapsedSinceDraw += elapsedTime;
				if(this.ElapsedSinceDraw.TotalMilliseconds > (1000.0f / (float)this.DesiredDrawInterval))
					this.ShouldDraw = true;
			}

			this.lastElapsedSinceStart = elapsedSinceStart;
		}

		public void Pause()
		{
			this.pause++;
		}

		public void Resume()
		{
			this.pause--;

			if(this.pause < 0)
				this.pause = 0;
		}

		public void ResetShouldUpdate()
		{
			this.ShouldUpdate = false;
			this.ElapsedSinceUpdate = TimeSpan.Zero;
		}

		public void ResetShouldDraw()
		{
			this.ShouldDraw = false;
			this.ElapsedSinceDraw = TimeSpan.Zero;
		}
	}
}
