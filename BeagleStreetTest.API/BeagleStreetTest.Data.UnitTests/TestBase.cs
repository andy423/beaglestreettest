using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;

namespace BeagleStreetTest.Data.UnitTests
{
    public class TestBase
    {
        private Fixture _fixture;

        public TestBase() 
        { 
            _fixture = new Fixture();
        }

        public T Create<T>()
        {
            return _fixture.Create<T>();
        }
    }
}
