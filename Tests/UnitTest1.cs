using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplestIocContainer;

namespace Tests
{
    public interface IUniverse
    {
        string Hip();
    }

    public interface IGod
    {
        string Hop();
    }

    class Odin : IGod
    {
        private readonly string _secret;

        public Odin(string secret)
        {
            _secret = secret;
        }

        public string Hop()
        {
            return "Odin" + _secret;
        }
    }

    class Zeus : IGod
    {
        private readonly string _secret;

        public Zeus(string secret)
        {
            _secret = secret;
        }

        public string Hop()
        {
            return "Zeus" + _secret;
        }
    }

    public interface IEarth
    {
        string Bar();
    }

    class Earth : IEarth
    {
        private readonly string _secret;

        public Earth(string secret)
        {
            _secret = secret;
        }

        public string Bar()
        {
            return _secret;
        }
    }

    public class Universe : IUniverse
    {
        private readonly IGod _god;
        private readonly IEarth _earth;

        public Universe(IGod god, IEarth earth)
        {
            _god = god;
            _earth = earth;
        }

        public string Hip()
        {
            return _god.Hop() + _earth.Bar();
        }
    }

    public interface IGodFactory
    {
        IGod Create(string name);
    }

    public class GodFactory : IGodFactory
    {
        public IGod Create(string name)
        {
            return new Odin("Bless you!, " + name);
        }
    }


    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var container = new Container()
                .Bind<IGod>(c => new Odin("GodSecret"))
                .Bind<IEarth>(c => new Earth("EarthSecret"))
                .Bind<IUniverse>(c => new Universe(c.Resolve<IGod>(), c.Resolve<IEarth>()));

            var result = container.Resolve<IUniverse>().Hip();
            Assert.AreEqual("OdinGodSecretEarthSecret", result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var container = new Container()
                .Bind<IUniverse>(c => new Universe(c.Resolve<IGodFactory>().Create("Nikolay"), c.Resolve<IEarth>()))
                .Bind<IGod>(c => new Odin("GodSecret"))
                .Bind(CreateInt)
                .Bind<IGodFactory>(() => new GodFactory())
                .Bind(typeof(IEarth), () => new Earth("EarthSecret"));

            var result = container.Resolve<IUniverse>().Hip();
            Assert.AreEqual("OdinBless you!, NikolayEarthSecret", result);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var container = new Container()
                .Bind(CreateInt);
            Assert.AreEqual(10, container.Resolve<int>());
        }

        private static int CreateInt()
        {
            return 10;
        }
    }
}
