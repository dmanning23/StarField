using GameTimer;
using Microsoft.Xna.Framework.Graphics;
using RectangleFLib;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vector2Extensions;

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
		List<LinePoint> Points { get; set; }

		/// <summary>
		/// list of all the stars in this layer
		/// </summary>
		List<Star> Stars { get; set; }

		static Random _random = new Random();

		/// <summary>
		/// How long we hold on to points
		/// </summary>
		private const float PointLife = 1.0f;

		/// <summary>
		/// Texture we are gonna use to render the points
		/// </summary>
		private Texture2D Tex;

		/// <summary>
		/// how big to draw the stars
		/// </summary>
		private float StarSize { get; set; }

		#endregion //Fields

		#region Methods

		/// <summary>
		/// Cosntructor
		/// </summary>
		/// <param name="color"></param>
		/// <param name="scale"></param>
		public CollectionLayer(Texture2D tex, Color color, float scale, float starSize, int numStars, RectangleF world)
		{
			Color = color;
			Scale = scale;
			Tex = tex;
			StarSize = starSize;

			Points = new List<LinePoint>();
			Stars = new List<Star>();

			//add all the stars
			for (int i = 0; i < numStars; i++)
			{
				var star = new Star();
				RandomStarLocation(star, world);
			}
		}

		/// <summary>
		/// Add a star to this layer
		/// </summary>
		/// <param name="world"></param>
		public static void RandomStarLocation(Star star, RectangleF world)
		{
			//create a random position somewhere inside the world
			star.Position = _random.NextVector2(world.Left, world.Right, world.Top, world.Bottom);
		}

		/// <summary>
		/// Add a point to the lines that are drawn from each star
		/// </summary>
		/// <param name="point"></param>
		/// <param name="angle"></param>
		/// <param name="time"></param>
		public void AddPoint(Vector2 point, float angle, GameClock time)
		{
			//create a point
			LinePoint pt = new LinePoint();

			//scale the offset of the point
			pt.Position = point * Scale;
			pt.Time = time.GetCurrentTime() + PointLife;

			//get the angle from the previous point
			Vector2 prev = Vector2.Zero;
			if (Points.Count > 0)
			{
				prev = Points[Points.Count - 1].Position;
			}
			pt.Angle = (point - prev).Angle();

			//store the point
			Points.Add(pt);
		}

		/// <summary>
		/// Update the stars and removed any expired objects
		/// </summary>
		/// <param name="time"></param>
		/// <param name="velocity"></param>
		/// <param name="world"></param>
		public void Update(GameClock time, Vector2 velocity, RectangleF world)
		{
			//remove expired points
			while ((0 < Points.Count) && (Points[0].Time >= time.GetCurrentTime()))
			{
				//throw out that old point
				Points.RemoveAt(0);
			}

			//update all star positions
			for (int i = 0; i < Stars.Count; i++)
			{
				Stars[i].Update(time, velocity);

				//if a star goes off the map, move it to a random position
				if (!world.Contains(Stars[i].Position))
				{
					RandomStarLocation(Stars[i], world);
				}
			}
		}

		public void Render(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < Stars.Count; i++)
			{
				//if there arent any points, just draw the thing
				if (Points.Count == 0)
				{
					spriteBatch.Draw(Tex,
								Stars[i].Position,
								null,
								Color,
								0.0f,
								Vector2.Zero,
								StarSize,
								SpriteEffects.None,
								0);
					return;
				}

				//the point to start drawing from 
				Vector2 start = Stars[i].Position;
				foreach (var point in Points)
				{
					//get the point to draw
					Vector2 end = start + point.Position;

					//draw the thing
					spriteBatch.Draw(Tex,
								  end,
								  null,
								  Color,
								  point.Angle,
								  new Vector2(0, 0.5f),
								  new Vector2(point.Length, StarSize),
								  SpriteEffects.None,
								  0);

					//update the start point
					start = end;
				}
			}
		}

		#endregion //Methods
	}
}
