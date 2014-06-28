using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Vector2Extensions;

namespace StarField
{
	/// <summary>
	/// One layer in the star field
	/// </summary>
	internal class StarLayer
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
		/// list of all the stars in this layer
		/// </summary>
		List<Star> Stars { get; set; }

		static Random _random = new Random();

		/// <summary>
		/// Texture we are gonna use to render the points
		/// </summary>
		private Texture2D Tex;

		/// <summary>
		/// how big to draw the stars
		/// </summary>
		private float StarSize { get; set; }

		public Vector2 Velocity 
		{
			get
			{
				return _velocity;
			}
			set
			{
				_velocity = value;
				_velocityLength = _velocity.Length() *(15.0f * Scale);
				_velocityAngle = ((_velocityLength > 0.0f) ? _velocity.Angle() : 0.0f);
			}
		}
		
		private Vector2 _velocity;
		private float _velocityLength;
		private float _velocityAngle;

		#endregion //Fields

		#region Methods

		/// <summary>
		/// Cosntructor
		/// </summary>
		/// <param name="color"></param>
		/// <param name="scale"></param>
		public StarLayer(Texture2D tex, Color color, float scale, float starSize, int numStars, Rectangle world)
		{
			Color = color;
			Scale = scale;
			Tex = tex;
			StarSize = starSize;
			Velocity = Vector2.Zero;

			Stars = new List<Star>();

			//add all the stars
			for (int i = 0; i < numStars; i++)
			{
				var star = new Star();
				RandomStarLocation(star, world);
				Stars.Add(star);
			}
		}

		/// <summary>
		/// Add a star to this layer
		/// </summary>
		/// <param name="world"></param>
		public static void RandomStarLocation(Star star, Rectangle world)
		{
			//create a random position somewhere inside the world
			star.Position = _random.NextVector2(world.Left, world.Right, world.Top, world.Bottom);
		}

		/// <summary>
		/// Update the stars and removed any expired objects
		/// </summary>
		/// <param name="time"></param>
		/// <param name="velocity"></param>
		/// <param name="world"></param>
		public void Update(Vector2 velocity, Rectangle world)
		{
			Velocity = velocity * Scale;

			//update all star positions
			for (int i = 0; i < Stars.Count; i++)
			{
				Stars[i].Update(Velocity);

				//if a star goes off the map, move it to a random position
				if (!world.Contains(Stars[i].Position))
				{
					RandomStarLocation(Stars[i], world);
				}
			}
		}

		public void Render(SpriteBatch spriteBatch)
		{
			float length = ((_velocityLength > StarSize) ? _velocityLength : StarSize);

			for (int i = 0; i < Stars.Count; i++)
			{
				//the point to start drawing from 
				Vector2 start = Stars[i].Position;

				//get the point to draw
				Vector2 end = start + Velocity;

				//draw the thing
				spriteBatch.Draw(Tex,
								end,
								null,
								Color,
								_velocityAngle,
								new Vector2(0, 0.5f),
								new Vector2(length, StarSize),
								SpriteEffects.None,
								0);
			}
		}

		#endregion //Methods
	}
}
