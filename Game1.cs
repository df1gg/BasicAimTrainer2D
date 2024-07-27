using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D targetSprite;
    Texture2D crosshairSprite;
    Texture2D backgroundSprite;

    SpriteFont gameFont;

    MouseState mState;
    bool mRealese = true;

    Vector2 targetPosition = new Vector2(300, 300);
    const int targetRadius = 45;

    int score;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        targetSprite = Content.Load<Texture2D>("target");
        crosshairSprite = Content.Load<Texture2D>("crosshairs");
        backgroundSprite = Content.Load<Texture2D>("sky");

        gameFont = Content.Load<SpriteFont>("galleryFont");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        mState = Mouse.GetState();

        if (mState.LeftButton == ButtonState.Pressed && mRealese == true)
        {
          float mouseTargetDist = Vector2.Distance(targetPosition, mState.Position.ToVector2());

          if (mouseTargetDist < targetRadius)
          {
            score++;

            Random rand = new Random();
            targetPosition.X = rand.Next(45, _graphics.PreferredBackBufferWidth);
            targetPosition.Y = rand.Next(25, _graphics.PreferredBackBufferHeight);
          }

          mRealese = false;
        }

        if (mState.LeftButton == ButtonState.Released)
        {
          mRealese = true;
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);


        _spriteBatch.Begin();

        _spriteBatch.Draw(backgroundSprite, Vector2.Zero, Color.White);
        _spriteBatch.Draw(targetSprite, new Vector2(targetPosition.X - targetRadius, targetPosition.Y - targetRadius), Color.White);
        _spriteBatch.DrawString(gameFont, score.ToString(), new Vector2(20, 20), Color.White);
        _spriteBatch.Draw(crosshairSprite, new Vector2(mState.Position.ToVector2().X - 25, mState.Position.ToVector2().Y - 25), Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
