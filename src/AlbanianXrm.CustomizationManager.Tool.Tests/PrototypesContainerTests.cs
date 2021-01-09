using AlbanianXrm.CustomizationManager.Interfaces;
using FakeItEasy;
using System;
using System.Windows.Forms;
using Xunit;

namespace AlbanianXrm.CustomizationManager.Tool
{
    public class PrototypesContainerTests
    {
        [Fact]
        public void WhoAmINeedsService()
        {
            var messageBroker = A.Fake<IMessageBroker>();
            A.CallTo(() => messageBroker.Show(Resources.MISSING_ORGANIZATION_SERVICE)).Returns(DialogResult.OK);
            var prototypesContainer = new PrototypesContainer();
            prototypesContainer.InitializeBindings(new ToolViewModel { MessageBroker = messageBroker });

            prototypesContainer.BtnWhoAmI_Click(prototypesContainer, new EventArgs());

            A.CallTo(() => messageBroker.Show(Resources.MISSING_ORGANIZATION_SERVICE)).MustHaveHappened(1, Times.Exactly);
        }

        [Fact]
        public void EntitiesNeedsService()
        {
            var messageBroker = A.Fake<IMessageBroker>();
            A.CallTo(() => messageBroker.Show(Resources.MISSING_ORGANIZATION_SERVICE)).Returns(DialogResult.OK);
            var prototypesContainer = new PrototypesContainer();
            prototypesContainer.InitializeBindings(new ToolViewModel { MessageBroker = messageBroker });

            prototypesContainer.BtnEntities_Click(prototypesContainer, new EventArgs());

            A.CallTo(() => messageBroker.Show(Resources.MISSING_ORGANIZATION_SERVICE)).MustHaveHappened(1, Times.Exactly);
        }

        [Fact]
        public void WebresourcesNeedsService()
        {
            var messageBroker = A.Fake<IMessageBroker>();
            A.CallTo(() => messageBroker.Show(Resources.MISSING_ORGANIZATION_SERVICE)).Returns(DialogResult.OK);
            var prototypesContainer = new PrototypesContainer();
            prototypesContainer.InitializeBindings(new ToolViewModel { MessageBroker = messageBroker });

            prototypesContainer.BtnWebresources_Click(prototypesContainer, new EventArgs());

            A.CallTo(() => messageBroker.Show(Resources.MISSING_ORGANIZATION_SERVICE)).MustHaveHappened(1, Times.Exactly);
        }

        [Fact]
        public void ControlNeedsViewModel()
        {
            var prototypesContainer = new PrototypesContainer();
            var ex = Record.Exception(() => prototypesContainer.InitializeBindings(null));

            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Equal("Value cannot be null.\r\nParameter name: toolViewModel", ex.Message);
        }
    }

}

