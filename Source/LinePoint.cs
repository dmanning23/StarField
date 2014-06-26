using Microsoft.Xna.Framework;

namespace StarField
{
	/// <summary>
	/// class for managing all the points in the line that is drawn from each star
	/// </summary>
	public class LinePoint
	{
		#region Fields

		/// <summary>
		/// The offset position from the previous point
		/// </summary>
		private Vector2 _pos;

		/// <summary>
		/// The angle from the previous point
		/// </summary>
		public float Angle { get; set; }

		/// <summary>
		/// The time this point was added
		/// </summary>
		public float Time { get; set; }

		#endregion //Fields

		#region Properties

		/// <summary>
		/// The rectangle of this dude
		/// </summary>
		public Vector2 Position
		{
			get
			{
				return _pos;
			}
		}

		public int X
		{
			set
			{
				_pos.X = value;
			}
		}

		public int Y
		{
			set
			{
				_pos.Y = value;
			}
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="pos"></param>
		public LinePoint(Vector2 pos, float angle)
		{
			_pos = _pos;
			Angle = angle;
		}

		#endregion //Methods
	}
}
