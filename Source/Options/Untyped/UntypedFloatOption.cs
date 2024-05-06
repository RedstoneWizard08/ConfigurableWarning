namespace ConfigurableWarning.Options.Untyped {
    internal class UntypedFloatOption(FloatOption inner) : IUntypedOption {
        public string GetName() => inner.GetName();
        public object GetValue() => inner.GetValue();
        public void SetValue(object value) => inner.SetValue((float)value);
    }
}
