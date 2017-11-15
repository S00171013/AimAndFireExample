using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimatedSprite 
{
    class Sentry : Sprite
    {
        protected Game myGame;
        private Projectile myProjectile;

        float chaseRadius = 200;



        public Sentry(Game g, Texture2D texture, Vector2 userPosition, int framecount) : base(g, texture, userPosition, framecount)
        {
            myGame = g;
        }

        public Projectile MyProjectile
        {
            get
            {
                return myProjectile;
            }

            set
            {
                myProjectile = value;
            }
        }

        public void loadProjectile(Projectile r)
        {
            MyProjectile = r;
        }

        public void Shoot(PlayerWithWeapon p)
        {
            if(inChaseZone(p) && MyProjectile.ProjectileState == Projectile.PROJECTILE_STATE.STILL)
            {
                MyProjectile.fire(p.position);
            }
        }


        public bool inChaseZone(PlayerWithWeapon p)
        {
            float distance = Math.Abs(Vector2.Distance(this.WorldOrigin, p.CentrePos));
            if (distance <= chaseRadius)
                return true;
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (MyProjectile != null && MyProjectile.ProjectileState != Projectile.PROJECTILE_STATE.STILL)
            {
                MyProjectile.Draw(spriteBatch);
            }            
        }

    }
}
