using Ivi.Visa;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisaTesting
{
    class ExpectMessageBasedSession : IDisposable
    {
        private Queue<string> simulatedReads, actualWrites = new Queue<string>();
        public MockMessageBasedSession session = new MockMessageBasedSession();

        public ExpectMessageBasedSession()
        {
            session.WriteHandler = WriteHandler;
        }

        public Queue<string> SimulatedReads
        {
            get { return simulatedReads; }
            set {
                simulatedReads = value;
                session.ReadHandler = simulatedReads.Dequeue;
            }
        }

        private void WriteHandler(string msg)
        {
            string expected = ExpectedWrites.Dequeue();
            Assert.AreEqual(expected, msg);
        }
        public Queue<string> ExpectedWrites { get; set; }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            disposed = true;
            if (disposing)
                session.Dispose();
            // Ensure all messages were read
            Assert.AreEqual(0, simulatedReads.Count);
            // Ensure all expected writes happened
            Assert.AreEqual(0, ExpectedWrites.Count);
        }
    }
}
