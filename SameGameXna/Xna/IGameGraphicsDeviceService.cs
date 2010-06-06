using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;

namespace SameGameXna.Xna
{
	public interface IGameGraphicsDeviceService : IGraphicsDeviceService
	{
		/// <summary>
		/// Creates the <see cref="Microsoft.Xna.Framework.Graphics.GraphicsDevice"/>.
		/// </summary>
		/// <param name="control">The host control.</param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		void CreateDevice(Control control, int width, int height);

		/// <summary>
		/// Checks to see if the <see cref="Microsoft.Xna.Framework.Graphics.GraphicsDevice"/> is still valid or needs a reset.
		/// </summary>
		/// <returns></returns>
		bool IsDeviceValid();

		/// <summary>
		/// Presents the back buffer onto the host control.
		/// </summary>
		void Present();
	}
}
