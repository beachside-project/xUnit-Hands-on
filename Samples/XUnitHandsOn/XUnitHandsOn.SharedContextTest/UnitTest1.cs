using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace XUnitHandsOn.SharedContextTest
{
    public class HeavyFixture : IDisposable
    {
        public HeavyFixture() => Thread.Sleep(TimeSpan.FromSeconds(2));

        public void Use()
        {
        }

        public void Dispose() => Thread.Sleep(TimeSpan.FromSeconds(2));
    }

    [CollectionDefinition("Heavy collection")]
    public class HeavyCollection : ICollectionFixture<HeavyFixture>
    {
        // CollectionDefinitionを付与したクラスのみ作成すればよい
        // 特別な実装は不要
    }

    [Collection("Heavy collection")]
    public class UnitTest1 : IDisposable
    {
        private readonly HeavyFixture _heavyFixture;

        public UnitTest1(HeavyFixture heavyFixture)
        {
            _heavyFixture = heavyFixture;
        }

        [Fact]
        public void Test1() => _heavyFixture.Use();

        [Fact]
        public void Test2() => _heavyFixture.Use();

        public void Dispose()
        {
            //_heavyFixture.Dispose();
        } 
    }

    [Collection("Heavy collection")]
    public class UnitTest2 : IDisposable
    {
        private readonly HeavyFixture _heavyFixture;

        public UnitTest2(HeavyFixture heavyFixture)
        {
            _heavyFixture = heavyFixture;
        }

        [Fact]
        public void Test() => _heavyFixture.Use();

        public void Dispose()
        {
            //_heavyFixture.Dispose();
        }
    }

    public class AsyncHeavyFixture : IAsyncLifetime
    {
        public Task InitializeAsync() => Task.Delay(TimeSpan.FromSeconds(2));

        public void Use()
        {
        }

        public Task DisposeAsync() => Task.Delay(TimeSpan.FromSeconds(2));
    }

    public class UnitTest3 : IClassFixture<AsyncHeavyFixture>
    {
        private readonly AsyncHeavyFixture _asyncHeavyFixture;

        public UnitTest3(AsyncHeavyFixture asyncAsyncHeavyFixture)
        {
            _asyncHeavyFixture = asyncAsyncHeavyFixture;
        }

        [Fact]
        public void Test() => _asyncHeavyFixture.Use();
    }

}
