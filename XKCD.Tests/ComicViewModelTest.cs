using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XKCD.Core.Model;
using XKCD.Core.ViewModel;
using XKCD.Tests.Fake;

namespace XKCD.Tests
{
    [TestFixture]
    public class ComicViewModelTest
    {
        private FakeComicService comicService;
        private ComicViewModel viewModel;

        [OneTimeSetUp]
        public void Init()
        {
            comicService = new FakeComicService();
            comicService.AddFake( new Comic( 1, DateTime.Now, "First", "", "", "" ) );
            comicService.AddFake( new Comic( 2, DateTime.Now, "Second", "", "", "" ) );
            comicService.AddFake( new Comic( 3, DateTime.Now, "Third", "", "", "" ) );
            comicService.AddFake( new Comic( 4, DateTime.Now, "Current", "", "", "" ) );
        }

        [SetUp]
        public void SetUpTest()
        {
            viewModel = new ComicViewModel( comicService );
        }

        [Test]
        public void Title_IsEmpty_OnNewViewModel()
        {
            var expected = string.Empty;

            var actual = viewModel.Title;

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void Number_IsZero_OnNewViewModel()
        {
            var expected = 0;

            var actual = viewModel.Number;

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public async Task Title_ContainsTitleOfCurrentComic_AfterFirstShown()
        {
            var expected = "Current";

            await viewModel.OnFirstShown();
            var actual = viewModel.Title;

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public async Task Number_ContainsNumberOfCurrentComic_AfterFirstShown()
        {
            var expected = 4;

            await viewModel.OnFirstShown();
            var actual = viewModel.Number;

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void Number_IsOne_AfterExecutingFirstComicCommand()
        {
            var expected = 1;

            viewModel.FirstComicCommand.Execute( null );
            var actual = viewModel.Number;

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void Number_Is2_AfterExecutingFirstComicCommandAndThenNextComicCommand()
        {
            var expected = 2;
            viewModel.FirstComicCommand.Execute( null );

            viewModel.NextComicCommand.Execute( null );
            var actual = viewModel.Number;

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void Number_IsThree_AfterExecutingLastComicCommandAndThenPreviousComicCommand()
        {
            var expected = 3;
            viewModel.LastComicCommand.Execute( null );

            viewModel.PreviousComicCommand.Execute( null );
            var actual = viewModel.Number;

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void Number_IsFour_AfterExecutingLastComicCommand()
        {
            var expected = 4;

            viewModel.LastComicCommand.Execute( null );
            var actual = viewModel.Number;

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void Number_IsBetweenOneAndZero_AfterExecutingRandomComicCommand()
        {
            var possibleNumbers = new List<int>() { 1, 2, 3, 4 };
            // Loading the current last comic to set the internal current comic number
            viewModel.LastComicCommand.Execute( null );

            viewModel.RandomComicCommand.Execute( null );
            var actual = viewModel.Number;

            CollectionAssert.Contains( possibleNumbers, actual );
        }

        [Test]
        public void PermanentLink_ContainsTheLinkToComicNumberFour_AfterExecutingLastComicCommand()
        {
            var expected = "http://xkcd.com/4/";

            viewModel.LastComicCommand.Execute( null );
            var actual = viewModel.PermanentLink;

            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void PermanentLink_IsEmpty_OnNewViewModel()
        {
            var expected = string.Empty;

            var actual = viewModel.PermanentLink;

            Assert.AreEqual( expected, actual );
        }
    }
}
