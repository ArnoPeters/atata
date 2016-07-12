﻿namespace Atata
{
    public interface IDataVerificationProvider<TData, TOwner> : IVerificationProvider<TOwner>
        where TOwner : PageObject<TOwner>
    {
        IDataProvider<TData, TOwner> DataProvider { get; }
    }
}