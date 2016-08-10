# SimplestIocContainerCSharp
Just simplest IoC container on C#

This container is not thread-safe.

Usage:

```c#
    public interface IGod
    {
        string Attack();
    }

    public interface IWeapon
    {
        string Hit();
    }

    public class Thor : IGod
    {
        private readonly IWeapon _weapon;

        public Thor(IWeapon weapon)
        {
            if (weapon == null)
                throw new ArgumentNullException(nameof(weapon));
            _weapon = weapon;
        }

        public string Attack()
        {
            return _weapon.Hit();
        }
    }

    public class Mjolnir : IWeapon
    {
        public string Hit()
        {
            return "BADABOOM!!!";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.Bind<IWeapon>(() => new Mjolnir());
            container.Bind<IGod>(c => new Thor(c.Resolve<IWeapon>()));

            var god = container.Resolve<IGod>();
            Console.WriteLine(god.Attack());
            Console.ReadLine();
        }
    }

```
will print:
BADABOOM!!!

You can use key object instead type parameter or specify single or multiple instance binding.
