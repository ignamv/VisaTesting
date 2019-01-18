using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ivi.Visa;
using System.Linq;

namespace VisaTesting
{
    /// <summary>
    /// A mock implementation of a Visa message-based session
    /// </summary>
    /// <remarks>
    /// All writes and reads are delegated to fields WriteHandler and ReadHandler
    /// </remarks>
    public class MockMessageBasedSession : IMessageBasedRawIO, IMessageBasedSession
    {
        public delegate void IWriteHandler(string msg);
        /// <summary>
        /// This delegate is called whenever the session is written to.
        /// The msg parameter is the message that was written.
        /// </summary>
        public IWriteHandler WriteHandler { get; set; }
        public delegate string IReadHandler();
        /// <summary>
        /// This delegate is called whenever the session is read from.
        /// Its return value becomes the read message.
        /// </summary>
        public IReadHandler ReadHandler { get; set; }

        public MockMessageBasedSession()
        {
            FormattedIO = new Ivi.Visa.FormattedIO.MessageBasedFormattedIO(this);
        }

        public void Read(byte[] buffer, long index, long count, out long actualCount, out ReadStatus readStatus)
        {
            string msg = ReadHandler();
            byte[] ret = Encoding.ASCII.GetBytes(msg);
            if (ret.Length > count)
            {
                // TODO: buffer excess bytes
                throw new Exception($"Buffer has size {count}, read {ret.Length} bytes");
            }
            actualCount = ret.Length;
            readStatus = ReadStatus.EndReceived;
            ret.CopyTo(buffer, 0);
        }

        public void Write(byte[] buffer, long index, long count)
        {
            string msg = Encoding.ASCII.GetString(buffer.Skip((int)index).Take((int)count).ToArray());
            WriteHandler(msg);
        }

        public void Dispose()
        {
        }

        public byte TerminationCharacter { get; set; }
        public bool TerminationCharacterEnabled { get; set; }

        public IMessageBasedFormattedIO FormattedIO { get; }
        public IMessageBasedRawIO RawIO => this;

        public string HardwareInterfaceName => "MockHardwareInterfaceName";
        public short HardwareInterfaceNumber => 0x987;
        public HardwareInterfaceType HardwareInterfaceType => HardwareInterfaceType.Custom;
        public string ResourceClass => "MockResourceClass";
        public short ResourceManufacturerId => 0x123;
        public string ResourceManufacturerName => "MockManufacturer";
        public string ResourceName => "MockResourceName";

        // None of the remaining functions are implemented


        public IOProtocol IOProtocol { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool SendEndEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int EventQueueCapacity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Version ResourceImplementationVersion => throw new NotImplementedException();

        public ResourceLockState ResourceLockState => throw new NotImplementedException();

        public Version ResourceSpecificationVersion => throw new NotImplementedException();

        public bool SynchronizeCallbacks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int TimeoutMilliseconds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler<VisaEventArgs> ServiceRequest;

        public void AbortAsyncOperation(IVisaAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public void AssertTrigger()
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginRead(int count)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginRead(int count, object state)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginRead(int count, VisaAsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginRead(byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginRead(byte[] buffer, object state)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginRead(byte[] buffer, long index, long count)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginRead(byte[] buffer, long index, long count, object state)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginRead(byte[] buffer, VisaAsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginRead(byte[] buffer, long index, long count, VisaAsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginWrite(string buffer)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginWrite(string buffer, object state)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginWrite(string buffer, VisaAsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginWrite(byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginWrite(byte[] buffer, object state)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginWrite(byte[] buffer, long index, long count)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginWrite(byte[] buffer, long index, long count, object state)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginWrite(byte[] buffer, VisaAsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IVisaAsyncResult BeginWrite(byte[] buffer, long index, long count, VisaAsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void DisableEvent(EventType eventType)
        {
            throw new NotImplementedException();
        }

        public void DiscardEvents(EventType eventType)
        {
            throw new NotImplementedException();
        }

        public void EnableEvent(EventType eventType)
        {
            throw new NotImplementedException();
        }

        public long EndRead(IVisaAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public string EndReadString(IVisaAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public long EndWrite(IVisaAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public void LockResource()
        {
            throw new NotImplementedException();
        }

        public void LockResource(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public void LockResource(int timeoutMilliseconds)
        {
            throw new NotImplementedException();
        }

        public string LockResource(TimeSpan timeout, string sharedKey)
        {
            throw new NotImplementedException();
        }

        public string LockResource(int timeoutMilliseconds, string sharedKey)
        {
            throw new NotImplementedException();
        }

        public byte[] Read()
        {
            throw new NotImplementedException();
        }

        public byte[] Read(long count)
        {
            throw new NotImplementedException();
        }

        public byte[] Read(long count, out ReadStatus readStatus)
        {
            throw new NotImplementedException();
        }

        public unsafe void Read(byte* buffer, long index, long count, out long actualCount, out ReadStatus readStatus)
        {
            throw new NotImplementedException();
        }

        public StatusByteFlags ReadStatusByte()
        {
            throw new NotImplementedException();
        }

        public string ReadString()
        {
            throw new NotImplementedException();
        }

        public string ReadString(long count)
        {
            throw new NotImplementedException();
        }

        public string ReadString(long count, out ReadStatus readStatus)
        {
            throw new NotImplementedException();
        }

        public void UnlockResource()
        {
            throw new NotImplementedException();
        }

        public VisaEventArgs WaitOnEvent(EventType eventType)
        {
            throw new NotImplementedException();
        }

        public VisaEventArgs WaitOnEvent(EventType eventType, out EventQueueStatus status)
        {
            throw new NotImplementedException();
        }

        public VisaEventArgs WaitOnEvent(EventType eventType, int timeoutMilliseconds)
        {
            throw new NotImplementedException();
        }

        public VisaEventArgs WaitOnEvent(EventType eventType, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public VisaEventArgs WaitOnEvent(EventType eventType, int timeoutMilliseconds, out EventQueueStatus status)
        {
            throw new NotImplementedException();
        }

        public VisaEventArgs WaitOnEvent(EventType eventType, TimeSpan timeout, out EventQueueStatus status)
        {
            throw new NotImplementedException();
        }

        public void Write(byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public unsafe void Write(byte* buffer, long index, long count)
        {
            throw new NotImplementedException();
        }

        public void Write(string buffer)
        {
            throw new NotImplementedException();
        }

        public void Write(string buffer, long index, long count)
        {
            throw new NotImplementedException();
        }
    }
}
