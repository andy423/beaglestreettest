using AutoFixture;

namespace BeagleSteetTest.Domain.UnitTests
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
