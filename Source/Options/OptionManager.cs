using System.Collections.Generic;
using Zorro.Settings;

namespace ConfigurableWarning.Options {
    public class OptionManager {
        public static OptionManager Instance { get; } = new();
        public static DefaultSettingsSaveLoad SaveLoader { get; } = new();
        private readonly Dictionary<string, IUntypedOption> _options = new();

        public void Register<T>(IOption<T> opt) {
            _options.Add(opt.GetName(), opt.AsUntyped());
        }

        public void Register(IUntypedOption opt) {
            _options.Add(opt.GetName(), opt);
        }
    }
}
