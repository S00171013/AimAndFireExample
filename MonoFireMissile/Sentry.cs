using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimatedSprite 
{
    class Sentry : RotatingSprite
    {
        protected Game myGame;
        private Projectile enemyProjectile;

        float chaseRadius = 200;

        public Sentry(Game g, Texture2D texture, Vector2 userPosition, int framecount) : base(g, texture, userPosition, framecount)
        {
            myGame = g;
        }

        public Projectile EnemyProjectile
        {
            get
            {
                return enemyProjectile;
            }

            set
            {
                enemyProjectile = value;
            }
        }

        public void loadProjectile(Projectile r)
        {
            EnemyProjectile = r;
        }


        public override void Update(GameTime gametime)
        {
            if(enemyProjectile!= null)
                enemyProjectile.Update(gametime);
            
            base.Update(gametime);
        }

        public void Follow(PlayerWithWeapon p)
        {
            if(inChaseZone(p) == true)
            {
                this.angleOfRotation = TurnToFace(position,
                                                p.position, angleOfRotation, 1f);
                //EnemyProjectile.fire(p.position);

            }
        }


        public bool inChaseZone(PlayerWithWeapon p)
        {
            float distance = Math.Abs(Vector2.Distance(this.WorldOrigin, p.CentrePos));
            if (distance <= chaseRadius)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (EnemyProjectile != null && EnemyProjectile.ProjectileState != Projectile.PROJECTILE_STATE.STILL)
            {
                EnemyProjectile.Draw(spriteBatch);
            }    
        }

    }
}
