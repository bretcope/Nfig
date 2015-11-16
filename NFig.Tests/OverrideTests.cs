﻿using System.Collections.Generic;
using NUnit.Framework;

namespace NFig.Tests
{
    [TestFixture]
    public class OverrideTests
    {
        [Test]
        public void ValidOverrideTest()
        {
            var factory = new SettingsFactory<OverrideSettings, Tier, DataCenter>();

            var overrides = new List<SettingValue<Tier, DataCenter>>()
            {
                new SettingValue<Tier, DataCenter>("A", "10", Tier.Any, DataCenter.Any),
                new SettingValue<Tier, DataCenter>("B", "11", Tier.Any, DataCenter.Any),
            };

            var s = factory.GetAppSettings(Tier.Local, DataCenter.Local, overrides);

            Assert.AreEqual(s.A, 10);
            Assert.AreEqual(s.B, 11);
            Assert.AreEqual(s.C, 2);
        }

        [Test]
        public void InvalidOverrideTest()
        {
            var factory = new SettingsFactory<OverrideSettings, Tier, DataCenter>();

            var overrides = new List<SettingValue<Tier, DataCenter>>()
            {
                new SettingValue<Tier, DataCenter>("A", "a", Tier.Any, DataCenter.Any),
                new SettingValue<Tier, DataCenter>("B", "b", Tier.Any, DataCenter.Any),
                new SettingValue<Tier, DataCenter>("C", "12", Tier.Any, DataCenter.Any),
            };

            OverrideSettings s;
            var exceptions = factory.TryGetAppSettings(out s, Tier.Local, DataCenter.Local, overrides);

            Assert.True(exceptions != null && exceptions.Count == 2);

            var ex = (InvalidSettingValueException<Tier, DataCenter>) exceptions[0];
            Assert.True(ex.IsOverride);
            Assert.True(ex.SettingName == "A");

            Assert.AreEqual(s.A, 0);
            Assert.AreEqual(s.B, 1);
            Assert.AreEqual(s.C, 12);
        }

        private class OverrideSettings : SettingsBase
        {
            [Setting(0)]
            public int A { get; private set; }

            [Setting(1)]
            public int B { get; private set; }

            [Setting(2)]
            public int C { get; private set; }
        }
    }
}