using AlbanianXrm.CustomizationManager.Helpers;
using AlbanianXrm.CustomizationManager.Interfaces;
using FakeItEasy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Windows.Forms;
using Xunit;

namespace AlbanianXrm.CustomizationManager.Tool
{
    public class PrototypesContainerServiceTests : FakeXrmEasyTestsBase
    {
        [Fact]
        public void WhoAmINeedsService()
        {
            _context.CallerId = new EntityReference("systemuser")
            {
                Id = Guid.NewGuid()
            };
            var messageBroker = A.Fake<IMessageBroker>();
            A.CallTo(() => messageBroker.Show(string.Format(Resources.HELLO_USERID, _context.CallerId.Id))).Returns(DialogResult.OK);
            var prototypesContainer = new PrototypesContainer();
            prototypesContainer.InitializeBindings(new ToolViewModel { MessageBroker = messageBroker, OrganizationService = _service });

            prototypesContainer.BtnWhoAmI_Click(prototypesContainer, new EventArgs());

            A.CallTo(() => messageBroker.Show(string.Format(Resources.HELLO_USERID, _context.CallerId.Id))).MustHaveHappened(1, Times.Exactly);
        }

        [Fact]
        public void EntitiesNeedsService()
        {
            _context.AddFakeMessageExecutor<RetrieveAllEntitiesRequest>(new RetrieveAllEntitiesExecutor());
            var managed = new EntityMetadata()
            {
                LogicalName = "albxrm_managed",
                SchemaName = "albxrm_Managed",
                IsCustomizable = new BooleanManagedProperty(false)
            };
            managed.SetSealedPropertyValue(nameof(managed.IsManaged), true);
            _context.InitializeMetadata(
                new EntityMetadata[]{
                    new EntityMetadata()
                    {
                        LogicalName = "albxrm_unittest",
                        SchemaName = "albxrm_UnitTest",
                        IsCustomizable = new BooleanManagedProperty(true)
                    },new EntityMetadata()
                    {
                        LogicalName = "albxrm_noncustomizable",
                        SchemaName = "albxrm_NonCustomizable",
                        IsCustomizable = new BooleanManagedProperty(false)
                    },
                    managed});
            var messageBroker = A.Fake<IMessageBroker>();
            A.CallTo(() => messageBroker.Show(string.Format(Resources.UNMANAGED_ENTITIES, 1) + "\r\nalbxrm_UnitTest")).Returns(DialogResult.OK);
            var prototypesContainer = new PrototypesContainer();
            prototypesContainer.InitializeBindings(new ToolViewModel { MessageBroker = messageBroker, OrganizationService = _service });

            prototypesContainer.BtnEntities_Click(prototypesContainer, new EventArgs());

            A.CallTo(() => messageBroker.Show(string.Format(Resources.UNMANAGED_ENTITIES, 1) + "\r\nalbxrm_UnitTest")).MustHaveHappened(1, Times.Exactly);
        }

        [Fact]
        public void WebresourcesNeedsService()
        {
            var messageBroker = A.Fake<IMessageBroker>();
            A.CallTo(() => messageBroker.Show(string.Format(Resources.UNMANAGED_ENTITIES, 0)+"\r\n")).Returns(DialogResult.OK);
            var prototypesContainer = new PrototypesContainer();
            prototypesContainer.InitializeBindings(new ToolViewModel { MessageBroker = messageBroker, OrganizationService = _service });


            prototypesContainer.BtnWebresources_Click(prototypesContainer, new EventArgs());

            A.CallTo(() => messageBroker.Show(string.Format(Resources.UNMANAGED_ENTITIES, 0) + "\r\n")).MustHaveHappened(1, Times.Exactly);
        }
    }

}

