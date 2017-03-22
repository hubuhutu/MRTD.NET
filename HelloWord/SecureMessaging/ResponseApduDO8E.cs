﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelloWord.Infrastructure;

namespace HelloWord.SecureMessaging
{
    public class ResponseApduDO8E : IBinary
    {
        private readonly IBinary _responseApdu;

        public ResponseApduDO8E(IBinary responseApdu)
        {
            _responseApdu = responseApdu;
        }
        public byte[] Bytes()
        {
            return _responseApdu
                .Bytes()
                .Skip(6)
                .Take(8)
                .ToArray();
        }
    }
}
