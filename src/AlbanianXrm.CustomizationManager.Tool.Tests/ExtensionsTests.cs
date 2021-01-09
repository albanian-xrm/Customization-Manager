using AlbanianXrm.CustomizationManager.Helpers;
using Xunit;

namespace AlbanianXrm.CustomizationManager
{
    public class ExtensionsTests
    {
        [Fact]
        public void PropertyLambaMustNotBeAMethodCall()
        {
            var dummyControl = new DummyControl();
            var dummyViewModel = new DummyViewModel();
            var ex = Record.Exception(() => dummyControl.Bind(_ => _.ToString(), dummyViewModel, _ => _.ToString()));

            Assert.NotNull(ex);
            Assert.Equal(string.Format(Extensions.EXPRESSION_REFERS_METHOD, "_ => _.ToString()"), ex.Message);
        }

        [Fact]
        public void PropertyLambaMustNotBeAField()
        {
            var dummyControl = new DummyControl();
            var dummyViewModel = new DummyViewModel();
            var ex = Record.Exception(() => dummyControl.Bind(_ => _.DummyString, dummyViewModel, _ => _._DummyString));

            Assert.NotNull(ex);
            Assert.Equal(string.Format(Extensions.EXPRESSION_REFERS_FIELD, "_ => _.DummyString"), ex.Message);
        }
    }
}
