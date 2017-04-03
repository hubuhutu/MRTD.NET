﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelloWord.Infrastructure;
using HelloWord.ISO7816.CommandAPDU.Body;
using HelloWord.ISO7816.CommandAPDU.Header;
using HelloWord.SecureMessaging.DataObjects.Builded;

namespace HelloWord.SecureMessaging
{
    public class ProtectedCommandApdu : IBinary
    {
        private readonly IBinary _kSmac;
        private readonly IBinary _kSenc;
        private readonly IBinary _incrementedSsc;
        private readonly IBinary _rawCommandApdu;


        public ProtectedCommandApdu(
                IBinary rawCommandApdu,
                IBinary kSenc,
                IBinary kSmac,
                IBinary incrementedSsc
            )
        {
            _rawCommandApdu = rawCommandApdu;
            _kSmac = kSmac;
            _kSenc = kSenc;
            _incrementedSsc = incrementedSsc;
        }
        public byte[] Bytes()
        {
            var do87 = new BuildedDO87(
                            _rawCommandApdu,
                            _kSenc
                        );
            var do97 = new BuildedDO97(_rawCommandApdu);
            var do8e = new BuildedDO8E(
                            _rawCommandApdu,
                            _incrementedSsc,
                            _kSmac,
                            _kSenc
                       );
            return new ConstructedProtectedCommandApdu(
                    new MaskedCommandApduHeader(
                        new CommandApduHeader(_rawCommandApdu)
                    ),
                    do87,
                    do97,
                    do8e
                ).Bytes();
        }
    }
}
