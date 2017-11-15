using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using AnimatedSprite;
using Utilities;

namespace Engines
{
    class ChaseAndFireEngine
    {
        PlayerWithWeapon p;
        SpriteBatch spriteBatch;
        private CircularChasingEnemy[] chasers;
        private Game _gameOwnedBy;

        Sentry sentry1;

        public ChaseAndFireEngine(Game game)
            {
                // Chase engine remembers reference to the game
                _gameOwnedBy = game;
                game.IsMouseVisible = true;
                SoundEffect[] _PlayerSounds = new SoundEffect[5];
                spriteBatch = new SpriteBatch(game.GraphicsDevice);

            
            p = new PlayerWithWeapon(game, game.Content.Load<Texture2D>(@"Textures/wizard_strip3"), new Vector2(400, 400), 3);
            //fireball = new Projectile(game, game.Content.Load<Texture2D>(@"Textures/fireball_strip4"),
            //                            new Sprite(game, game.Content.Load<Texture2D>(@"Textures/explosion_strip8"),p.position,8)
            //                            ,p.position, 4);

            p.loadProjectile(new Projectile(game, game.Content.Load<Texture2D>(@"Textures/fireball_strip4"),
                                        new Sprite(game, game.Content.Load<Texture2D>(@"Textures/explosion_strip8"), p.position, 8)
                                        , p.position, 4));

            chasers = new CircularChasingEnemy[Utility.NextRandom(2,5)];

            // Lab 8 Question: Add Sentry that fires projectiles at the player.
            sentry1 = new Sentry(game, game.Content.Load<Texture2D>(@"Textures/CrossBow"), new Vector2(800, 800), 1);

            // Load Projectile Image.
            sentry1.loadProjectile(new Projectile(game, game.Content.Load<Texture2D>(@"Textures/Arrow"),
                                        new Sprite(game, game.Content.Load<Texture2D>(@"Textures/explosion_strip8"), sentry1.position, 8),
                                        sentry1.position, 1));



            for (int i = 0; i < chasers.Count(); i++)
                {
                    chasers[i] = new CircularChasingEnemy(game,
                            game.Content.Load<Texture2D>(@"Textures/Dragon_strip3"), 
                                Vector2.Zero,
                             3);
                    chasers[i].myVelocity = (float)Utility.NextRandom(2, 5);
                    chasers[i].position = new Vector2(Utility.NextRandom(game.GraphicsDevice.Viewport.Width - chasers[i].spriteWidth),
                            Utility.NextRandom(game.GraphicsDevice.Viewport.Height - chasers[i].spriteHeight));
                }
                
            }


        public void Update(GameTime gameTime)
        {
            
            p.Update(gameTime);
            foreach (CircularChasingEnemy chaser in chasers)
            {
                if (p.MyProjectile.ProjectileState == Projectile.PROJECTILE_STATE.EXPOLODING && p.MyProjectile.collisionDetect(chaser))
                    chaser.die();
                chaser.follow(p);
                chaser.Update(gameTime);
            }
            
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            p.Draw(spriteBatch);
            foreach (CircularChasingEnemy chaser in chasers)
                chaser.Draw(spriteBatch);
        }


        
    }
}
