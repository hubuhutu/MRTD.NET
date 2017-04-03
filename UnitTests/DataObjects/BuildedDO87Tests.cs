﻿using HelloWord.Infrastructure;
using HelloWord.SecureMessaging.DataObjects.Builded;
using NUnit.Framework;
using UnitTests.FakeObjects;

namespace UnitTests.DataObjects
{
    [TestFixture]
    public class BuildedDO87Tests
    {
        [Test]
        [TestCase("8709016375432908C044F6", "00A4020C02011E")]
        public void Build_DO87_from_UnprotectedCommandApdu(string exc, string rawCommandApdu)
        {
            Assert.AreEqual(
                    exc,
                    new Hex(
                        new BuildedDO87(
                            new BinaryHex(rawCommandApdu), // rawCommandApdu 
                            new FkKSenc()
                        )
                    ).ToString()
                );
        }
    }
}
