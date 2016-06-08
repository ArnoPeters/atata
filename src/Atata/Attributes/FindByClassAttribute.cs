﻿namespace Atata
{
    public class FindByClassAttribute : TermFindAttribute
    {
        public FindByClassAttribute(TermMatch match)
            : base(match)
        {
        }

        public FindByClassAttribute(TermFormat format, TermMatch match = TermMatch.Inherit)
            : base(format, match)
        {
        }

        public FindByClassAttribute(string value, TermMatch match)
            : base(value, match)
        {
        }

        public FindByClassAttribute(params string[] values)
            : base(values)
        {
        }

        protected override TermFormat DefaultFormat
        {
            get { return TermFormat.Kebab; }
        }

        public override IComponentScopeLocateStrategy CreateStrategy(UIComponentMetadata metadata)
        {
            return new FindByClassStrategy();
        }
    }
}
