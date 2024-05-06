namespace ConfigurableWarning.Options.Untyped {
    internal class UntypedBoolOption(BoolOption inner) : IUntypedOption {
        public string GetName() => inner.GetName();
        public object GetValue() => inner.GetValue();
        public void SetValue(object value) => inner.SetValue((bool)value);
    }
}
