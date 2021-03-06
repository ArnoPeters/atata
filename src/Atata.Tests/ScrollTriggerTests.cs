﻿using NUnit.Framework;

namespace Atata.Tests
{
    public class ScrollTriggerTests : UITestFixture
    {
        private ScrollablePage page;

        protected override bool ReuseDriver => false;

        protected override void OnSetUp()
        {
            page = Go.To<ScrollablePage>();
        }

        [Test]
        public void ScrollDownAttribute()
        {
            page.
                BottomText.Should.Not.BeVisibleInViewPort().
                BottomText.Triggers.Add(new ScrollDownAttribute(TriggerEvents.BeforeGet));

            page.
                BottomText.Get(out string text).
                BottomText.Should.BeVisibleInViewPort();
        }

        [Test]
        public void ScrollUpAttribute()
        {
            page.
                ScrollDown().
                TopText.Should.Not.BeVisibleInViewPort().
                TopText.Triggers.Add(new ScrollUpAttribute(TriggerEvents.BeforeGet));

            page.
                TopText.Get(out string text).
                TopText.Should.BeVisibleInViewPort();
        }

        [Test]
        public void ScrollToAttribute()
        {
            page.
                BottomText.Should.Not.BeVisibleInViewPort().
                BottomText.Triggers.Add(new ScrollToAttribute(TriggerEvents.BeforeGet));

            page.
                BottomText.Get(out string text).
                BottomText.Should.BeVisibleInViewPort();
        }
    }
}
