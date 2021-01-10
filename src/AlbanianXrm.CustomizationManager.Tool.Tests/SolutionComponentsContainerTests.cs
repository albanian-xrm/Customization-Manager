using AlbanianXrm.CustomizationManager.Models;
using FakeItEasy;
using System;
using Xunit;

namespace AlbanianXrm.CustomizationManager
{
    public class SolutionComponentsContainerTests
    {
        [Fact]
        public void CmbFilteringSolution_DropDown_DoesNothingIfItContainsElements()
        {
            var solutionComponentsContainer =A.Fake<SolutionComponentsContainer>((o)=>o.CallsBaseMethods());
            A.CallTo(() => solutionComponentsContainer.CmbFilteringSolution_SelectedValueChanged(A<object>.Ignored, A<EventArgs>.Ignored)).DoesNothing();
            solutionComponentsContainer.cmbFilteringSolution.Items.Add(new Solution() { UniqueName = "Default" });
            solutionComponentsContainer.cmbFilteringSolution.SelectedIndex = 0;

            solutionComponentsContainer.CmbFilteringSolution_DropDown(solutionComponentsContainer, new EventArgs());
        }

        [Fact]
        public void ControlNeedsViewModel()
        {
            var solutionComponentsContainer = new SolutionComponentsContainer();
            var ex = Record.Exception(() => solutionComponentsContainer.InitializeBindings(null));

            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Equal("Value cannot be null.\r\nParameter name: toolViewModel", ex.Message);
        }
    }
}
