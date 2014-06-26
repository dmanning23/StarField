using GameTimer;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace StarField
{
	/// <summary>
	/// One layer in the star field
	/// </summary>
	public class CollectionLayer
	{
		#region Fields

		/// <summary>
		/// The color to draw all the starts in this layer
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// How much to scale the offset points and movement in this layer
		/// </summary>
		public float Scale { get; set; }

		/// <summary>
		/// The points for the lines to draw from each star
		/// </summary>
		Queue<LinePoint> Points { get; set; }

		List<Star> Stars { get; set; }

		#endregion //Fields

		#region Methods

		/// <summary>
		/// Cosntructor
		/// </summary>
		/// <param name="color"></param>
		/// <param name="scale"></param>
		public CollectionLayer(Color color, float scale)
		{
			Color = color;
			Scale = scale;
		}

		/// <summary>
		/// Add a star to this layer
		/// </summary>
		/// <param name="world"></param>
		public void RandomStarLocation(Star star, Rectangle world)
		{
			//TODO: create a random position somewhere inside the world
		}

		/// <summary>
		/// Add a point to the lines that are drawn from each star
		/// </summary>
		/// <param name="point"></param>
		/// <param name="angle"></param>
		/// <param name="time"></param>
		public void AddPoint(Vector2 point, float angle, GameClock time)
		{
			//TODO: create a point

			//TODO: scale the offset of the point

			//TODO: store the point
		}

		/// <summary>
		/// Update the stars and removed any expired objects
		/// </summary>
		/// <param name="time"></param>
		/// <param name="velocity"></param>
		/// <param name="world"></param>
		public void Update(GameClock time, Vector2 velocity, Rectangle world)
		{
			//TODO: remove expired points

			//TODO: update all star positions

			//TODO: remove any stars that have gone off the map

				//TODO: if a star goes off the map, move it to a random position
		}

		#endregion //Methods
	}
}
