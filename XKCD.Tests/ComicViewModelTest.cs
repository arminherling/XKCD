﻿using NUnit.Framework;
using System;
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
            var expected = true;

            var actual = string.IsNullOrEmpty( viewModel.Title );

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
    }
}
