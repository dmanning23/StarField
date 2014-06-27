using Microsoft.Xna.Framework;

namespace StarField
{
	/// <summary>
	/// little class for managing a star and it's movement
	/// </summary>
	public class Star
	{
		#region Fields

		private Vector2 _pos;

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
			set
			{
				_pos = value;
			}
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="pos"></param>
		public Star()
		{
		}

		public void Update(Vector2 velocity)
		{
			//add the direction + velocty to the location
			_pos = _pos + velocity;
		}

		#endregion //Methods
	}
}
