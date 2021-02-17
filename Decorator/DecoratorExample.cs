using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Example
{
    public class DecoratorExample : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            Shotgun shotgun = new Shotgun();

            Silencer silencedShotgun = new Silencer(shotgun);
            silencedShotgun.Shoot();

            Scope scopeGun = new Scope(silencedShotgun);

            Gun gun = new Silencer(new Scope(new Scope(new Silencer(new Shotgun()))));
            gun.Shoot();
        }

    }


    public abstract class Gun
    {
        public float range;
        public float volume;
        public abstract void Shoot();

    }

    public abstract class GunDecorator : Gun
    {
        protected Gun gun;
        
        public void SetGun(Gun gun)
        {
            this.gun = gun;
        }

        public override void Shoot()
        {
            if (gun != null)
            {
                Debug.Log("Volume: " + volume);
                gun.Shoot();
            }
        }
    }

    public class Shotgun : Gun
    {
        public override void Shoot()
        {
            Debug.Log("Volume: " + volume);
            Debug.Log("Shotgun SHoots!");
        }
    }

    public class Silencer : GunDecorator
    {
        public Silencer(Gun gun)
        {
            this.gun = gun;
        }

        public override void Shoot()
        {
            gun.volume = 0;
            gun.Shoot();
        }
    }

    public class Scope : GunDecorator
    {
        public Gun decoratedGun;
        public Scope(Gun gun)
        {
            decoratedGun = gun;
        }

        public override void Shoot()
        {
            gun.range += 10;
            gun.Shoot();
            
        }
    }
}