﻿using SmartCardApi.Infrastructure;

namespace SmartCardApiTests.FakeObjects
{
    public class FkKSenc : IBinary
    {
        public byte[] Bytes()
        {
            return new BinaryHex("979EC13B1CBFE9DCD01AB0FED307EAE5").Bytes();
        }
    }
}
