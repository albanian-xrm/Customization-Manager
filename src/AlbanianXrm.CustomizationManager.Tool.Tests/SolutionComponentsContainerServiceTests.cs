using AlbanianXrm.CustomizationManager.Helpers;
using AlbanianXrm.CustomizationManager.Interfaces;
using AlbanianXrm.CustomizationManager.Models;
using AlbanianXrm.CustomizationManager.Tool;
using FakeItEasy;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        }

        [Fact]
        public void MnuRefreshSolutionsUsesService()
        {
            _context.Initialize(new Solution()
            {
                SolutionId = Guid.NewGuid(),
                FriendlyName = "Active Solution",
                UniqueName = "Active"
            });

            var iWorkerHostWrapper = new DummyWorkerHostWrapper();

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

        }   
        
        [Fact]
        public void CmbFilteringSolutionUsesService()
        {
            _context.Initialize(new Solution()
            {
                SolutionId = Guid.NewGuid(),
                FriendlyName = "Active Solution",
                UniqueName = "Active"
            });

            var iWorkerHostWrapper = new DummyWorkerHostWrapper();

            var solutionComponentsContainer = new SolutionComponentsContainer();
            var toolViewModel = new ToolViewModel { OrganizationService = _service };
            toolViewModel.AsyncWorkQueue = new AsyncWorkQueue(iWorkerHostWrapper, toolViewModel);
            solutionComponentsContainer.InitializeBindings(toolViewModel);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            solutionComponentsContainer.CmbFilteringSolution_DropDown(null, new EventArgs());
            while (iWorkerHostWrapper.Working > 0 && stopwatch.ElapsedMilliseconds < 10000) //10 seconds threshold for the asynchronous operation  
            {
                Thread.Sleep(15);
            }

            solutionComponentsContainer.CmbFilteringSolution_SelectedValueChanged(null, new EventArgs());

            while (iWorkerHostWrapper.Working > 0 && stopwatch.ElapsedMilliseconds < 10000) //10 seconds threshold for the asynchronous operation  
            {
                Thread.Sleep(15);
            }

        }
    }
}
