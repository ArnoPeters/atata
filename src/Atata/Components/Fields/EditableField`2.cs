﻿using System;

namespace Atata
{
    /// <summary>
    /// Represents the base class for editable field controls.
    /// It can be used for controls like &lt;input&gt;, &lt;select&gt; and other editable controls.
    /// </summary>
    /// <typeparam name="T">The type of the control's data.</typeparam>
    /// <typeparam name="TOwner">The type of the owner page object.</typeparam>
    public abstract class EditableField<T, TOwner> : Field<T, TOwner>
        where TOwner : PageObject<TOwner>
    {
        protected EditableField()
        {
        }

        /// <summary>
        /// Gets the <see cref="DataProvider{TData, TOwner}"/> instance for the value indicating whether the control is read-only.
        /// By default checks "readonly" attribute of scope element.
        /// Override <see cref="GetIsReadOnly"/> method to change the behavior.
        /// </summary>
        public DataProvider<bool, TOwner> IsReadOnly => GetOrCreateDataProvider("read-only", GetIsReadOnly);

        /// <summary>
        /// Gets the verification provider that gives a set of verification extension methods.
        /// </summary>
        public new FieldVerificationProvider<T, EditableField<T, TOwner>, TOwner> Should => new FieldVerificationProvider<T, EditableField<T, TOwner>, TOwner>(this);

        protected virtual bool GetIsReadOnly()
        {
            return Attributes.ReadOnly;
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        protected abstract void SetValue(T value);

        /// <summary>
        /// Sets the value.
        /// Also executes <see cref="TriggerEvents.BeforeSet" /> and <see cref="TriggerEvents.AfterSet" /> triggers.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The instance of the owner page object.</returns>
        public TOwner Set(T value)
        {
            ExecuteTriggers(TriggerEvents.BeforeSet);
            Log.Start(new DataSettingLogSection(this, ConvertValueToString(value)));

            SetValue(value);

            Log.EndSection();
            ExecuteTriggers(TriggerEvents.AfterSet);

            return Owner;
        }

        /// <summary>
        /// Sets the random value.
        /// For value generation uses randomization attributes, for example:
        /// <see cref="RandomizeStringSettingsAttribute"/>, <see cref="RandomizeNumberSettingsAttribute"/>, <see cref="RandomizeIncludeAttribute"/>, etc.
        /// Also executes <see cref="TriggerEvents.BeforeSet" /> and <see cref="TriggerEvents.AfterSet" /> triggers.
        /// </summary>
        /// <returns>The instance of the owner page object.</returns>
        public TOwner SetRandom()
        {
            T value = GenerateRandomValue();
            return Set(value);
        }

        /// <summary>
        /// Sets the random value and records it to <paramref name="value"/> parameter.
        /// For value generation uses randomization attributes, for example:
        /// <see cref="RandomizeStringSettingsAttribute" />, <see cref="RandomizeNumberSettingsAttribute" />, <see cref="RandomizeIncludeAttribute" />, etc.
        /// Also executes <see cref="TriggerEvents.BeforeSet" /> and <see cref="TriggerEvents.AfterSet" /> triggers.
        /// </summary>
        /// <param name="value">The generated value.</param>
        /// <returns>The instance of the owner page object.</returns>
        public TOwner SetRandom(out T value)
        {
            value = GenerateRandomValue();
            return Set(value);
        }

        /// <summary>
        /// Sets the random value and invokes <paramref name="callback"/>.
        /// For value generation uses randomization attributes, for example:
        /// <see cref="RandomizeStringSettingsAttribute" />, <see cref="RandomizeNumberSettingsAttribute" />, <see cref="RandomizeIncludeAttribute" />, etc.
        /// Also executes <see cref="TriggerEvents.BeforeSet" /> and <see cref="TriggerEvents.AfterSet" /> triggers.
        /// </summary>
        /// <param name="callback">The callback to be invoked after the value is set.</param>
        /// <returns>The instance of the owner page object.</returns>
        public TOwner SetRandom(Action<T> callback)
        {
            T value = GenerateRandomValue();
            Set(value);
            callback?.Invoke(value);
            return Owner;
        }

        /// <summary>
        /// Generates the random value.
        /// </summary>
        /// <returns>The generated value.</returns>
        protected virtual T GenerateRandomValue()
        {
            return ValueRandomizer.GetRandom<T>(Metadata);
        }
    }
}
