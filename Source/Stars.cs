using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace StarField
{
	public class Stars
	{
		#region Fields

		/// <summary>
		/// The layers of this star field
		/// </summary>
		private List<StarLayer> Layers { get; set; }

		private Color StartColor = new Color(1.0f, 1.0f, 1.0f, 0.6f);
		private const byte ColorDelta = 30;
		private const float StartScale = 1.0f;
		private const float ScaleDelta = -0.2f;
		private const float StartStarSize = 18.0f;
		private const float StarSizeDelta = -5.0f;
		private const int NumStartStars = 50;
		private const int StartStarsDelta = 40;

		#endregion //Fields

		#region Methods

		public Stars(GraphicsDevice graphicsDevice, Rectangle world, float starSizeScale = 1.0f)
		{
			//create the texture we need
			var tex = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
			tex.SetData<Color>(new Color[] { Color.White });
			
			Layers = new List<StarLayer>();

			//start params for layers
			Color color = StartColor;
			float scale = StartScale;
			float starSize = StartStarSize * starSizeScale;
			int startStars = NumStartStars;

			for (int i = 0; i < 4; i++)
			{
				Layers.Add(new StarLayer(tex, color, scale, starSize, startStars, world));
				color.A -= ColorDelta;
				scale += ScaleDelta;
				starSize += StarSizeDelta * starSizeScale;
				startStars += StartStarsDelta;
			}
		}

		public void Update(Vector2 velocity, Rectangle world)
		{
			for (int i = 0; i < Layers.Count; i++)
			{
				Layers[i].Update(velocity, world);
			}
		}

		public void Render(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < Layers.Count; i++)
			{
				Layers[i].Render(spriteBatch);
			}
		}

		#endregion //Methods
	}
}
