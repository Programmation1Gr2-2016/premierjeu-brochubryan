using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace exercise01
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        GameObject heros;
        GameObject enemi;
        GameObject projectile;
        bool enemidirection = true;
        // si le projectile est en vie ou non
        bool projVie = true;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();


            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;


            
            this.graphics.ApplyChanges();
            // ToggleFullScreen = mode plein écran sans bordure
            //ApplyChanges = Mode plein écran avec la barre de titres Windows 
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            fenetre = graphics.GraphicsDevice.Viewport.Bounds;
            fenetre.Width = graphics.GraphicsDevice.DisplayMode.Width;
            fenetre.Height = graphics.GraphicsDevice.DisplayMode.Height;


            heros = new GameObject();
            heros.estVivant = true;
            heros.vitesse = 10;
            heros.sprite = Content.Load<Texture2D>("Mario (1).png");
            heros.position = heros.sprite.Bounds;
            //enemi
            enemi = new GameObject();
            enemi.estVivant = true;
            enemi.vitesse = 11;
            enemi.sprite = Content.Load<Texture2D>("enemie.png");
            enemi.position = enemi.sprite.Bounds;
            //projectile
            projectile = new GameObject();
            projectile.estVivant = true;
            projectile.vitesse =14;
            projectile.sprite = Content.Load<Texture2D>("enemi.png");
            projectile.position = enemi.sprite.Bounds;


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                heros.position.X += heros.vitesse;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                heros.position.X -= heros.vitesse;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                heros.position.Y -= heros.vitesse;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                heros.position.Y += heros.vitesse;
            }

            if (enemidirection == true)
            {
                enemi.position.X += enemi.vitesse;
            }
            else if (enemidirection == false)
            {
                enemi.position.X -= enemi.vitesse;
            }
            if (projVie == true)
            {
                projectile.position.Y += projectile.vitesse;
            }

           







            // TODO: Add your update logic here
            UpdateHeros();
            UpdateEnemi();
            UpdateProjectile();
           
            
            base.Update(gameTime);
        }
        protected void UpdateHeros()
        {
            //hero détection des bords
            if (heros.position.X < fenetre.Left)
            {
                heros.position.X = fenetre.Left;
            }
            if (heros.position.X + heros.sprite.Width > fenetre.Right)
            {
                heros.position.X = fenetre.Right - heros.sprite.Width;
            }
            if (heros.position.Y < fenetre.Top)
            {
                heros.position.Y = fenetre.Top;
            }
            if (heros.position.Y + heros.sprite.Height > fenetre.Bottom)
            {
                heros.position.Y = fenetre.Bottom - heros.sprite.Height;
            }
            
        }
        protected void UpdateEnemi()
        {
            //enemi détection de bord
            if (enemi.position.X < fenetre.Left)
            {
                enemidirection = true;
            }
            if (enemi.position.X + enemi.sprite.Bounds.Width > fenetre.Right)
            {
                enemidirection = false;
            }
        }
        protected void UpdateProjectile()
        {
            if (projectile.position.Y > fenetre.Bottom)
            {
                projVie = false;
            }

        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(heros.sprite, heros.position, Color.White);
            spriteBatch.Draw(enemi.sprite, enemi.position, Color.White);
            spriteBatch.Draw(projectile.sprite, projectile.position, Color.White);
            spriteBatch.End();




            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
