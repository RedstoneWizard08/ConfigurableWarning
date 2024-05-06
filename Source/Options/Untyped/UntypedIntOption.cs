namespace ConfigurableWarning.Options.Untyped {
    internal class UntypedIntOption(IntOption inner) : IUntypedOption {
        public string GetName() => inner.GetName();
        public object GetValue() => inner.GetValue();
        public void SetValue(object value) => inner.SetValue((int)value);
    }
}
