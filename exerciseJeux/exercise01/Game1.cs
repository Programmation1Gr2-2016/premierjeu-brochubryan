using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;

namespace exercise01
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        const int NBENEMI = 3;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        GameObject heros;
        GameObject[] enemi;
        GameObject projectile;
        bool enemidirection = true;
        Texture2D defaite;
        Texture2D victoire;
        bool projVie = true;
        private object thread;
        Random r = new Random();
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

            this.Window.Position = new Point(0, 0);

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


            defaite = Content.Load<Texture2D>("defaite.png");
            victoire = Content.Load<Texture2D>("victory.png");
            heros = new GameObject();
            heros.estVivant = true;
            heros.vitesse = 10;
            heros.sprite = Content.Load<Texture2D>("Mario (1).png");
            heros.position = heros.sprite.Bounds;
            heros.position.X = 0;
            heros.position.Y = 700;
            //enemi
            enemi = new GameObject[NBENEMI];
            for (int i = 0; i < enemi.Length; i++)
            {


                enemi[i] = new GameObject();
                enemi[i].estVivant = true;
                enemi[i].vitesse = 11;
                enemi[i].sprite = Content.Load<Texture2D>("enemie.png");
                enemi[i].position = enemi[i].sprite.Bounds;
                enemi[i].position.X = i * 300;
                enemi[i].position.Y = 0;
                //enemi[i].direction.X = r.Next(-4, 5);
               // enemi[i].direction.Y = r.Next(-4, 5);
            }

            //projectile
            projectile = new GameObject();
            projectile.estVivant = true;
            projectile.vitesse = 14;
            projectile.sprite = Content.Load<Texture2D>("enemi.png");
            for (int i = 0; i < enemi.Length; i++)
            {
                
                projectile.position = enemi[i].sprite.Bounds;

            }



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
            for (int i = 0; i < enemi.Length; i++)
            {
                if (enemidirection == true)
                {
                    enemi[i].position.X += enemi[i].vitesse;
                }
                else if (enemidirection == false)
                {
                    enemi[i].position.X -= enemi[i].vitesse;
                }
            }

            if (projVie == true)
            {
                projectile.position.Y += projectile.vitesse;
            }

            //collision


            for (int i = 0; i < enemi.Length; i++)
            {
                if (heros.position.Intersects(enemi[i].position))
                {
                    enemi[i].estVivant = false;
                    projectile.estVivant = false;
                }


            }
            if (projectile.position.Intersects(heros.position))
            {
                heros.estVivant = false;

            }
          
            
            if (projectile.position.Y == fenetre.Bottom)
            {
                projectile.estVivant = false;
            }
            if (projectile.estVivant == false)
            {
                for (int i = 0; i < enemi.Length; i++)
                {
                    projectile.position = enemi[i].position;
                    projectile.estVivant = true;
                    projectile.position.Y += projectile.vitesse;
                }

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
            for (int i = 0; i < enemi.Length; i++)
            {
                if (enemi[i].position.X < fenetre.Left)
                {
                    enemidirection = true;
                }
                if (enemi[i].position.X + enemi[i].sprite.Bounds.Width > fenetre.Right)
                {
                    enemidirection = false;
                }
            }

        }
       
        protected void UpdateProjectile()
        {
            for (int i = 0; i < enemi.Length; i++)
            {
                if (projectile.estVivant)
                {
                    projectile.position.X = enemi[i].position.X;
                }
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
            if (heros.estVivant)
            {
                spriteBatch.Draw(heros.sprite, heros.position, Color.White);

            }
            else if (heros.estVivant == false)
            {
                GraphicsDevice.Clear(Color.Red);
                spriteBatch.Draw(defaite, new Rectangle(0, 0, defaite.Width, defaite.Height), Color.White);
                projectile.estVivant = false;


            }

            for (int i = 0; i < enemi.Length; i++)
            {
                if (enemi[i].estVivant)
                {
                    spriteBatch.Draw(enemi[i].sprite, enemi[i].position, Color.White);
                }
                else
                {
                    spriteBatch.Draw(victoire, new Rectangle(0, 0, victoire.Width, victoire.Height), Color.White);
                    projectile.estVivant = false;
                }
            }




            if (projectile.estVivant == true)
            {

                spriteBatch.Draw(projectile.sprite, projectile.position, Color.White);
            }
            if (projVie == true)
            {

                spriteBatch.Draw(projectile.sprite, projectile.position, Color.White);
            }
            spriteBatch.End();




            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
