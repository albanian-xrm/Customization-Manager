using AlbanianXrm.CustomizationManager.Helpers;
using AlbanianXrm.CustomizationManager.Interfaces;
using AlbanianXrm.CustomizationManager.Models;
using FakeItEasy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Xunit;

namespace AlbanianXrm.CustomizationManager
{
    public class SolutionComponentsContainerServiceTests : FakeXrmEasyTestsBase
    {
        [Fact]
        public void RefreshSolutionListUsesService()
        {
            var iWorkerHostWrapper = A.Fake<IWorkerHostWrapper>();

            var solutionComponentsContainer = new SolutionComponentsContainer();
            var toolViewModel = new ToolViewModel { OrganizationService = _service };
            toolViewModel.AsyncWorkQueue = new AsyncWorkQueue(iWorkerHostWrapper, toolViewModel);
            solutionComponentsContainer.InitializeBindings(toolViewModel);
            var doWorkEventArgs = new DoWorkEventArgs(null);

            solutionComponentsContainer.RefreshSolutionList(A.Fake<BackgroundWorker>(), doWorkEventArgs);

            A.CallTo(() => _service.RetrieveMultiple(A<QueryExpression>.Ignored)).MustHaveHappened(1, Times.Exactly);
        }

        [Fact]
        public void MnuRefreshSolutionsUsesService()
        {
            _context.Initialize(new Solution[]{new Solution()
            {
                SolutionId = Guid.NewGuid(),
                FriendlyName = "Active Solution",
                UniqueName = "Active"
            }, new Solution()
            {
                SolutionId = Guid.NewGuid(),
                FriendlyName = "Default Solution",
                UniqueName = "Default"
            } });

            var iWorkerHostWrapper = new DummyBackgroundWorkerHostWrapper();

            var solutionComponentsContainer = new SolutionComponentsContainer();
            var toolViewModel = new ToolViewModel { OrganizationService = _service };
            toolViewModel.AsyncWorkQueue = new AsyncWorkQueue(iWorkerHostWrapper, toolViewModel);
            solutionComponentsContainer.InitializeBindings(toolViewModel);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            solutionComponentsContainer.MnuRefreshSolutions_Click(null, new EventArgs());

            while (iWorkerHostWrapper.Working > 0 && stopwatch.ElapsedMilliseconds < 10000) //10 seconds threshold for the asynchronous operation  
            {
                Thread.Sleep(15);
            }


            Assert.Equal("Default", (solutionComponentsContainer.cmbFilteringSolution.SelectedItem as Solution)?.UniqueName);
        }

        [Fact]
        public void CmbFilteringSolutionUsesService()
        {
            var defaultSolutionGuid = Guid.NewGuid();
            _context.Initialize(new Entity[]{new Solution()
            {
                SolutionId = Guid.NewGuid(),
                FriendlyName = "Active Solution 2",
                UniqueName = "Active2"
            }, new Solution()
            {
                SolutionId = defaultSolutionGuid,
                FriendlyName = "Default Solution",
                UniqueName = "Default"
            } ,
            new SolutionComponent(){
                Id = Guid.NewGuid(),
                [SolutionComponent.Fields.SolutionId] = new EntityReference(Solution.EntityLogicalName, defaultSolutionGuid),
                [SolutionComponent.Fields.ComponentType] = new OptionSetValue((int)OptionSets.ComponentType.WebResource)
            } });

            var iWorkerHostWrapper = new DummyBackgroundWorkerHostWrapper();

            var solutionComponentsContainer = A.Fake<SolutionComponentsContainer>((o) => o.CallsBaseMethods());
            var toolViewModel = new ToolViewModel { OrganizationService = _service };
            toolViewModel.AsyncWorkQueue = new AsyncWorkQueue(iWorkerHostWrapper, toolViewModel);
            toolViewModel.Solutions.Add(new Solution()
            {

                SolutionId = Guid.NewGuid(),
                FriendlyName = "Active Solution",
                UniqueName = "Active"
            });
            solutionComponentsContainer.InitializeBindings(toolViewModel);

            solutionComponentsContainer.CmbFilteringSolution_DropDown(solutionComponentsContainer, new EventArgs());

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            solutionComponentsContainer.MnuRefreshSolutions_Click(solutionComponentsContainer, new EventArgs());
            while (iWorkerHostWrapper.Working > 0 && stopwatch.ElapsedMilliseconds < 10000) //10 seconds threshold for the asynchronous operation  
            {
                Thread.Sleep(15);
            }

            stopwatch.Restart();
            solutionComponentsContainer.CmbFilteringSolution_SelectedValueChanged(solutionComponentsContainer, new EventArgs());

            while (iWorkerHostWrapper.Working > 0 && stopwatch.ElapsedMilliseconds < 10000) //10 seconds threshold for the asynchronous operation  
            {
                Thread.Sleep(15);
            }

            Assert.Equal("Default", (solutionComponentsContainer.cmbFilteringSolution.SelectedItem as Solution)?.UniqueName);
        }
    }
}
