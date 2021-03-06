﻿namespace Atata
{
    public class PageObjectVerificationProvider<TPageObject> :
        UIComponentVerificationProvider<TPageObject, PageObjectVerificationProvider<TPageObject>, TPageObject>,
        IPageObjectVerificationProvider<TPageObject>
        where TPageObject : PageObject<TPageObject>
    {
        public PageObjectVerificationProvider(TPageObject pageObject)
            : base(pageObject)
        {
        }

        public NegationPageObjectVerificationProvider Not => new NegationPageObjectVerificationProvider(Owner, this);

        public class NegationPageObjectVerificationProvider :
            NegationUIComponentVerificationProvider<NegationPageObjectVerificationProvider>,
            IPageObjectVerificationProvider<TPageObject>
        {
            internal NegationPageObjectVerificationProvider(TPageObject pageObject, IVerificationProvider<TPageObject> verificationProvider)
                : base(pageObject, verificationProvider)
            {
            }
        }
    }
}
