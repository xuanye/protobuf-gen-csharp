#region Copyright notice and license
// Protocol Buffers - Google's data interchange format
// Copyright 2008 Google Inc.  All rights reserved.
// https://developers.google.com/protocol-buffers/
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are
// met:
//
//     * Redistributions of source code must retain the above copyright
// notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above
// copyright notice, this list of conditions and the following disclaimer
// in the documentation and/or other materials provided with the
// distribution.
//     * Neither the name of Google Inc. nor the names of its
// contributors may be used to endorse or promote products derived from
// this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
// OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

using System;
using System.IO;

namespace Google.Protobuf
{
    /// <summary>
    /// Thrown when a protocol message being parsed is invalid in some way,
    /// e.g. it contains a malformed varint or a negative byte length.
    /// </summary>
    public sealed class InvalidProtocolBufferException : IOException
    {
        public InvalidProtocolBufferException(string message)
            : base(message)
        {
        }

        public InvalidProtocolBufferException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public static InvalidProtocolBufferException MoreDataAvailable()
        {
            return new InvalidProtocolBufferException(
                "Completed reading a message while more data was available in the stream.");
        }

        public static InvalidProtocolBufferException TruncatedMessage()
        {
            return new InvalidProtocolBufferException(
                "While parsing a protocol message, the input ended unexpectedly " +
                "in the middle of a field.  This could mean either that the " +
                "input has been truncated or that an embedded message " +
                "misreported its own length.");
        }

        public static InvalidProtocolBufferException NegativeSize()
        {
            return new InvalidProtocolBufferException(
                "CodedInputStream encountered an embedded string or message " +
                "which claimed to have negative size.");
        }

        public static InvalidProtocolBufferException MalformedVarint()
        {
            return new InvalidProtocolBufferException(
                "CodedInputStream encountered a malformed varint.");
        }

        /// <summary>
        /// Creates an exception for an error condition of an invalid tag being encountered.
        /// </summary>
        public static InvalidProtocolBufferException InvalidTag()
        {
            return new InvalidProtocolBufferException(
                "Protocol message contained an invalid tag (zero).");
        }

        public static InvalidProtocolBufferException InvalidBase64(Exception innerException)
        {
            return new InvalidProtocolBufferException("Invalid base64 data", innerException);
        }

        public static InvalidProtocolBufferException InvalidEndTag()
        {
            return new InvalidProtocolBufferException(
                "Protocol message end-group tag did not match expected tag.");
        }

        public static InvalidProtocolBufferException RecursionLimitExceeded()
        {
            return new InvalidProtocolBufferException(
                "Protocol message had too many levels of nesting.  May be malicious.  " +
                "Use CodedInputStream.SetRecursionLimit() to increase the depth limit.");
        }

        public static InvalidProtocolBufferException JsonRecursionLimitExceeded()
        {
            return new InvalidProtocolBufferException(
                "Protocol message had too many levels of nesting.  May be malicious.  " +
                "Use JsonParser.Settings to increase the depth limit.");
        }

        public static InvalidProtocolBufferException SizeLimitExceeded()
        {
            return new InvalidProtocolBufferException(
                "Protocol message was too large.  May be malicious.  " +
                "Use CodedInputStream.SetSizeLimit() to increase the size limit.");
        }

        public static InvalidProtocolBufferException InvalidMessageStreamTag()
        {
            return new InvalidProtocolBufferException(
                "Stream of protocol messages had invalid tag. Expected tag is length-delimited field 1.");
        }
    }
}