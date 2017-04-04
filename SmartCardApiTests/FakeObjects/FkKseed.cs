﻿using SmartCardApi.Infrastructure;

namespace SmartCardApiTests.FakeObjects
{
    public class FkKSeed : IBinary
    {
        public byte[] Bytes()
        {
            return new BinaryHex("239AB9CB282DAF66231DC5A4DF6BFBAE").Bytes();
        }
    }
}
