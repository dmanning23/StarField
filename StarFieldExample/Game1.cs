using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarField;

namespace StarFieldExample;

/// <summary>
/// This example project shows starfield similar to the title screen of Mega Man 8
/// </summary>
public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    /// <summary>
    /// The Stars object manages all the starfields and stars
    /// </summary>
    private Stars StarBackground { get; set; }

    /// <summary>
    /// The Stars object needs a rectangle to spawn and remove stars
    /// </summary>
    Rectangle _rect;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        //Initialize the rect used to manage stars as twice the screen size
        _rect = new Rectangle(0, 0, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height);
        _rect.Width *= 2;

        //Create the Stars object, which creates 4 layers of starfield

        //This creates a nice default star field
        StarBackground = new Stars(_graphics.GraphicsDevice, _rect, 0.25f);

        //Specify a texture to use for stars. This doesn't work
        //StarBackground = new Stars(Content.Load<Texture2D>("circle"), _rect, 0.25f);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        //Update the starfield and shoot all the stars to the left
        StarBackground.Update(new Vector2(-5.0f, 0.0f), _rect);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        //Draw the stars with NonPremultiplied so the alpha blending works correctly
        _spriteBatch.Begin(blendState: BlendState.NonPremultiplied);
        StarBackground.Render(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
